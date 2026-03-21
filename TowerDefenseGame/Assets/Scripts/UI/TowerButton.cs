using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab; 

    private Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void OnClick()
    {
        UIManager.Instance.OnTowerButtonClicked(towerPrefab, myImage);
    }
}
