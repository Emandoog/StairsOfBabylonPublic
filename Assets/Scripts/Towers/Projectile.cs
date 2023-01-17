using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed;
    protected float projectileDamage;
    protected int projectilePierce;
    protected Rigidbody RB;
    private  void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    public abstract void  AddForceToProjectile();
    public abstract void Die();
    public abstract void SetProjectile(float damage, int pierce = 0);

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyCore>().EnemyTakeDamage(projectileDamage);
            if (projectilePierce > 0)
            {
                projectilePierce -= 1;
            }
            else
            {
                Die();
            }

        }
        else if(other.CompareTag("HittableByProjectiles"))
        {
            Die();
        }
    }
}
