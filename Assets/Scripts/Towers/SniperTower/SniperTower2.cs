using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower2 : SniperTower
{

    //new float towerDamage = 20;
    // new float towerDamageScope = 0.4f;
    // new int pierce = 1;
    // new float   AttackTimer = 1.5f;
    //public  bool canUpgrade = true;

    private void Start()
    {
       
        StartCoroutine(StartShooting());
    }
    public override void TowerAction()
    {
        var clone = Instantiate(ProjectileToShoot, PointToShoot.transform.position, PointToShoot.transform.rotation);
        clone.GetComponent<Projectile>().SetProjectile(RollForDamage(towerDamage, towerDamageScope,true));
    }
    IEnumerator StartShooting()
    {
        TowerAction();
        yield return new WaitForSecondsRealtime(AttackTimer);

        StartCoroutine(StartShooting());
    }
}
