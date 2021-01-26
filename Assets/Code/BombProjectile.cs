using UnityEngine;
using System.Collections;
using System;

//Projectile associated with damage dealer
public class BombProjectile : MonoBehaviour, IProjectile
{
    public float lifetime = 5f;
    public float explosionRadius = 2f;
    public float explosionDamage = 10f;
    public bool isBallistic = false;
    private Rigidbody r;
    private ParticleSystem ps;
    private int terrainLayer;
    

    public Action<IProjectile, Collider> HitCallback { get; set; }

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
        ps.transform.parent = null;
        ps.Play();

        Destroy(ps.gameObject, ps.main.duration);
        Destroy(this.gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
