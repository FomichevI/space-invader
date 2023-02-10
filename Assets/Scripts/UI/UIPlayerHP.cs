using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHP : MonoBehaviour
{
    public static UIPlayerHP S;
    [SerializeField] private GameObject[] playerHP;

    private void Awake()
    {
        if (S == null)
            S = this;
    }

    public void ShowHP(int currentHP)
    {
        foreach (GameObject go in playerHP)
            go.SetActive(false);
        for (int i = 0; i < currentHP; i++)
            playerHP[i].SetActive(true);
    }

}
