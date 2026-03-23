using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enviromentLayer;

    [SerializeField] private LayerMask towerLayer;
    [SerializeField] private float placeDist = 0.5f;
    
    private GameObject towerToBuild;
    private int selectedTowerCost;


    [SerializeField] private GameObject buildCursor;
    [SerializeField] private GameObject CursorCenter;
    [SerializeField] private GameObject CursorRange;


    public void Update()
    {
        if (towerToBuild != null && TryPlace(out Vector3 hitPos))
        {
            float towerRange = towerToBuild.GetComponent<TowerBase>().range;
            buildCursor.SetActive(true);
            buildCursor.transform.position = hitPos;
            CursorCenter.transform.localScale = new Vector3(placeDist * 2, placeDist * 2, placeDist * 2);
            CursorRange.transform.localScale = new Vector3(towerRange, towerRange, towerRange);
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                PlaceTower(hitPos);
            }
        }
        else
        {
            buildCursor.SetActive(false);
        }
    }

    public void SelectTowerToBuild(GameObject selectedTower)
    {
        towerToBuild = selectedTower;
        if (towerToBuild != null)
        {
            selectedTowerCost = selectedTower.GetComponent<TowerBase>().cost;

        }
    }

    public bool TryPlace(out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        if (GameManager.Instance.CanAfford(selectedTowerCost)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit, 100f, enviromentLayer))
            {

                if (hit.transform.gameObject.layer == 7)
                {
                
                Collider[] otherTowers = Physics.OverlapSphere(hit.point, placeDist, towerLayer);

                if (otherTowers.Length == 0)
                {
                    hitPoint = hit.point;
                    return true;
                }
                }
            }
        }

        return false;
    }
    private void PlaceTower(Vector3 postion)
    {
        Instantiate(towerToBuild, postion, Quaternion.identity);
        GameManager.Instance.RemoveMoney(selectedTowerCost);
    }
}
