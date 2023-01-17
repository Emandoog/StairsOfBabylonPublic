using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildableGround : MonoBehaviour
{

    
    private Color baseColor;
    private Renderer rend;
    private GameObject CurrentTower;
    public bool empty = true;
    
    public float currentTowerValue;
   
    public bool ActiveSelection = false;
    public event EventHandler OnGridHover;
    [SerializeField] private float GroundHeightMod = 0f;
    [SerializeField] private GameObject Tower1;
    [SerializeField] private GameObject Tower2;
    [SerializeField] private GameObject Tower3;
    //[SerializeField] private GameObject BuildUI;
    [SerializeField] private int rotation = 0 ;
    [SerializeField] private float sellValue = 0.7f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    /// <summary>
    /// Rotates ground
    /// </summary>
    public void Rotate()
    {
       
        switch (rotation) 
        {
            case 0:
                gameObject.transform.rotation = Quaternion.Euler( new Vector3(gameObject.transform.rotation.x, 90, gameObject.transform.rotation.z));
                break;
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z));
                break;
            case 2:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x,270, gameObject.transform.rotation.z));
                break;
            case 3:
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.x, 360, gameObject.transform.rotation.z));
                rotation = -1;
                break;


        }
        rotation += 1;
    }
    /// <summary>
    /// Hightlight the tile when player hovers over it
    /// </summary>
    private void OnMouseEnter()
    {

        if (ActiveSelection == false)
        {
            HighlightEmpty();
           
        }
        BuildHandler.instance.SelectedBuildGrid = gameObject;
    }
   
    private void OnMouseExit()
    {
        if (ActiveSelection == false)
        {
            HighlightReset();
           
        }
        BuildHandler.instance.SelectedBuildGrid = null;
    }

    /// <summary>
    /// adds height to the ground
    /// </summary>
    public void AddHeight() 
    {
         if (GroundHeightMod < 50) 
         {
            GroundHeightMod += 1;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,  GroundHeightMod * GridSetUp.stepHeight, gameObject.transform.position.z);
         }

    }
    /// <summary>
    ///  remove height of the tower
    /// </summary>
    public void RemoveHeight()
    {
        if (GroundHeightMod > 0)
        {
            GroundHeightMod -= 1;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, GroundHeightMod * GridSetUp.stepHeight, gameObject.transform.position.z);
        }

    }


    /// <summary>
    /// Build tower in the selected tile based on the given Id
    /// </summary>
    /// <param name="TowerId"> Id of th tower we want to build</param>
    public void BuildTower(int TowerId = 1)
    {
        GameObject clone;
        switch (TowerId)
        {
            case 1:
                if(PointHandler.instance.buildPoints >= 10) 
                {
                    currentTowerValue = currentTowerValue + 10;
                    PointHandler.instance.AddPoints(-10);

                    clone = Instantiate(Tower1, gameObject.transform);
                    clone.GetComponent<Tower>().GridPoint = gameObject;
                    CurrentTower = clone;
                    empty = false;
                }
                break;
            case 2:
                if (PointHandler.instance.buildPoints >= 10) 
                {
                    currentTowerValue = currentTowerValue + 10;
                    PointHandler.instance.AddPoints(-10);

                    clone = Instantiate(Tower2, gameObject.transform);
                    clone.GetComponent<Tower>().GridPoint = gameObject;
                    CurrentTower = clone;
                    empty = false;
                }
                break;
            case 3:
                if (PointHandler.instance.buildPoints >= 10) 
                {
                    currentTowerValue = currentTowerValue + 10;
                    PointHandler.instance.AddPoints(-10);

                    clone = Instantiate(Tower3, gameObject.transform);
                    clone.GetComponent<Tower>().GridPoint = gameObject;
                    CurrentTower = clone;
                    empty = false;
                }
                break;
            case 4:
                break;

        }
       
       
    }
    /// <summary>
    /// Destroys tower in tile and return certain amout of point that were used to build it
    /// </summary>
    public void SellTower()
    {
        PointHandler.instance.AddPoints((int)MathF.Round(currentTowerValue *sellValue));
        Destroy(CurrentTower);
        currentTowerValue = 0;
        empty = true;
    }
    /// <summary>
    /// Upgrades tower 
    /// </summary>
    public void UpgradeTower()
    {
        if (CurrentTower !=null)
        {
            //Debug.Log(CurrentTower.GetComponent<TowerAttack>().canUpgrade);
            if (CurrentTower.GetComponent<TowerAttack>().canUpgrade == true && CurrentTower.GetComponent<TowerAttack>().upgreadeCost <= PointHandler.instance.buildPoints) 
            {
                currentTowerValue += CurrentTower.GetComponent<TowerAttack>().upgreadeCost;

                PointHandler.instance.AddPoints(-CurrentTower.GetComponent<TowerAttack>().upgreadeCost);
                GameObject TowerToUpgradeTo = CurrentTower.GetComponent<TowerAttack>().UpgradedTower;
                Destroy(CurrentTower);

                var  clone = Instantiate(TowerToUpgradeTo, gameObject.transform);
                clone.GetComponent<Tower>().GridPoint = gameObject;
                CurrentTower = clone;
           
            }
        }
    }
    /// <summary>
    /// Attempt to higlight the tile when the tile when  the tile is not actively selected(on hover)
    /// </summary>
    public void HighlightEmpty() 
    {
        if(ActiveSelection == false) 
        { 
             rend.material.color = new Color(rend.material.color.r, rend.material.color.g + 1, rend.material.color.b);
        }
    }
    /// <summary>
    /// Attempt to hightlight or de-highlight the tile when it is actively selected (on click)
    /// </summary>
    public void Highlight()
    {
        if (ActiveSelection == false)
        {
            HighlightReset();
        }
        else 
        {
            rend.material.color = new Color(rend.material.color.r + 1, rend.material.color.g + 1, rend.material.color.b + 1);
        }
       

    }
    /// <summary>
    /// resets higlight
    /// </summary> 
    public void HighlightReset()
    {
        if (ActiveSelection == false) 
        {
            rend.material.color = baseColor;
        }
       
    }
}
