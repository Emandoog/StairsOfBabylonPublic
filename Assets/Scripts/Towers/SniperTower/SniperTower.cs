using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : TowerAttack
{
    [SerializeField] protected GameObject PointToShoot;
    [SerializeField] protected GameObject ProjectileToShoot;
    //[SerializeField] protected int upgreadeCost = 20;
    [SerializeField] protected float towerDamage = 10;
    [SerializeField] protected float towerDamageScope = 0.5f;
    [SerializeField] protected int pierce = 0;
    [SerializeField] protected float AttackTimer = 2;
    //public  bool canUpgrade = true;

    private void Start()
    {
    
        StartCoroutine(StartShooting());
    }
    public override void TowerAction() 
    {
        var clone = Instantiate(ProjectileToShoot,PointToShoot.transform.position, PointToShoot.transform.rotation);
        clone.GetComponent<Projectile>().SetProjectile(RollForDamage(towerDamage,towerDamageScope),pierce);
    }
  

    IEnumerator StartShooting() 
    {
        TowerAction();
        yield return new WaitForSecondsRealtime(AttackTimer);

        StartCoroutine(StartShooting());
    }

    
}
