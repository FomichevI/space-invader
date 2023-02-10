using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    //настройки пастронов
    [SerializeField] private int[] maxBullets;
    private int[] currentBullets;
    [SerializeField] private UIWeapon[] uiWeapons;
    [SerializeField] private int[] addingBullets;



    void Awake()
    {
        Application.targetFrameRate = 60;

        if (S == null)
            S = this;

        currentBullets = new int[maxBullets.Length];
        for (int i = 0; i < maxBullets.Length; i++)
        {
            currentBullets[i] = maxBullets[i];
            uiWeapons[i].RefreshBullets(currentBullets[i]);
        }
    }

    public void Fire(int weaponIndex, float cd)
    {
        currentBullets[weaponIndex] -= 1;
        uiWeapons[weaponIndex].Fire(currentBullets[weaponIndex], cd);
    }

    public int GetCurrentBullets(int weaponIndex)
    {
        return currentBullets[weaponIndex];
    }

    public void AddBullets()
    {
        for (int i = 0; i < currentBullets.Length; i++)
        {
            currentBullets[i] += addingBullets[i];
            if (currentBullets[i] > maxBullets[i])
                currentBullets[i] = maxBullets[i];
            uiWeapons[i].RefreshBullets(currentBullets[i]);
        }
        UIAudioManager.S.PlayRecharge();
    }

    public void AddOneHpToPlayer()
    {
        PlayerController.S.AddHP();
        UIAudioManager.S.PlayHeal();
    }

    public void InactiveWeapon(int weaponIndex)
    {
        uiWeapons[weaponIndex].InactiveWeapon();
    }
    public void ActiveWeapon(int weaponIndex)
    {
        uiWeapons[weaponIndex].ActiveWeapon();
    }

}
