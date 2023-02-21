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
            EventAggregator.AddBullets.Invoke();
        else if (boxType == eBoxType.health)
            EventAggregator.HealPlayer.Invoke();
        Destroy(gameObject);
        BoxSpawner.S.PickUpBox(boxType, transform.position);
    }
}
