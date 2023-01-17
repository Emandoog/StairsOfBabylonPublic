using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  TowerAttack : MonoBehaviour
{
    public  GameObject UpgradedTower;
    public int upgreadeCost;
    public int totalValue;

    public bool canUpgrade;
    public abstract void TowerAction();
    /// <summary>
    /// Returns damage, damage variation can be negative.
    /// </summary>
    /// <param name="damage">Base damage of the projectile.</param>
    /// <param name="scope">Damage variation of the projectile.</param>
    /// <param name="lucky">Whether the variation is calculated twice.</param>
    /// <returns></returns>
    public float RollForDamage(float damage = 1, float scope= 0.3f, bool lucky = false) 
    {
        float roll1, roll2;
        if (lucky)
        {
           
            roll1 = Mathf.Round(damage * (1f + (Random.Range(-scope, scope))));
            roll2 = Mathf.Round(damage * (1f + (Random.Range(-scope, scope))));

            if (roll1 > roll2)
            {
               // Debug.Log(roll1);
                return roll1;
            }
            else
            {
              //  Debug.Log(roll2);
                return roll2;
            }

        }
        else 
        {

            roll1 = Mathf.Round(damage * (1 + (Random.Range(-scope, scope))));
            //Debug.Log(roll1);
            return roll1;
           
        }
      

    }
    /// <summary>
    /// Returns damage, damage variation can only be positive
    /// </summary>
    /// <param name="damage">Base damage of the projectile.</param>
    /// <param name="scope">Damage variation of the projectile.</param>
    /// <param name="lucky">Whether the variation is calculated twice.</param>
    /// <returns></returns>
    public float RollForDamagePerfect(float damage = 1, float scope = 0.3f, bool lucky = false)
    {
        float roll1, roll2;
        if (lucky)
        {

            roll1 = Mathf.Round( damage * (1f + (Random.Range(0, scope))));
            roll2 = Mathf.Round( damage * (1f + (Random.Range(0, scope))));
           
            if (roll1 > roll2)
            {
               // Debug.Log(roll1);
                return roll1;
            }
            else
            {
              //  Debug.Log(roll2);
                return roll2;
            }

        }
        else
        {

            roll1 = Mathf.Round(damage * (1 + (Random.Range(0, scope))));
           // Debug.Log(roll1);
            return roll1;

        }


    }

   
}

    
