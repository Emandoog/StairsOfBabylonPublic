using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundHandler : MonoBehaviour
{
    [System.Serializable] 
    public class Round // this is a round list with enemies that will spwan in each one 
    {
        public List<GameObject> EnemyList;
    }
    public List<Round> RoundList = new List<Round>();

    public static RoundHandler instance;
   
    [SerializeField] private List<GameObject> enemyTypes = new List<GameObject>();
   
    [SerializeField] public int currentRound = 0;
    [SerializeField] private int maxRounds;
    public int enemiesToSpawnThisRound;
    public int enemiesRemaining = 0;
    [SerializeField] private int timeBetweenRounds = 10;
    [SerializeField] private float spawnTimeOffset = 0.2f;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TMP_Text roundTimer;
    [SerializeField] private GameObject gameCompleteScreen;
    private bool gameEnded = false;
    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

    }

    private void Start()
    {
        roundTimer.text = "Press the start button to start game";
        maxRounds = RoundList.Count;
        enemiesToSpawnThisRound = RoundList[currentRound].EnemyList.Count;
        startGameButton.onClick.AddListener(StartFirstRound);
  
    }
    /// <summary>
    /// Starts first round 
    /// </summary>
    private void StartFirstRound()
    {
        // Debug.Log(RoundList[0].EnemyList[0].name);
        startGameButton.gameObject.SetActive(false);
        currentRound = 0;
        StartCoroutine(StartNextRound());


    }
 
    /// <summary>
    /// Spawns Enemy of a given type at the start of the monster path
    /// </summary>
    /// <param name="enemy"> Enemy to spawn </param>
    private void SpawnEnemy(GameObject enemy)
    {
        var clone = Instantiate(enemy, GridSetUp._MonsterPath[0].position, GridSetUp._MonsterPath[0].rotation, gameObject.transform);
        clone.transform.position = GridSetUp._MonsterPath[0].position;
        clone.GetComponent<EnemyCore>().BoundEnemyToRound();
    }


    /// <summary>
    /// Starts next round 
    /// </summary>
    /// <returns></returns>
    IEnumerator StartNextRound() 
    {
        roundTimer.text = "";
        enemiesToSpawnThisRound = RoundList[currentRound].EnemyList.Count;
        enemiesRemaining = enemiesToSpawnThisRound;
        foreach (GameObject enemyToSpawn in RoundList[currentRound].EnemyList)
        {
            SpawnEnemy(enemyToSpawn);

            yield return new WaitForSeconds(spawnTimeOffset);
        }
        
       

    }
    /// <summary>
    /// Removes one enemy from enemy count in a current round, starts wait time for next round if all enemies die.
    /// </summary>
    public void EnemyKilled() 
    {
        enemiesRemaining -= 1;
        if (enemiesRemaining <= 0)
        {
            if (currentRound +1 >= maxRounds) 
            {

                EndOfGame();
                return;
            }
            currentRound += 1;
            if (gameEnded == false)
            {
                StartCoroutine(WaitForNextRound()); 
            }
           

        }
    
    }
    /// <summary>
    ///  Ends the game after all round are complete.
    /// </summary>
    public void EndOfGame() 
    {
        gameEnded = true;
        gameCompleteScreen.SetActive(true);
        //Debug.Log("Game Ended");
    
    }

    /// <summary>
    /// Creates waiting time between rounds.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForNextRound()
    {
        int timeLeft = timeBetweenRounds;
        roundTimer.text = "Time left: " + timeLeft;
        while (timeLeft != 0) 
        {
            yield return new WaitForSeconds(1);
            timeLeft -= 1;
            roundTimer.text = "Time left: "+ timeLeft ;
            
        }
        StartCoroutine(StartNextRound());




    }
}
