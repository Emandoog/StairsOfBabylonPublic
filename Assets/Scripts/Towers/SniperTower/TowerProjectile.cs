using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerProjectile : Projectile
{
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        Destroy(gameObject, 3);
    }
    void Update()
    {
        AddForceToProjectile();
    }

    public override void AddForceToProjectile()
    {
       
        RB.velocity = transform.forward * projectileSpeed;
            
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void SetProjectile(float damage, int pierce = 0)
    {
        projectileDamage = damage;
        projectilePierce = pierce;

    }
    

    //private  void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        other.GetComponent<EnemyCore>().EnemyTakeDamage(projectileDamage);
    //        if (Pierce > 0)
    //        {
    //            Pierce -= 1;
    //        }
    //        else 
    //        {
    //            other.GetComponent<Projectile>().Die();
    //        }
           
    //    }
    //}
}
