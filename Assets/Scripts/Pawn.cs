using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Animator anim;
    public float speed;
    public float turnSpeed;
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the direction my stick (or keys) are pressed
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Make sure that direction can't go further than a direction of 1
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);

        anim.SetFloat("Forward", animationDirection.z * speed);
        anim.SetFloat("Right", animationDirection.x * speed);

        RotateToMousePointer();
    }

    public void RotateToMousePointer()
    {
        // Find the plane the character is standing on
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        // Draw a ray from the mouse pointer on the screen towards the plane we are standing on
        Ray myRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        // using the distance to the intersection of plane and ray, find the point in world space
        float distance;
        groundPlane.Raycast(myRay, out distance);
        Vector3 targetPoint = myRay.GetPoint(distance);
        // Rotate towards that point
        RotateTowards(targetPoint);
    }

    private void RotateTowards(Vector3 lookAtPoint)
    {
        // Find the rotation to look at our lookAtPoint
        Quaternion goalRotation;
        goalRotation = Quaternion.LookRotation( lookAtPoint - transform.position , Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);

    }
}
