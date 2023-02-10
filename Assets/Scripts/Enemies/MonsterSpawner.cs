using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner S;

    [SerializeField] private GameObject[] monstersPrefabs; //�� ������ �������� � ������ ����������
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float maxTimeLagBetweenMonsters = 1;
    private float currentTimeLagBetweenMonsters;

    private int currentWaveNumber = 0;
    private int currentMonstersOnWave = 0; //���������� ��������, ������� ������ ��������� �� ������ �����
    private int currentMonsteraAllive = 0;

    private bool waveSpawning = false;

    private void Start()
    {
        if (S == null)
            S = this;
    }

    private void FixedUpdate()
    {
        //����� �������� ���������� � ������� ��������� �� ������, ���� �� �������� ����� ���������� �������� �� ������ �����
        //����� ����� ���������� ������ ����� ����������� ���� �������� �� ����������
        if (waveSpawning)
        {
            if (currentMonstersOnWave != 0)
            {
                if (currentTimeLagBetweenMonsters > 0)
                    currentTimeLagBetweenMonsters -= 0.02f;
                else
                {
                    SpawnMonster();
                    currentMonstersOnWave -= 1;
                    currentTimeLagBetweenMonsters = maxTimeLagBetweenMonsters;
                }
            }
            else
            {
                if (currentWaveNumber % 5 == 0)
                {
                    SpawnBoss();
                    UIPanelsController.S.ShowBossText();
                    UIAudioManager.S.PlayDanger();
                }
                waveSpawning = false;
            }
        }

        else
        {
            if (currentMonsteraAllive <= 0)
            {
                currentWaveNumber++;
                Debug.Log("Wave " + currentWaveNumber + " !");
                waveSpawning = true;
                currentMonstersOnWave = 3 + currentWaveNumber - 1;
                UIPanelsController.S.ShowNewWaveText(currentWaveNumber);
                if (currentWaveNumber > 1)
                    UIAudioManager.S.PlayComplete();
            }
        }
    }

    public void MonsterDead()
    {
        currentMonsteraAllive -= 1;
    }



    private void SpawnMonster()
    {
        int spawnPosNumber = Random.Range(0, spawnPoints.Length);
        int chanceMonsterSpawn = Random.Range(1, 101);
        //������� ������� � ������� ���� ����� ���������� � ������ 35%, 3 ���� - 20%, 4 ���� - 10%
        GameObject currentMonster;
        if (chanceMonsterSpawn <= 35)
            currentMonster = monstersPrefabs[0];
        else if (chanceMonsterSpawn <= 70)
            currentMonster = monstersPrefabs[1];
        else if (chanceMonsterSpawn <= 90)
            currentMonster = monstersPrefabs[2];
        else
            currentMonster = monstersPrefabs[3];
        GameObject monsterGO = Instantiate(currentMonster, spawnPoints[spawnPosNumber].position, Quaternion.Euler(Vector3.zero));
        currentMonsteraAllive += 1;
    }
    private void SpawnBoss()
    {
        int spawnPosNumber = Random.Range(0, spawnPoints.Length);
        GameObject bossGO = Instantiate(bossPrefab, spawnPoints[spawnPosNumber].position, Quaternion.Euler(Vector3.zero));
        currentMonsteraAllive += 1;
    }
}
