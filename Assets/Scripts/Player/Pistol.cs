using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponController
{
    public override void Fire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = fireTrans.position;
        projGO.GetComponent<Rigidbody>().velocity = (directionFire.position - fireTrans.position).normalized * 40;
        base.Fire();
    }
}
