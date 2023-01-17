using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject cameraPos1;
    [SerializeField] GameObject cameraPos2;
    private bool poss1 = true;
    //private Camera mainCamera;
   
    public void ChangeCameraPoisition(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (poss1)
            {
                poss1 = false;
                gameObject.transform.position = cameraPos2.transform.position;
                gameObject.transform.rotation = cameraPos2.transform.rotation;

            }
            else
            {

                poss1 = true;
                gameObject.transform.position = cameraPos1.transform.position;
                gameObject.transform.rotation = cameraPos1.transform.rotation;
            }
        }
        
       


    }
}
