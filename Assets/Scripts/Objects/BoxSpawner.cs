using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public static BoxSpawner S;

    [SerializeField] private GameObject healBoxPrefab;
    [SerializeField] private GameObject bulletBoxPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool[] activeSpawnPoints;

    [SerializeField] private int maxNumberHealBoxOnLevel = 2;
    [SerializeField] private int maxNumberBulletBoxOnLevel = 2;
    private int currentNumberHealBox = 0;
    private int currentNumberBulletBox = 0;

    [SerializeField] private float maxTimeLagBetweenHealBoxes = 10;
    [SerializeField] private float maxTimeLagBetweenBulletBoxes = 10;
    private float currentTimeLagBetweenHealBoxes;
    private float currentTimeLagBetweenBulletBoxes;

    private void Start()
    {
        if (S == null)
            S = this;
        activeSpawnPoints = new bool[spawnPoints.Length];

        for (int i = 0; i < activeSpawnPoints.Length; i++)
            activeSpawnPoints[i] = true;
    }

    private void FixedUpdate()
    {
        if (currentNumberHealBox < maxNumberHealBoxOnLevel)
        {
            if (currentTimeLagBetweenHealBoxes > 0)
                currentTimeLagBetweenHealBoxes -= 0.02f;
            else
            {
                SpawnHillBox();
                currentTimeLagBetweenHealBoxes = maxTimeLagBetweenHealBoxes;
            }
        }

        if (currentNumberBulletBox < maxNumberBulletBoxOnLevel)
        {
            if (currentTimeLagBetweenBulletBoxes > 0)
                currentTimeLagBetweenBulletBoxes -= 0.02f;
            else
            {
                SpawnBulletBox();
                currentTimeLagBetweenBulletBoxes = maxTimeLagBetweenBulletBoxes;
            }
        }
    }

    public void PickUpBox(eBoxType type, Vector3 boxPosition)
    {
        if (type == eBoxType.health)
            currentNumberHealBox--;
        else if (type == eBoxType.bullet)
            currentNumberBulletBox--;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].position == boxPosition)
                activeSpawnPoints[i] = true;
        }

    }

    private void SpawnHillBox()
    {
        int spawnPosNumber;
        while (true)
        {
            spawnPosNumber = Random.Range(0, spawnPoints.Length);
            if (activeSpawnPoints[spawnPosNumber])
                break;
        }
        GameObject monsterGO = Instantiate(healBoxPrefab, spawnPoints[spawnPosNumber].position, Quaternion.Euler(Vector3.zero));
        currentNumberHealBox++;
        activeSpawnPoints[spawnPosNumber] = false;
        //Debug.Log("hill");
    }
    private void SpawnBulletBox()
    {
        int spawnPosNumber;
        while (true)
        {
            spawnPosNumber = Random.Range(0, spawnPoints.Length);
            if (activeSpawnPoints[spawnPosNumber])
                break;
        }
        GameObject monsterGO = Instantiate(bulletBoxPrefab, spawnPoints[spawnPosNumber].position, Quaternion.Euler(Vector3.zero));
        currentNumberBulletBox++;
        activeSpawnPoints[spawnPosNumber] = false;
        //Debug.Log("bull");
    }
}
