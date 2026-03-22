using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TowerBuilder towerBuilder;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.green;

    private Image curSelectedButtonImage;
    [SerializeField] private TMP_Text WaveCounter;

    private bool isGameStartedUI = false;
    [SerializeField] private TMP_Text startStopButtonText;


    [SerializeField] private TMP_Text MoneyUIText;
    [SerializeField] private TMP_Text HealthUIText;

    [SerializeField] private TMP_Text speedMultiplierButtonText;
    private int[] speedMultipliers = { 1, 2, 4 };
    private int currentMultiplierIndex = 0;

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
    public void ToggleStartStop()
    {
        isGameStartedUI = !isGameStartedUI;

        if (isGameStartedUI)
        {
            startStopButtonText.text = "Stop";
            GameManager.Instance.StartGame();
        }
        else
        {
            startStopButtonText.text = "Start";
            GameManager.Instance.StopGame();
        }
    }
    public void ToggleSpeedMultiplier()
    {
        currentMultiplierIndex = (currentMultiplierIndex + 1) % speedMultipliers.Length;
        int newMultiplier = speedMultipliers[currentMultiplierIndex];

        if (speedMultiplierButtonText != null)
        {
            speedMultiplierButtonText.text = "x" + newMultiplier.ToString();
        }

        GameManager.Instance.SetSpeedMultiplier(newMultiplier);
    }
    public void OnTowerButtonClicked(GameObject towerPrefab, Image clickedButtonImage)
    {
        if (curSelectedButtonImage == clickedButtonImage) 
        { 
            ResetButtonColors();
            towerBuilder.SelectTowerToBuild(null);
            return;
        }
        ResetButtonColors();

        curSelectedButtonImage = clickedButtonImage;
        curSelectedButtonImage.color = selectedColor;

        towerBuilder.SelectTowerToBuild(towerPrefab);

    }
    public void SetWaveCompleteUI()
    {
        isGameStartedUI = false;
        if (startStopButtonText != null)
        {
            startStopButtonText.text = "Start";
        }
    }
    public void SetMoneyUI(int amount)
    {
        if (MoneyUIText != null)
        {
            MoneyUIText.text = amount.ToString();
        }
    }
    public void SetHealthUI(int amount)
    {
        if (HealthUIText != null)
        {
            HealthUIText.text = amount.ToString();
        }
    }

    public void ResetButtonColors()
    {
        if(curSelectedButtonImage != null)
        {
            curSelectedButtonImage.color = normalColor;
            curSelectedButtonImage = null;
        }
    }

    public void UpdateWaveCounter(int CurrentWave, int TotalWaves)
    {
        WaveCounter.text =(CurrentWave.ToString() + " / "  +TotalWaves.ToString());
    }
}
