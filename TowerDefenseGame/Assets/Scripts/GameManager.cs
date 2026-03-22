using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    //Game Variables
    public int SpeedMultiplier = 1;
    public int GameSpeed = 1; 
    private bool isGameRunning = false;

    [SerializeField] private int health = 20;
    [SerializeField] private int money = 100;



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
        UIManager.Instance.SetMoneyUI(money);

        UIManager.Instance.SetHealthUI(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGameRunning = true;
        GameSpeed = 1 * SpeedMultiplier;

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.StartNextwave();
        }
    }

    public void StopGame()
    {
        isGameRunning = false;
        GameSpeed = 0;
    }
    public void SetSpeedMultiplier(int newMultiplier)
    {
        SpeedMultiplier = newMultiplier;
        if (isGameRunning)
        {
            GameSpeed = 1 * SpeedMultiplier;
        }
    }
    //helper Functions
    public void AddMoney(int amount)
    {
        money += amount;

        UIManager.Instance.SetMoneyUI(money);
    }
    public void RemoveMoney(int amount)
    {
        money -= amount;

        UIManager.Instance.SetMoneyUI(money);
    }
    public bool CanAfford(int amount)
    {
        return money >= amount;
    }
    public void AddHealth(int amount)
    {
        health += amount;

        UIManager.Instance.SetHealthUI(health);
    }
    public void RemoveHealth(int amount)
    {
        health -= amount;

        UIManager.Instance.SetHealthUI(health);

        if (health <= 0)
        {
            StopGame();
        }
    }
}
