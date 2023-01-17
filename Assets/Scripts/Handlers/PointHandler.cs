using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    public static PointHandler instance;
    public int buildPoints = 30;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

    }


    /// <summary>
    /// Adds Build points based on the paramether
    /// </summary>
    /// <param name="pointsToAdd"> Build points to add </param>
    public void AddPoints(int pointsToAdd)
    {
        buildPoints += pointsToAdd;
        BuildHandler.instance.UpdateUI();
    
    }
}
