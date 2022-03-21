using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public string WaveName;
    public int TotalofEnemy;
    public GameObject[] EnemyType;
    public int[] NumberofEnemyType;
    public Transform[] SpawnPoint;
}

public class ESpawnManager : MonoBehaviour
{
    [SerializeField]
    Wave[] waves;

    [SerializeField] private float NextWaveWait = 10;//次にwaveが始まるまでの時間

    private Wave CurrentWave;
    private int CurrentWaveNumber = 0;

    private bool CanSpawn = true;

    private GameObject[] TotalEnemy;

    private int SpawnEnemyCount = 0 ;

    [SerializeField] private GameObject ClearText;

    [SerializeField] private AudioSource AS;

    [SerializeField] private AudioClip fightvc0;

    [SerializeField] private AudioClip fightvc1;

    [SerializeField] private AudioClip fightvc2;

    [SerializeField] private AudioClip fightvc3;

    void Start()
    {
        WaveStart(10);
        ClearText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {//Enemyの数を確認しwaveが終わっているかどうか判断
        TotalEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (TotalEnemy.Length < 1 && !CanSpawn)
        {
            Debug.Log("WaveClear");
            CanSpawn = true;
            CurrentWaveNumber++;
            SelectVc();//ランダムでボイス
            if (CurrentWaveNumber < 5)
            {
                CurrentWave = waves[CurrentWaveNumber];
                StartCoroutine(StartSpawn());
            }
            else
            {
                Debug.Log("StageClear!!");
                ClearText.SetActive(true);
                //ステージクリア後の処理
            }
        }

    }

    public void WaveStart(float WaitTime)
    {
        CurrentWave = waves[CurrentWaveNumber];
        StartCoroutine(FirstWave(WaitTime));
    }

    void SpawnEnemy()
    {
        Debug.Log(CurrentWaveNumber);
        if (CanSpawn)
        {
            for (int i = 0; i < CurrentWave.NumberofEnemyType.Length; i++)
            {
                for (int j = 0; j < CurrentWave.NumberofEnemyType[i]  ; j++)
                {
                    Instantiate(CurrentWave.EnemyType[i], CurrentWave.SpawnPoint[SpawnEnemyCount].position,CurrentWave.SpawnPoint[SpawnEnemyCount].rotation);
                    SpawnEnemyCount++;
                }
            }
            SpawnEnemyCount = 0;
            CanSpawn = false;
        }
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(NextWaveWait);
        SpawnEnemy();
    }

    IEnumerator FirstWave(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SpawnEnemy();
    }

    private void SelectVc()
    {
        int i = Random.Range(0, 4);
        switch(i)
        {
            case 0:
                AS.PlayOneShot(fightvc0);
                break;
            case 1:
                AS.PlayOneShot(fightvc1);
                break;
            case 2:
                AS.PlayOneShot(fightvc2);
                break;
            case 3:
                AS.PlayOneShot(fightvc3);
                break;
        }
    }

}
