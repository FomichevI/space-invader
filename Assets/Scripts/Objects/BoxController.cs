using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBoxType { bullet, health}
public class BoxController : MonoBehaviour
{
    [SerializeField] private eBoxType boxType;

    private void OnTriggerEnter(Collider other)
    {
        if (boxType == eBoxType.bullet)
            GameManager.S.AddBullets();
        else if (boxType == eBoxType.health)
            GameManager.S.AddOneHpToPlayer();
        Destroy(gameObject);
        BoxSpawner.S.PickUpBox(boxType, transform.position);
    }
}
