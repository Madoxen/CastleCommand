using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BombTowerRotater : MonoBehaviour
{
    [SerializeField]
    BallisticProjectileDamageDealer dealer;
    [SerializeField]
    Transform cannon;
    // Start is called before the first frame update
    void Awake()
    {

    }


    private void FixedUpdate()
    {
        //Rotate towards last velocity, but only around Y (up, green axis)
        Vector3 euler = Quaternion.LookRotation(dealer.lastVelocity, transform.up).eulerAngles;
        transform.localRotation = Quaternion.Euler(new Vector3(0, euler.y, 0));
        cannon.transform.localRotation = Quaternion.Euler(new Vector3(euler.x, 0, 0));
    }
}
