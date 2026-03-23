using UnityEngine;

public class AoeTower : TowerBase
{
    protected override void Attack(Collider[] colliders)
    {
        foreach(Collider col in colliders)
        {
            col.GetComponent<Enemy>().TakeDamage(damage);

            Debug.Log("Aoe Attack");


            attacParticales.Play();
        }
    }
}
