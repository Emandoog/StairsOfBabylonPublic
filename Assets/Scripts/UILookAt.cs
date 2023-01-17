using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{


    private void FixedUpdate()
    {
        gameObject.transform.LookAt(Camera.main.transform);
    }
}
