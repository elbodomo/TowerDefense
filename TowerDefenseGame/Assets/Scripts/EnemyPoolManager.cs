using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    public Transform[] Waypoints;

    [SerializeField] private GameObject LightEnemy;
    [SerializeField] private GameObject HeavyEnemy;

    [SerializeField] private int LightEnemyCount = 50;
    [SerializeField] private int HeavyEnemyCount = 25;

    [SerializeField] private List<GameObject> LightEnemyPool = new List<GameObject>();
    [SerializeField] private List<GameObject> HeavyEnemyPool = new List<GameObject>();

    [SerializeField] private List<GameObject> ActiveLightEnemyPool = new List<GameObject>();
    [SerializeField] private List<GameObject> ActiveHeavyEnemyPool = new List<GameObject>();

    private void Awake()
    {
        //Singleton Implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePool();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitializePool()
    {
        for (int i = 0; i < LightEnemyCount; i++)
        {
            GameObject enemy = Instantiate(LightEnemy, transform.position, Quaternion.identity, transform);
            enemy.SetActive(false);
            LightEnemyPool.Add(enemy);
        }

        for (int i = 0; i < HeavyEnemyCount; i++)
        {
            GameObject enemy = Instantiate(HeavyEnemy, transform.position, Quaternion.identity, transform);
            enemy.SetActive(false);
            HeavyEnemyPool.Add(enemy);
        }
    }

    [ContextMenu("Spawn Light Enemy")]
    public void SpawnLightEnemy()
    {
        LightEnemyPool[0].SetActive(true);
        ActiveLightEnemyPool.Add(LightEnemyPool[0]);
        LightEnemyPool.RemoveAt(0);
    }
    [ContextMenu("Spawn Heavy Enemy")]
    public void SpawnHeavyEnemy()
    {
        HeavyEnemyPool[0].SetActive(true);
        ActiveHeavyEnemyPool.Add(HeavyEnemyPool[0]);
        HeavyEnemyPool.RemoveAt(0);
    }
    public void DespawnEnemy(Enemy enemy)
    {
        switch (enemy.myType)
        {
            case EnemyType.Light:
                LightEnemyPool.Add(enemy.gameObject);
                ActiveLightEnemyPool.Remove(enemy.gameObject);
                break;
            case EnemyType.Heavy:
                HeavyEnemyPool.Add(enemy.gameObject);
                ActiveHeavyEnemyPool.Remove(enemy.gameObject);
                break;
        }
    }
}
