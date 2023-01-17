using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower2 : LaserTower1
{

    //[SerializeField] protected GameObject ProjectileToShoot;
    //[SerializeField] protected float towerDamage = 1;
    //[SerializeField] protected float towerDamageScope = 2f;
    //[SerializeField] protected int pierce = 0;
    //[SerializeField] protected float AttackTimer = 1;

    // [SerializeField] protected int upgreadeCost = 20;
   



    //[SerializeField] protected List<GameObject> PointsToShootFrom = new List<GameObject>();
    // Start is called before the first frame update
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
