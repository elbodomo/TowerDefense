using System;
using System.Collections;
using UnityEngine;

// data structure in json to save and switch wave structure

[Serializable]
public class WaveContainer
{
    public Wave[] waves;
}

[Serializable]
public class Wave
{
    public WaveAction[] actions;
}
[Serializable]
public class WaveAction
{
    public string actionType; // break or spawn
    public int enemyIndex;  //0 = light, 1 heavy
    public int amount; //how many
    public float interval;  //time between spawns
    public float waitDuration; // time to wait before frist spawn
}


public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField] private TextAsset jsonWaveFile;

    private WaveContainer waveData;
    private int currentWaveIndex = 0;
    private bool isWaveRunning = false;

    private int activeEnemies = 0;
    private bool doneSpawning = false;
    private void Awake()
    {
        //Singleton Implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadJsonData();
    }

    private void LoadJsonData()
    {
        if (jsonWaveFile != null)
        {
            waveData = JsonUtility.FromJson<WaveContainer>(jsonWaveFile.text);
            Debug.Log(waveData.waves.Length + " waves loaded");
        }
        else
        {
            Debug.Log("Wave File not found");
        }
    }

    [ContextMenu("StartNextwave")]
    public void StartNextwave()
    {
        if (isWaveRunning || waveData == null) return;

        if(currentWaveIndex >= waveData.waves.Length)
        {
            Debug.Log("all waves Completed");
            return;
        }
        StartCoroutine(PlayWaveRoutine(waveData.waves[currentWaveIndex]));
    }
    private IEnumerator PlayWaveRoutine(Wave wave)
    {
        isWaveRunning = true;
        doneSpawning = false;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateWaveCounter(currentWaveIndex + 1, waveData.waves.Length);
        }

        foreach (WaveAction action in wave.actions)
        {
            string type = action.actionType.ToLower();

            if(type == "break")
            {
                float waitTimer = action.waitDuration;
                while (waitTimer > 0)
                {
                    waitTimer -= Time.deltaTime * GameManager.Instance.GameSpeed;
                    yield return null;
                }
            }else if (type == "spawn")
            {
                for ( int i = 0; i<action.amount; i++)
                {
                    if (action.enemyIndex == 0)
                    {
                        EnemyPoolManager.Instance.SpawnLightEnemy();
                    }
                    else if (action.enemyIndex == 1)
                    {
                        EnemyPoolManager.Instance.SpawnHeavyEnemy();
                    }


                    float intervalTimer = action.interval;
                    while (intervalTimer > 0)
                    {
                        intervalTimer -= Time.deltaTime * GameManager.Instance.GameSpeed;
                        yield return null;
                    }
                }
            }

        }


        doneSpawning = false;
        Debug.Log("All enemys spawned");

    }
    public void EnemyDefeated()
    {
        activeEnemies--;
        CheckWaveCompleted();
    }

    private void CheckWaveCompleted()
    {
        if (doneSpawning && activeEnemies <= 0)
        {
            currentWaveIndex++;
            isWaveRunning = false;
            Debug.Log("Wave fully cleared!");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.StopGame();
            }

            if (UIManager.Instance != null)
            {
                UIManager.Instance.SetWaveCompleteUI();
            }
        }
    }
}
