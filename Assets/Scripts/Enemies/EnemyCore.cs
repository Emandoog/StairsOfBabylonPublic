using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public int BPBounty = 10;
    private bool isEnemyBoundToRound = false;// this exisits for testing purposes
    [SerializeField] private float life = 100;
    [SerializeField] private int damage = 1;

    private bool dead = false;
    public void EnemyTakeDamage(float DamageToTake = 1)
    {

        life = life - DamageToTake;
        if (life <= 0 && dead == false)
        {

            dead = true;
            OnKill();
           
        }

    }
    /// <summary>
    /// This should be calleed when enemy dies
    /// </summary>
    public void OnKill()
    {
        if (isEnemyBoundToRound)
        {
            RoundHandler.instance.EnemyKilled();
        }
        //Debug.Log("Killed Enemy");
        PointHandler.instance.AddPoints(BPBounty);
        Destroy(gameObject);


    }
    /// <summary>
    /// This should be called when the enemy arrives at the end of the monster path
    /// </summary>
    public void OnArrivedAtTheEnd() 
    {
        if (isEnemyBoundToRound)
        {
            LifeHandler.instance.PlayerTakeDamage(2);
            RoundHandler.instance.EnemyKilled();
        }
        Destroy(gameObject);

    }
    public void BoundEnemyToRound()
    {
        isEnemyBoundToRound = true;

    }
   
}
