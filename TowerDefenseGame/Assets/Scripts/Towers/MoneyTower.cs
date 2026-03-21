using UnityEngine;

public class MoneyTower : TowerBase
{
    protected override void Update()
    {
        fireTimer -= Time.deltaTime * GameManager.Instance.GameSpeed;

        if (fireTimer <= 0f)
        {
            GameManager.Instance.AddMoney(damage);

            fireTimer = fireRate;

            //add money Animation
        }
    }
    protected override void Attack(Collider[] colliders)
    {

    }
}
