using UnityEngine;
using System.Collections;
using System;


//Shoots projectiles at enemies
//Requirements: ProjectilePrefab must have a Projectile Component
[RequireComponent(typeof(SingleTargetAcquisition))]
public class ProjectileDamageDealer : MonoBehaviour, IDamageDealer, IDescriptorCreator
{
    public GameObject projectilePrefab;
    public int Damage;
    public float ProjectileSpeed;
    public Vector3 arrowOrigin;

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


        Vector3 dir = ((transform.position + arrowOrigin) - target.transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, arrowOrigin + transform.position, Quaternion.identity);
        projectile.GetComponent<IProjectile>().HitCallback = OnProjectileHit;
        projectile.GetComponent<IProjectile>().TargetedTeam = ta.CurrentAttackTarget.Team;
        projectile.transform.LookAt(target.transform.position);
        projectile.GetComponent<Rigidbody>().velocity = -dir * ProjectileSpeed;
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


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(transform.position + arrowOrigin, 0.1f);
    }

    public Descriptor CreateDescription()
    {
        return new Descriptor
        {
            group = DescriptorGroup.STATS,
            priority = 1,
            text = "<style=Stats>Damage: " + Damage + "\nProjectile Speed: " + ProjectileSpeed + "</style>"
        };
    }
}
