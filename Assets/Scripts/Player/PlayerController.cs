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

    private PlayerAudioManager audioManager;
    private PlayerAnimator playerAnimator;
    private Rigidbody rb;
    private Health health;

    private float yRot; //параметр, необходимый для определения направления движения персонажа

    //параметры для стрельбы
    private float currentCd;
    private bool canMove = true; //при выстреле из дробовика или винтовки игрок не может двигаться
    private float shotgunRecoil = 10000;

    //параметры для имуна к урону после удара
    private float immunityAfterHit = 1f;
    private bool isImmunity = false;


    void Awake()
    {
        if (S == null)
            S = this;

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();
        health = GetComponent<Health>();
        audioManager = GetComponent<PlayerAudioManager>();

    UIPlayerHP.S.ShowHP(health.GetHP());
    }
    private void OnEnable()
    {
        EventAggregator.HealPlayer.AddListener(AddHp);
        EventAggregator.MovePlayer.AddListener(Move);
        EventAggregator.StorPlayer.AddListener(StopMove);
        EventAggregator.Fire.AddListener(Fire);
        EventAggregator.SetWeapon.AddListener(SetWeapon);
    }
    private void OnDisable()
    {
        EventAggregator.HealPlayer.RemoveListener(AddHp);
        EventAggregator.MovePlayer.RemoveListener(Move);
        EventAggregator.StorPlayer.RemoveListener(StopMove);
        EventAggregator.Fire.RemoveListener(Fire);
        EventAggregator.SetWeapon.RemoveListener(SetWeapon);
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
            audioManager.PlayMove();
            //активируем движение тут, чтобы избежать возможность передвижения во время стрельбы из винтовки
            if (currentCd <= 0)
                canMove = true;
        }
    }

    public void StopMove()
    {
        audioManager.StopMove();
    }

    private void FixedUpdate()
    {
        yRot = transform.rotation.y;

        if (currentCd > 0)
            currentCd -= 0.02f;        
    }

    public void Fire()
    {
        if (isAlive && currentCd <= 0)
        {
            if (GameManager.S.GetCurrentBullets(currentWeaponIndex) > 0) //проверка на наличие патронов
            {
                playerAnimator.StartFire();
                currentCd = weapons[currentWeaponIndex].collDown;
                if (weapons[currentWeaponIndex].type == weaponsTypes.rifle) //во время трельбы из винтовки передвигаться нельзя
                {
                    canMove = false;
                    playerAnimator.AnimateMove(Vector3.zero, yRot); //остонавливаем анимацию передвижения
                    audioManager.PlayRifle();
                }
                else if (weapons[currentWeaponIndex].type == weaponsTypes.shotgun) //стрельба из дробовика добавляет отдачу
                {
                    rb.AddForce((transform.position - PlayerInput.S.GetLookPos()).normalized * shotgunRecoil);
                    audioManager.PlayShotgun();
                }
                else
                {
                    audioManager.PlayPistol();
                }

                //сам выстрел снарядом
                weapons[currentWeaponIndex].Fire();
                //отображение выстрела на UI
                GameManager.S.Fire(currentWeaponIndex, currentCd);
            }
            else //сообщение о недостаточном количестве патронов
            {
                audioManager.PlayMisfire();
            }
        }
    }

    public void SetWeapon(int weaponIndex)
    {
        if (currentCd <= 0)
        {
            if (weaponIndex >= 0 && weaponIndex < weapons.Length)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].gameObject.SetActive(false);
                }
                weapons[weaponIndex].gameObject.SetActive(true);
                currentWeaponIndex = weaponIndex;
                audioManager.PlayChangeWeapon();
            }
        }
    }

    public void SetNextWeapon()
    {
        if (currentWeaponIndex < weapons.Length - 1)
            currentWeaponIndex += 1;
        else
            currentWeaponIndex = 0;
        EventAggregator.SetWeapon.Invoke(currentWeaponIndex);
    }

    public void SetPreviousWeapon()
    {
        if (currentWeaponIndex > 0)
            currentWeaponIndex -= 1;
        else
            currentWeaponIndex = weapons.Length - 1;
        EventAggregator.SetWeapon.Invoke(currentWeaponIndex);
    }

    public void Death()
    {
        playerAnimator.Death();
        isAlive = false;
        PlayerInput.S.canMove = false;
        Invoke("ShowGameOver", 1);
        audioManager.PlayDeath();
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
            audioManager.PlayGetDamage();
        }
    }

    public void AddHp()
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
