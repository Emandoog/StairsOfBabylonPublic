using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridSetUp : MonoBehaviour
{
    [SerializeField] private GameObject playableGround;
    [SerializeField] private GameObject paddingGround;
    [SerializeField] private GameObject markerEnemy;
    [SerializeField] private float stepHeightModfier = 0.1f;
  
    private List<string> middleTiles = new List<string>();
    private Dictionary<string,GameObject> gridMap = new Dictionary<string,GameObject>();
    private int maxWidth,maxheight;

    public static float stepHeight;
    public static List<Transform> _MonsterPath = new List<Transform>();// enemies use this to walk on path
    public List<string> _Path = new List<string>();

    void Start()
    {
        stepHeight = stepHeightModfier;
        SpawnGrid(10, 10);
        SpawnStartMarker();
    }

    /// <summary>
    /// Creates Level
    /// </summary>
    /// <param name="width"> Width of the level</param>
    /// <param name="height"> Height of the level</param>
    /// <param name="padding"> Padding of the level</param>
    private void SpawnGrid(int width, int height,double padding = 3) 
    {
        if (width <= 0 || height <= 0)
        {
            Debug.Log("WrongParameters");
            return;
        
        }
        var paddingProper = (int)padding;
        
        maxWidth = width;
        maxheight = height;
        
        for (int i = 0- paddingProper; i < width + padding; i++)
        {
            for (int x = 0- paddingProper; x < height + padding; x++)
            {

                if ((i == 0 || i == maxWidth-1 ) && (x >= 0 && x < maxheight) )
                {
                    //Padding tiles setup
                    GameObject temp = Instantiate(playableGround, new Vector3(i, 0, x), playableGround.transform.rotation, gameObject.transform);
                    gridMap.Add(i + "," + x, temp);
                    temp.GetComponent<GridPoint>().Border = true;
                    temp.GetComponent<GridPoint>().x = i;
                    temp.GetComponent<GridPoint>().y = x;
                    temp.GetComponent<GridPoint>().SetTileType(0);
                    temp.gameObject.name = "(" + i + "," + x + ")";
                    //Debug.Log(getTile(i, x).name);

                }
                else if ((x == 0 || x == maxheight-1 ) && (i >= 0 && i  < maxWidth)) 
                {

                    //Padding tiles setup
                    GameObject temp = Instantiate(playableGround, new Vector3(i, 0, x), playableGround.transform.rotation, gameObject.transform);
                    gridMap.Add(i + "," + x, temp);
                    temp.GetComponent<GridPoint>().x = i;
                    temp.GetComponent<GridPoint>().y = x;
                    temp.GetComponent<GridPoint>().Border = true;
                    temp.GetComponent<GridPoint>().SetTileType(0);
                    temp.gameObject.name = "(" + i + "," + x + ")";
                    //Debug.Log(getTile(i, x).name);



                }
                else if (i < width && i > 0 && x < height && x > 0)
                {
                    //Buildable tiles setup
                    GameObject temp = Instantiate(playableGround, new Vector3(i, 0, x), playableGround.transform.rotation, gameObject.transform);
                    gridMap.Add(i + "," + x, temp);
                    middleTiles.Add(i + "," + x);
                    temp.GetComponent<GridPoint>().x = i;
                    temp.GetComponent<GridPoint>().y = x;
                    
                    temp.GetComponent<GridPoint>().SetTileType(0);
                    temp.gameObject.name = "(" + i + "," + x + ")";
                   // Debug.Log(getTile(i, x).name);
                }
                else
                {   //Padding tiles setup
                    GameObject temp = Instantiate(playableGround, new Vector3(i, (float)0.5, x), playableGround.transform.rotation, paddingGround.transform);
                    temp.gameObject.name = "(" + i + "," + x + ")";
                    temp.GetComponent<GridPoint>().SetTileType(2);
                }
               




            }

        }
        //After level is done build monster path
        BuildPath();

    }

    /// <summary>
    /// Builds monster path
    /// </summary>
    private void BuildPath()
    {

        double maxRange =  ((maxheight * maxWidth)/3)-1;// maximum path lenght
        int x, y;
        bool done = false;
        GameObject currentTile;
        List<GameObject> _PossibleTiles = new List<GameObject>();
        var temp = middleTiles[Random.Range(0,middleTiles.Count)];  //take random starting point from BuildableTiles
        currentTile = gridMap[temp];
     
        x = currentTile.GetComponent<GridPoint>().x;
        y = currentTile.GetComponent<GridPoint>().y;
        _Path.Add("" + x + "," + y);
        _MonsterPath.Add(getTile(x,y).transform);
        currentTile.transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y , currentTile.transform.position.z);
        //check surrounding tiles
        //add them to the list if they pass the check
        if (IsTileValidForPath(x, y + 1))
        {
            _PossibleTiles.Add(getTile(x, y + 1));
           
        }
        if (IsTileValidForPath(x, y - 1))
        {
            _PossibleTiles.Add(getTile(x, y - 1));
          
        }
        if (IsTileValidForPath(x + 1, y ))
        {
            _PossibleTiles.Add(getTile(x + 1, y));
           
        }
        if (IsTileValidForPath(x - 1, y ))
        {
            _PossibleTiles.Add(getTile(x - 1, y));
          
        }
        var random = Random.Range(0, _PossibleTiles.Count); // randomize number based on the number of possible directions
        var temp2 = _PossibleTiles[random]; //select tile
        

        x = temp2.GetComponent<GridPoint>().x;
        y = temp2.GetComponent<GridPoint>().y;
      
        _Path.Add("" + x + "," + y);
        _MonsterPath.Add(getTile(x, y).transform);// add tile to monster path
          
        temp2.transform.position =  new Vector3(temp2.transform.position.x, temp2.transform.position.y  , temp2.transform.position.z);
        for (int i = 1; i < maxRange+1; i++)
        {

            _PossibleTiles.Clear();
            bool AtleastOnePossibleTile = false;
            if (i == maxRange) // we are done creating path
            { 
              done = true;
            
                SetPath();// set pathing tiles
                return;

            }
            if (IsTileValidForPath(x, y + 1))
            {
                _PossibleTiles.Add(getTile(x, y + 1));
                AtleastOnePossibleTile = true;
            }
            if (IsTileValidForPath(x, y - 1))
            {
                _PossibleTiles.Add(getTile(x, y - 1));
                AtleastOnePossibleTile = true;
            }
            if (IsTileValidForPath(x+1, y ))
            {
                _PossibleTiles.Add(getTile(x+1, y));
                AtleastOnePossibleTile = true;
            }
            if (IsTileValidForPath(x -1 , y ))
            {
                _PossibleTiles.Add(getTile(x -1 ,y ));
                AtleastOnePossibleTile = true;
            }
            if (!AtleastOnePossibleTile)// cannot continue looking for path
            {
                done = true;

                SetPath();// set pathing tiles
                return;

               
            }
            var random2 = Random.Range(0, _PossibleTiles.Count-1); // randomize number based on the number of possible tiles
          
            var temp3 = _PossibleTiles[random2];
            x = temp3.GetComponent<GridPoint>().x;
            y = temp3.GetComponent<GridPoint>().y;
            _Path.Add("" + x + "," + y);
            _MonsterPath.Add(getTile(x, y).transform);
          
            temp3.transform.position = new Vector3(temp3.transform.position.x, temp3.transform.position.y +stepHeightModfier*i , temp3.transform.position.z);


        }

     

    }

    /// <summary>
    /// Sets monster path tiles 
    /// </summary>
    private void SetPath()
    {   //setup path starting tile
        gridMap[_Path[0]].GetComponent<GridPoint>().pathIndex = 0;
        gridMap[_Path[0]].GetComponent<GridPoint>().SetTileType(3);
        for (int u = 1; u < _Path.Count; u++)
        {
            if (u == _Path.Count-1)
            {   //setup path ending tile
                gridMap[_Path[u]].GetComponent<GridPoint>().pathIndex = 0;
                gridMap[_Path[u]].GetComponent<GridPoint>().SetTileType(4);
                return;
            }
            //setup path tile
            gridMap[_Path[u]].GetComponent<GridPoint>().pathIndex = u;
            gridMap[_Path[u]].GetComponent<GridPoint>().SetTileType(1);

        }

    }
    /// <summary>
    /// Returns GameObject based on given x and y of a tile.
    /// </summary>
    /// <param name="x"> X paramether</param>
    /// <param name="y"> Y paramether</param>
    /// <returns></returns>
    private GameObject getTile(int x, int y)  // return tile gameobject based on tile X,Y position
    {
        string tile = x + "," + y;
        return gridMap[tile];
    }
    /// <summary>
    /// Check if a tile is valid for making it part of the monster path
    /// </summary>
    /// <param name="x"> X paramether</param>
    /// <param name="y"> Y paramether</param>
    /// <returns></returns>
    public bool IsTileValidForPath(int x, int y) 
    {
        bool isValid = true;
        if (getTile(x, y).GetComponent<GridPoint>().Border == true || _Path.Contains("" + x + "," + y))
        {
            isValid = false;
        }
        return isValid;
    }
    /// <summary>
    /// Spawns Marker enemy that follows monster path
    /// </summary>
    public void SpawnStartMarker() 
    {
        var clone = Instantiate(markerEnemy, _MonsterPath[0].position, _MonsterPath[0].rotation, gameObject.transform);
        clone.transform.position = _MonsterPath[0].position;
    }
    //public void SpawnMarker(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        var clone = Instantiate(markerEnemy, _MonsterPath[0].position, _MonsterPath[0].rotation, gameObject.transform);
    //        clone.transform.position = _MonsterPath[0].position;
    //    }

    //}

    //private void AddTilesToGrid()
    //{

    //    for (int i = 0; i < maxWidth; i++)
    //    {
    //        for (int x = 0; x < maxheight; x++)
    //        {
    //            GameObject temp = Instantiate(playableGround, new Vector3(i, 0, x), playableGround.transform.rotation, gameObject.transform);


    //            // gridMap.Add(i + "," + x, temp);
    //            // Debug.Log(getTile(i, x).name);
    //        }

    //    }



    //}


}
