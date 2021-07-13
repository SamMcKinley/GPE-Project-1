using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public Transform playerPosition;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // Move the camera to the players position + the offset
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.position + offset, speed * Time.deltaTime);
    }
}
