using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints;
    [SerializeField] private Renderer rend;//для монстров
    [SerializeField] private Material mat;//для персонажа
    [SerializeField] private bool isPlayer = false;

    //переменные для отображения полоски здоровья
    [SerializeField] private bool showHpBar = true;
    [SerializeField] private Transform hpBarTrans;
    [SerializeField] private Vector3 hpBarOffset;
    [SerializeField] private Image hpBarFill;
    private float maxHp;

    private float showDamageTime = 0.2f;
    private float currentShowDamageTime;
    private bool underDamage;

    private void Start()
    {
        if (!isPlayer)
        {
            rend.material.color = Color.white;
            maxHp = hitPoints;
        }
        else
        {
            mat.color = Color.white;
        }
    }

    private void Update()
    {
        if (showHpBar)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + hpBarOffset);
            if (hpBarTrans.position != pos)
                hpBarTrans.position = pos;
        }
    }


    public void GetDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            if (!isPlayer)
            {
                GetComponent<EnemyController>().Death();
                MonsterSpawner.S.MonsterDead();
            }
            else
                PlayerController.S.Death();
        }

        if (!isPlayer)
        {
            rend.material.color = Color.red;
            hpBarFill.fillAmount = hitPoints / maxHp;
        }
        else
        {
            mat.color = Color.red;
        }
        underDamage = true;
        currentShowDamageTime = showDamageTime;
    }

    public void AddHP(int value)
    {
        hitPoints += value;
    }


    private void FixedUpdate()
    {
        if (underDamage)
        {
            if (currentShowDamageTime > 0)
                currentShowDamageTime -= 0.02f;
            else
            {
                underDamage = false;
                if (!isPlayer)
                    rend.material.color = Color.white;
                else
                    mat.color = Color.white;
            }
        }
    }

    public int GetHP()
    {
        return hitPoints;
    }

}
