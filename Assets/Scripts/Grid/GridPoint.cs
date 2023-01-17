using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    public int gridType;
    public int x, y;
    public bool Border = false;
    public int pathIndex = -1;
    [SerializeField] private GameObject groundBuildable;
    [SerializeField] private GameObject groundMonsterPath;
    [SerializeField] private GameObject groundPadding;
    [SerializeField] private GameObject groundMonsterPathStart;
    [SerializeField] private GameObject groundMonsterPathEnd;
    [SerializeField] private bool MonsterPath = false;
    private GameObject currentGround;
   
    /// <summary>
    /// Sets Tile type based on given paramether
    /// </summary>
    /// <param name="GridType">Grid type index</param>
    public void  SetTileType(int GridType)
    {
        gridType = GridType;
        switch (GridType)
        {
            case 0:

                if (currentGround != null)
                {
                    Destroy(currentGround);
                }
                currentGround = Instantiate(groundBuildable,gameObject.transform.position,gameObject.transform.rotation, gameObject.transform);
                break;

            case 1:

                if (currentGround != null)
                {
                    Destroy(currentGround);
                }
                currentGround = Instantiate(groundMonsterPath, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                break;

            case 2:

                if (currentGround != null)
                {
                    Destroy(currentGround);
                }
                currentGround = Instantiate(groundPadding, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                break;
            case 3:

                if (currentGround != null)
                {
                    Destroy(currentGround);
                }
                currentGround = Instantiate(groundMonsterPathStart, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                break;
            case 4:

                if (currentGround != null)
                {
                    Destroy(currentGround);
                }
                currentGround = Instantiate(groundMonsterPathEnd, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                break;

        }
    
        
    }


}


