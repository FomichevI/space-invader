 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponsTypes { pistol, rifle, shotgun }

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerAudioManager))]

public class PlayerController : MonoBehaviour
{
    public static PlayerController S;

    public bool isAlive = true;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float maxMoveSpeed = 10;
    [SerializeField] private WeaponController[] weapons;

    private int currentWeaponIndex = 0;

    private PlayerAnimator playerAnimator;
    private Rigidbody rb;
    private Health health;

    private float yRot; //��������, ����������� ��� ����������� ����������� �������� ���������

    //��������� ��� ��������
    private float currentCD;
    private bool canMove = true; //��� �������� �� ��������� ��� �������� ����� �� ����� ���������
    private float shotgunRecoil = 10000;

    //��������� ��� ����� � ����� ����� �����
    private float immunityAfterHit = 1f;
    private bool isImmunity = false;


    void Awake()
    {
        if (S == null)
            S = this;

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
        health = GetComponent<Health>();

        UIPlayerHP.S.ShowHP(health.GetHP());
    }

    public void Move(Vector3 direction)
    {
        if (isAlive)
        {
            if (canMove)
            {
                if (rb.velocity.magnitude < maxMoveSpeed)
                    rb.AddForce(direction * moveSpeed);

                playerAnimator.AnimateMove(direction, yRot);
            }

            //���������� �������� ���, ����� �������� ����������� ������������ �� ����� �������� �� ��������
            if (currentCD <= 0)
                canMove = true;
        }
    }

    //�� ��������
    //public void Rotate(Vector3 direction)
    //{
    //    rb.AddTorque(Vector3.Cross(transform.forward, direction) * 2);
    //}

    private void FixedUpdate()
    {
        yRot = transform.rotation.y;

        if (currentCD > 0)
            currentCD -= 0.02f;        
    }

    public void Fire()
    {
        if (isAlive && currentCD <= 0)
        {
            if (GameManager.S.GetCurrentBullets(currentWeaponIndex) > 0) //�������� �� ������� ��������
            {
                playerAnimator.StartFire();
                currentCD = weapons[currentWeaponIndex].collDown;

                if (weapons[currentWeaponIndex].type == weaponsTypes.rifle) //�� ����� ������� �� �������� ������������� ������
                {
                    canMove = false;
                    playerAnimator.AnimateMove(Vector3.zero, yRot); //������������� �������� ������������
                    PlayerAudioManager.S.PlayRifle();
                }
                else if (weapons[currentWeaponIndex].type == weaponsTypes.shotgun) //�������� �� ��������� ��������� ������
                {
                    rb.AddForce((transform.position - PlayerInput.S.GetLookPos()).normalized * shotgunRecoil);
                    PlayerAudioManager.S.PlayShotgun();
                }
                else
                    PlayerAudioManager.S.PlayPistol();

                //��� ������� ��������
                weapons[currentWeaponIndex].Fire();
                //����������� �������� �� UI
                GameManager.S.Fire(currentWeaponIndex, currentCD);
            }
            else //��������� � ������������� ���������� ��������
            {
                PlayerAudioManager.S.PlayMisfire();
            }
        }
    }

    public void SetWeapon(int weaponIndex)
    {
        if (currentCD <= 0)
        {
            if (weaponIndex >= 0 && weaponIndex < weapons.Length)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].gameObject.SetActive(false);
                    GameManager.S.InactiveWeapon(i); //����������� ������ ���������� ������
                }
                weapons[weaponIndex].gameObject.SetActive(true);
                currentWeaponIndex = weaponIndex;
                GameManager.S.ActiveWeapon(currentWeaponIndex);
                PlayerAudioManager.S.PlayChangeWeapon();
            }
        }
    }

    public void SetNextWeapon()
    {
        if (currentCD <= 0)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            GameManager.S.InactiveWeapon(currentWeaponIndex);

            if (currentWeaponIndex < weapons.Length - 1)
                currentWeaponIndex += 1;
            else
                currentWeaponIndex = 0;

            weapons[currentWeaponIndex].gameObject.SetActive(true);
            GameManager.S.ActiveWeapon(currentWeaponIndex);
            PlayerAudioManager.S.PlayChangeWeapon();
        }
    }

    public void SetPreviousWeapon()
    {
        if (currentCD <= 0)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
            GameManager.S.InactiveWeapon(currentWeaponIndex);

            if (currentWeaponIndex > 0)
                currentWeaponIndex -= 1;
            else
                currentWeaponIndex = weapons.Length - 1;

            weapons[currentWeaponIndex].gameObject.SetActive(true);
            GameManager.S.ActiveWeapon(currentWeaponIndex);
            PlayerAudioManager.S.PlayChangeWeapon();
        }
    }

    public void Death()
    {
        playerAnimator.Death();
        isAlive = false;
        PlayerInput.S.canMove = false;
        Invoke("ShowGameOver", 1);
        PlayerAudioManager.S.PlayDeath();
    }

    private void ShowGameOver()
    {
        UIPanelsController.S.ShowGameOverPanel();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 && !isImmunity)
        {
            health.GetDamage(1);
            UIPlayerHP.S.ShowHP(health.GetHP());
            isImmunity = true;
            Invoke("StopImmunity", immunityAfterHit);
            PlayerAudioManager.S.PlayGetDamage();
        }
    }

    public void AddHP()
    {
        if(health.GetHP() < 3)
        {
            health.AddHP(1);
            UIPlayerHP.S.ShowHP(health.GetHP());
        }
    }

    private void StopImmunity()
    {
        isImmunity = false;
    }

}
