using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    public Vector3 cameraOffset;
    private GameObject targetObject;

    private void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("PhotonPlayerTag");
        //TODO - find function makes pc to control all 3

        //TODO - make camera follow the cube properly?
        cameraOffset = transform.position - targetObject.transform.position;
    }
    
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}


