using UnityEngine;

public class SniperTower : TowerBase
{
    protected override void Attack(Collider[] colliders)
    {
        Collider nearestTarget = colliders[0];

        foreach(Collider col in colliders)
        {
            if (Vector3.Distance(col.transform.position, transform.position) < Vector3.Distance(nearestTarget.transform.position,transform.position))
            {
                nearestTarget = col;
            }
        }
        nearestTarget.GetComponent<Enemy>().TakeDamage(damage);
        Debug.Log("Sniper Attack");

        // add sniper anim
    }
}
