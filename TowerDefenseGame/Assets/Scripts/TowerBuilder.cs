using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask towerLayer;
    [SerializeField] private float placeDist = 0.5f;
    
    private GameObject towerToBuild;
    private int selectedTowerCost;


    [SerializeField] private GameObject buildCursor;


    public void Update()
    {
        if (towerToBuild != null && TryPlace(out Vector3 hitPos))
        {
            buildCursor.SetActive(true);
            buildCursor.transform.position = hitPos;
            buildCursor.transform.localScale = new Vector3(placeDist, placeDist, placeDist);

            if (Input.GetMouseButtonDown(0))
            {
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
        selectedTowerCost = selectedTower.GetComponent<TowerBase>().cost;
    }

    public bool TryPlace(out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        if (GameManager.Instance.CanAfford(selectedTowerCost)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit, 100f, groundLayer))
            {
                Collider[] otherTowers = Physics.OverlapSphere(hit.point, placeDist, towerLayer);

                if (otherTowers.Length == 0)
                {
                    hitPoint = hit.point;
                    return true;
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
