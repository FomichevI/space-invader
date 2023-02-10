using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 1;
    public int damage = 3;

    public void FixedUpdate()
    {
        if (lifeTime > 0)
            lifeTime -= 0.02f;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            other.GetComponent<Health>().GetDamage(damage);
            Destroy(gameObject);
        }
    }

}
