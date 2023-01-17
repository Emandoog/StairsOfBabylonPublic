using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject GridPoint;
   
    /// <summary>
    /// Hightlight the ground when player hovers over the tower. 
    /// </summary>
    private void OnMouseEnter()
    {
        GridPoint.GetComponent<BuildableGround>().HighlightEmpty();
        BuildHandler.instance.SelectedBuildGrid = GridPoint; 
    }
    /// <summary>
    /// De-hightlight the ground when player stops hovering over the tower.
    /// </summary>
    private void OnMouseExit()
    {
        GridPoint.GetComponent<BuildableGround>().HighlightReset();
        BuildHandler.instance.SelectedBuildGrid = null;
    }
}
