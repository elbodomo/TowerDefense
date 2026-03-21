using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected LayerMask enemyLayer;

    [SerializeField] public float range { get; protected set; } = 5f;
    [SerializeField] public int cost { get; protected set; } = 50;

    protected float fireTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    protected virtual void Update()
    {
        fireTimer -= Time.deltaTime * GameManager.Instance.GameSpeed;

        if (fireTimer <= 0f)
        {
            Collider[] targets = CheckForTargets();
            if (targets.Length > 0)
            {
                Attack(targets);
                fireTimer = fireRate;
            }
        }
    }

    protected virtual Collider[] CheckForTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyLayer);
        return colliders;
    }
    protected abstract void Attack(Collider[] colliders);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
