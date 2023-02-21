using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput S;

    //определение направления движения игрока
    private float horizontalMove;
    private float verticalMove;
    private Vector3 movement;

    //определение позиции мыши
    private Ray ray;
    private RaycastHit hit;
    private Vector3 lookPos;
    [SerializeField] private LayerMask groundLayer;
    public bool canMove = true; //для отключения возможности поворачиваться в режиме паузы

    private float zoom;

    void Start()
    {
        if (S == null)
            S = this;
    }


    void FixedUpdate()
    {
        //перемещение персонажа
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        movement = new Vector3(horizontalMove, 0, verticalMove);
        if (canMove && movement.magnitude != 0)
        {
            EventAggregator.MovePlayer.Invoke(movement);
        }
        else
        {
            EventAggregator.StorPlayer.Invoke();
        }

        //поворот персонажа в сторону позиции мыши
        if (canMove)
        {
            CastRay();
            lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPos);
            //PlayerController.S.Rotate(lookPos);
        }

        //стрельба
        if (Input.GetMouseButton(0))
            EventAggregator.Fire.Invoke();
    }

    private void Update()
    {
        //смена оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EventAggregator.SetWeapon.Invoke(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EventAggregator.SetWeapon.Invoke(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EventAggregator.SetWeapon.Invoke(2);

        zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0)
        {
            if (zoom < 0)
                PlayerController.S.SetPreviousWeapon();
            else
                PlayerController.S.SetNextWeapon();
        }
    }

    void CastRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 100, groundLayer);
    }

    public Vector3 GetLookPos()
    {
        return lookPos;
    }
}
