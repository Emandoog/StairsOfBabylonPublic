using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private  float speed = 3;
    private  int waypointIndex = 1;
    private Transform target;
    private EnemyCore enemyCore;

    // Start is called before the first frame update
    void Start()
    {
        enemyCore = GetComponent<EnemyCore>();
        target = GridSetUp._MonsterPath[waypointIndex];
    }

    
   
    private void FixedUpdate()
    {
        /// move enemy to the next waypoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed *Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <=0.05f)
        {
            GoToNextWaypoint();

        }
    }

    public void GoToNextWaypoint() 
    {
        waypointIndex = waypointIndex + 1;
        if (waypointIndex == GridSetUp._MonsterPath.Count)
        {
            enemyCore.OnArrivedAtTheEnd();
            return;
        }
        target = GridSetUp._MonsterPath[waypointIndex];
       
    }
}
