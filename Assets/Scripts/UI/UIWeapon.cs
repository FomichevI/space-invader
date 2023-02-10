using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWeapon : MonoBehaviour
{
    [SerializeField] private Image fillImg;
    [SerializeField] private TextMeshProUGUI bulletsText;
    private float currentCDTime;
    private float CDTime;

    public void Fire(int bulletsLeft, float cdTime)
    {
        CDTime = cdTime;
        currentCDTime = CDTime;
        RefreshBullets(bulletsLeft);
    }

    public void RefreshBullets(int bullets)
    {
        bulletsText.text = bullets.ToString();
    }

    private void FixedUpdate()
    {
        if (currentCDTime / CDTime > 0)
        {
            currentCDTime -= 0.02f;
            fillImg.fillAmount = 1 - currentCDTime / CDTime;
        }
        else
            fillImg.fillAmount = 1;
    }

    public void InactiveWeapon()
    {
        fillImg.gameObject.SetActive(false);
    }
    public void ActiveWeapon()
    {
        fillImg.gameObject.SetActive(true);
    }
}
