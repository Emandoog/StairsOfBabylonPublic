using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeHandler : MonoBehaviour
{

    public  int playerLife = 30;
    public static LifeHandler instance;
    [SerializeField] private TMP_Text BPTotalText;
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
        BPTotalText.text = "Player life: " + playerLife;
    }

    /// <summary>
    /// Removes player lives 
    /// </summary>
    /// <param name="damageToTake">Amout of damage player lives will take </param>
    public void PlayerTakeDamage( int damageToTake)
    {
        playerLife -= damageToTake;
        BPTotalText.text = "Player life: " + playerLife;
        if (playerLife <= 0)
        {
            RoundHandler.instance.EndOfGame();

        }
    
    }
}
