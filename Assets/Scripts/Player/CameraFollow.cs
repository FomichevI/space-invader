using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 pos;

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        pos = new Vector3(target.position.x, target.position.y, target.position.z);

        if (pos.z >= 23)
            pos.z = 23;
        else if (pos.z <= -37)
            pos.z = -37;
        if (pos.x >= 28)
            pos.x = 28;
        if (pos.x <= -28)
            pos.x = -28;

        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
    }
}
