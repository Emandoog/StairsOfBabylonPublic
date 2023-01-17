using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower1 : TowerAttack
{

    [SerializeField] protected GameObject ProjectileToShoot;
    [SerializeField] protected float towerDamage = 1;
    [SerializeField] protected float towerDamageScope = 2f;
    [SerializeField] protected int pierce = 0;
    [SerializeField] protected float AttackTimer = 1;
    [SerializeField] protected List<GameObject> PointsToShootFrom = new List<GameObject>();
    void Start()
    {
       
        StartCoroutine(StartShooting());
    }



    public override void TowerAction()
    {
        GameObject clone;
        foreach (GameObject point in PointsToShootFrom)
        {
           
            clone = Instantiate(ProjectileToShoot, point.transform.position, point.transform.rotation);
            clone.GetComponent<Projectile>().SetProjectile(RollForDamagePerfect(towerDamage, towerDamageScope), pierce);
        }

    }
    IEnumerator StartShooting()
    {
        TowerAction();
        yield return new WaitForSecondsRealtime(AttackTimer);

        StartCoroutine(StartShooting());
    }
}
