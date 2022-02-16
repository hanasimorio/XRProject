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

    [SerializeField] private float NextWaveWait = 10;//����wave���n�܂�܂ł̎���

    private Wave CurrentWave;
    private int CurrentWaveNumber = 0;

    private bool CanSpawn = true;

    private GameObject[] TotalEnemy;

    private int SpawnEnemyCount = 0 ;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {//Enemy�̐����m�F��wave���I����Ă��邩�ǂ������f
        TotalEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (TotalEnemy.Length < 1 && !CanSpawn)
        {
            Debug.Log("WaveClear");
            CanSpawn = true;
            CurrentWaveNumber++;
            if (CurrentWaveNumber < 5)
            {
                CurrentWave = waves[CurrentWaveNumber];
                StartCoroutine(StartSpawn());
            }
            else
            {
                Debug.Log("StageClear!!");
                //�X�e�[�W�N���A��̏���
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


}
