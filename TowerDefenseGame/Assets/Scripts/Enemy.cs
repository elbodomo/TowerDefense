using UnityEngine;

public enum EnemyType { Light, Heavy }
public class Enemy : MonoBehaviour
{
    public EnemyType myType;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int damageToBase = 1;
    [SerializeField] private int moneyReward = 5;

    private float currentHealth;
    private int nextWaypointIndex;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        nextWaypointIndex = 0;
        transform.position = EnemyPoolManager.Instance.Waypoints[nextWaypointIndex].position;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveAlongPath();
    }
    private void MoveAlongPath()
    {
        if (EnemyPoolManager.Instance.Waypoints == null) return;


        Transform nextWaypoint = EnemyPoolManager.Instance.Waypoints[nextWaypointIndex];
        Vector3 direction = (nextWaypoint.position - transform.position).normalized;

        transform.position += direction * moveSpeed * GameManager.Instance.GameSpeed* Time.deltaTime;
        transform.LookAt(nextWaypoint.position);

        if(Vector3.Distance(transform.position, nextWaypoint.position)<= 0.1f)
        {
            nextWaypointIndex++;

            if(nextWaypointIndex >= EnemyPoolManager.Instance.Waypoints.Length)
            {
                //base reached
                Deactivate();
                GameManager.Instance.RemoveHealth(damageToBase);
            }
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Deactivate();
            GameManager.Instance.AddMoney(moneyReward);
            // todo kill behaviour
        }
    }

    private void Deactivate()
    {

        transform.position = EnemyPoolManager.Instance.transform.position;

        EnemyPoolManager.Instance.DespawnEnemy(this);

        gameObject.SetActive(false);

    }
}
