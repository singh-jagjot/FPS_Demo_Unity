using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
        transform.Rotate(new Vector3(0.0f,90f,0.0f),Space.World);
    }
}
