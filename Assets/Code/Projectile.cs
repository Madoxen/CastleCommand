using UnityEngine;
using System.Collections;
using System;

//Projectile associated with damage dealer
public class Projectile : MonoBehaviour, IProjectile
{
    public Action<IProjectile, Collider> HitCallback { get; set; }
    public float lifetime = 5f;
    public bool isSticky = false;
    public bool isBallistic = false;
    private Rigidbody r;
    private int terrainLayer;


    private void Awake()
    {
        r = GetComponent<Rigidbody>();
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
        HitCallback(this, col);
        if (col.gameObject.layer == terrainLayer)
            AfterHit(col);
    }

    public void AfterHit(Collider col)
    {
        if (isSticky)
        {
            r.velocity = Vector3.zero;
            r.isKinematic = true;
            this.transform.parent = col.transform;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
