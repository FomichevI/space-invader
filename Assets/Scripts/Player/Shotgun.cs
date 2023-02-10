using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponController
{

    public override void Fire()
    {
        int projectileCount = Random.Range(10, 15);
        for (int i = 0; i<= projectileCount; i++)
        {
            float plusDirecX = Random.Range(-1f, 1f);
            float plusDirecZ = Random.Range(-1f, 1f);
            //добавляем разброс
            Vector3 direction = new Vector3(directionFire.position.x + plusDirecX, directionFire.position.y, directionFire.position.z + plusDirecZ);
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = fireTrans.position;
            projGO.GetComponent<Rigidbody>().velocity = (direction - fireTrans.position).normalized * (30 + Random.Range(0,15));
        }
        base.Fire();
    }
}
