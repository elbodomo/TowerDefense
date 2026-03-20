using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    //Game Variables
    public int GameSpeed = 1;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //helper Functions
    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void RemoveMoney(int amount)
    {
        money -= amount;
    }
    public bool CanAfford(int amount)
    {
        return money >= amount;
    }
    public void AddHealth(int amount)
    {
        health += amount;
    }
    public void RemoveHealth(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            //Game over
        }
    }
}
