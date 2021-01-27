using UnityEngine;
using System.Collections;
using System;

//Projectile associated with damage dealer
public class BombProjectile : MonoBehaviour, IProjectile
{
    public float lifetime = 5f;
    public float explosionRadius = 2f;
    public int explosionDamage = 10;
    public bool isBallistic = false;
    private Rigidbody r;
    private ParticleSystem ps;
    private int terrainLayer;
   

    public Action<IProjectile, Collider> HitCallback { get; set; }
    public Team TargetedTeam { get; set; }

    private void Awake()
    {
        r = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
        terrainLayer = LayerMask.NameToLayer("Terrain");
    }

    private void Start()
    {
        Invoke(nameof(Die), lifetime);
    }

    private void FixedUpdate()
    {
        if (isBallistic && r.velocity.magnitude > 0.001)
            transform.rotation = Quaternion.LookRotation(r.velocity.normalized);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == terrainLayer)
            AfterHit(col);
    }

    public void AfterHit(Collider col)
    {
        //TODO: explode
        GetComponentInChildren<VariablePitchClipPlayer>().PlaySound();
        ps.transform.parent = null;
        ps.transform.localScale = new Vector3(1, 1, 1);
        ps.transform.localRotation = Quaternion.identity;
        ps.Play();
        

        Collider[] cs = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in cs)
        {
            Team t = c.GetComponent<ITeamable>()?.Team;
            if (t?.TeamID == TargetedTeam?.TeamID)
            {
                c.GetComponent<HealthComponent>().CurrentHealth -= explosionDamage;
            }
        }

        Destroy(this.gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
