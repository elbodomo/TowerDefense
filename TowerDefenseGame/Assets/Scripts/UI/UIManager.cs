using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TowerBuilder towerBuilder;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.green;

    private Image curSelectedButtonImage;

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

    public void ResetButtonColors()
    {
        if(curSelectedButtonImage != null)
        {
            curSelectedButtonImage.color = normalColor;
            curSelectedButtonImage = null;
        }
    }
}
