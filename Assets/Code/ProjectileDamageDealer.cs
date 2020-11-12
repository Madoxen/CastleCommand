using UnityEngine;
using System.Collections;
using System;


//Shoots projectiles at enemies
//Requirements: ProjectilePrefab must have a Projectile Component
[RequireComponent(typeof(SingleTargetAcquisition))]
public class ProjectileDamageDealer : MonoBehaviour, IDamageDealer
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
        if (!ta.CurrentAttackTarget)
            return;


        Vector3 dir = ((transform.position + arrowOrigin) - ta.CurrentAttackTarget.transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, arrowOrigin + transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().HitCallback = OnProjectileHit;
        projectile.transform.LookAt(ta.CurrentAttackTarget.transform.position);
        projectile.GetComponent<Rigidbody>().velocity = -dir * ProjectileSpeed;
        Attacked?.Invoke(this);


        //This is optional, so null check 
        GetComponent<VariablePitchClipPlayer>()?.PlaySound();
    }


    public void OnProjectileHit(Projectile p, Collider col)
    {
        if (col.gameObject == ta.CurrentAttackTarget)
        {
            ta.CurrentAttackTarget.GetComponent<HealthComponent>().CurrentHealth -= Damage;
            Destroy(p.gameObject);
        }
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(transform.position + arrowOrigin, 0.1f);
    }
}
