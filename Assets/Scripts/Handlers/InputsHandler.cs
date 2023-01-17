using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsHandler : MonoBehaviour
{
    [SerializeField] GameInputs gameInputs;
    [SerializeField] GameObject GridHandler;
    [SerializeField] GameObject BuildHandler;
    [SerializeField] GameObject cameraHandler;

    private void Awake()
    {
        gameInputs = new GameInputs();


    }

    private void OnEnable()
    {
        gameInputs.GameActionMap.Enable();
       // gameInputs.GameActionMap.WaveMarker.started += GridHandler.GetComponent<GridSetUp>().SpawnMarker;
        gameInputs.GameActionMap.LMB.started += BuildHandler.GetComponent<BuildHandler>().SelectGridPoint;
        gameInputs.GameActionMap.CameraMovement.started += cameraHandler.GetComponent<CameraMovement>().ChangeCameraPoisition;


    }
    private void OnDisable()
    {
        gameInputs.GameActionMap.Disable();
      //  gameInputs.GameActionMap.WaveMarker.started -=  GridHandler.GetComponent<GridSetUp>().SpawnMarker;
        gameInputs.GameActionMap.LMB.started -= BuildHandler.GetComponent<BuildHandler>().SelectGridPoint;
        gameInputs.GameActionMap.CameraMovement.started -= cameraHandler.GetComponent<CameraMovement>().ChangeCameraPoisition;


    }

}
