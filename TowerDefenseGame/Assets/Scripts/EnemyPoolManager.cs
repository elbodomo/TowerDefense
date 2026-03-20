using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    public Transform[] Waypoints;

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

    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("Spawn Light Enemy")]
    public void spawnLightEnemy()
    {
        LightEnemyPool[0].SetActive(true);
        ActiveLightEnemyPool.Add(LightEnemyPool[0]);
        LightEnemyPool.RemoveAt(0);
    }
    public void despawnEnemy(Enemy enemy)
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
