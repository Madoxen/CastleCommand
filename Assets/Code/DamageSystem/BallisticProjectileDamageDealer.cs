using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;


//Shoots projectiles at enemies
//Requirements: ProjectilePrefab must have a Projectile Component
[RequireComponent(typeof(SingleTargetAcquisition))]
public class BallisticProjectileDamageDealer : MonoBehaviour, IDamageDealer, IDescriptorCreator
{
    public GameObject projectilePrefab;
    public int Damage;
    public Vector3 arrowOrigin;
    public float projectileSpeed = 10.0f;
    const float gravity = 9.81f;


    private Vector3 debugVel;
    private Vector3 targetpos;
    private Vector3 debugNormal;
    SingleTargetAcquisition ta;

    public event Action<object> Attacked;

    void Awake()
    {
        ta = GetComponent<SingleTargetAcquisition>();
    }


    public void Attack()
    {
        if (!(ta.CurrentAttackTarget is MonoBehaviour target))
            return;
        
        Vector3 targetV = Vector3.zero;
        var agent = target.GetComponent<NavMeshAgent>();

        if (agent != null)
            targetV = agent.velocity;


        Collider c = target.GetComponent<Collider>();
        Vector3 center = c != null ? c.bounds.center - target.transform.position: Vector3.zero;

        (float, float)? angles = SolveBallistics(target.transform.position + center, targetV);
        if (angles != null) {
            GameObject projectile = Instantiate(projectilePrefab, arrowOrigin + transform.position, Quaternion.identity);
            
            //We have 3 points
            //Tower base
            //tower arrow origin
            //target
            //Derive plane from those 3 points, get normal, rotate around normal fin
            Vector3 p1 = transform.position; //0,0,0
            Vector3 p2 = transform.position + arrowOrigin;
            Vector3 p3 = targetpos;

            Vector3 v1 = p2 - p1;
            Vector3 v2 = p3 - p1;

            Vector3 normal = Vector3.Cross(v1, v2).normalized;
            debugNormal = normal;

            Vector3 targetDir = v2; targetDir.y = 0; targetDir.Normalize();
            targetDir = Quaternion.AngleAxis(-angles.Value.Item1, normal) * targetDir;
            targetDir.Normalize();

            projectile.GetComponent<Rigidbody>().velocity = targetDir * projectileSpeed;
            debugVel = projectile.GetComponent<Rigidbody>().velocity;
            projectile.GetComponent<IProjectile>().HitCallback = OnProjectileHit;
            projectile.GetComponent<IProjectile>().TargetedTeam = ta.CurrentAttackTarget.Team;
        }


        Attacked?.Invoke(this);
        //This is optional, so null check 
        //TODO: separate audio/visual from purely gameplay components
        GetComponent<VariablePitchClipPlayer>()?.PlaySound();
    }


    public void OnProjectileHit(IProjectile p, Collider col)
    {
        if (ta.CurrentAttackTarget is MonoBehaviour target && col.gameObject == target.gameObject)
        {
            target.GetComponent<HealthComponent>().CurrentHealth -= Damage;
            p.AfterHit(col);
        }
    }

    //Solves ballistics in 2D, for 3D ballistics pitch the angle in future target direction
    //Returns tuple of floats -> vertical angle, and horizontal angle in degrees
    //Returns null if no solution was found
    (float, float)? SolveBallistics(Vector3 tpos, Vector3 tvel)
    {
        float time = 0.0f;
        float step = 0.01f;
        const float simulationMaxTime = 5.5f;

        //Solvers for 2D plane
        float? optimal(float x, float y, float v)
        {
            //From wikipedia https://en.wikipedia.org/wiki/Projectile_motion#Angle_%7F'%22%60UNIQ--postMath-0000003A-QINU%60%22'%7F_required_to_hit_coordinate_(x,y)
            float r = v * v * v * v - gravity * (gravity * x * x + 2.0f * y * v * v);
            if (r < 0.0)
                return null;
            return Mathf.Atan((v * v - Mathf.Sqrt(r)) / (gravity * x));
        }

        float? lob(float x, float y, float v)
        {
            //From wikipedia https://en.wikipedia.org/wiki/Projectile_motion#Angle_%7F'%22%60UNIQ--postMath-0000003A-QINU%60%22'%7F_required_to_hit_coordinate_(x,y)
            float r = v * v * v * v - gravity * (gravity * x * x + 2.0f * y * v * v);
            if (r < 0.0)
                return null;
            return Mathf.Atan((v * v + Mathf.Sqrt(r)) / (gravity * x));
        }


        float travelTime(float x, float angle, float v)
        {
            return x / (Mathf.Cos(angle) * v);
        }

        Vector3 origin = transform.position + arrowOrigin;
        while (time < simulationMaxTime)
        {
            //Guess next position
            Vector3 predictedPos = tpos + (tvel * time);
            //Get pitch angle
            Vector3 targetDir = predictedPos - origin;
 
            float pitch = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
            targetpos = predictedPos;
            //Factor position on relative vertical 2D plane
            //We take our point as (0,0)
            float? angle = optimal(new Vector3(targetDir.x, 0, targetDir.z).magnitude, targetDir.y, projectileSpeed);
            
            if (angle != null)
            {
                float ttime = travelTime(new Vector3(targetDir.x, 0, targetDir.z).magnitude, (float)angle, projectileSpeed);
                if (ttime < time)
                {
                    return ((float)angle * Mathf.Rad2Deg, pitch);
                }
            }
            time += step;
        }
        return null;
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(transform.position + arrowOrigin, 0.1f);
        Gizmos.DrawSphere(targetpos, 0.1f);
        Gizmos.color = new Color(1, 1, 0);
        Gizmos.DrawRay(transform.position + arrowOrigin, debugVel.normalized * 10f);
        Gizmos.color = new Color(0, 1, 1);
        Gizmos.DrawRay(transform.position + arrowOrigin, -debugVel.normalized * 10f);
        Gizmos.color = new Color(0, 1, 0);

        Vector3 targetDir = targetpos - (arrowOrigin + transform.position);
        
        Gizmos.DrawRay(transform.position, debugNormal.normalized * 10f);
    }

    public Descriptor CreateDescription()
    {
        return new Descriptor
        {
            group = DescriptorGroup.STATS,
            priority = 1,
            text = "<style=Stats>Damage: " + Damage + "\nProjectile Speed: " + projectileSpeed + "</style>"
        };
    }
}
