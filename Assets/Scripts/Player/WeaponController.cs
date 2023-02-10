using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float collDown = 1;
    public weaponsTypes type;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] private GameObject effect;

    [SerializeField] protected Transform fireTrans;
    [SerializeField] protected Transform directionFire;


    public virtual void Fire()
    {
        effect.SetActive(true);
        Invoke("SwitchOffEffect", 0.2f);
    }

    private void SwitchOffEffect()
    {
        effect.SetActive(false);
    }

}
