using UnityEngine;
using System.Collections;
using System;

//Projectile associated with damage dealer
public class Projectile : MonoBehaviour
{
    public Action<Projectile, Collider> HitCallback;
    public float lifetime = 5f;

    private void Start()
    {
        Invoke(nameof(Die), lifetime);
    }

    private void OnTriggerEnter(Collider col)
    {
        HitCallback(this, col);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
