using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower3 : SniperTower
{

    //new float towerDamage = 25;
    //new float towerDamageScope = 0.5f;
    //new int pierce = 2;
    //new float AttackTimer = 1;
    //public  bool canUpgrade = false;
    private void Start()
    {
       
        StartCoroutine(StartShooting());
    }
    public override void TowerAction()
    {
        var clone = Instantiate(ProjectileToShoot, PointToShoot.transform.position, PointToShoot.transform.rotation);
        clone.GetComponent<Projectile>().SetProjectile(RollForDamagePerfect(towerDamage, towerDamageScope,true),pierce);
    }


    IEnumerator StartShooting()
    {
        TowerAction();
        yield return new WaitForSecondsRealtime(AttackTimer);

        StartCoroutine(StartShooting());
    }
}
