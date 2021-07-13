using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    //The animator
    public Animator anim;
    [Header("PlayerSpeeds")]
    [SerializeField, Tooltip("Change the movement speed and turn speed for the player")]
    public float speed;
    [SerializeField] private float turnSpeed;
    [Header("Called Objects")]
    //Public player camera
    public Camera playerCamera;
    public Weapon weapon;
    public Transform weaponMount;
    public Weapon testWeapon;
    private Collider topCollider;
    private Rigidbody topRigidbody;
    private List<Collider> ragdollColliders;
    private List<Rigidbody> ragdollRigidbodies;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;

    // Start is called before the first frame update

    //At start, get the animator.
    private void Start()
    {
        anim = GetComponent<Animator>();
        topCollider = GetComponent<Collider>();
        topRigidbody = GetComponent<Rigidbody>();

        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        //Start not in Ragdoll
        StopRagdoll();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

    }


    /// <summary>
    /// Gets the direction of key presses, and shows that
    /// One Axis is Horizontal and the other is Vertical.
    /// </summary>
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartRagdoll();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopRagdoll();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(20);
        }
    }

    /// <summary>
    /// Allows the character to rotate towards the mouse pointer.
    /// Works using Rays
    /// </summary>
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


    public void EquipWeapon ( Weapon myWeapon)
    {
        GameObject weaponObject = Instantiate(myWeapon.gameObject, weaponMount) as GameObject;
        weapon = weaponObject.GetComponent<Weapon>();
    }

    private void RotateTowards(Vector3 lookAtPoint)
    {
        // Find the rotation to look at our lookAtPoint
        Quaternion goalRotation;
        goalRotation = Quaternion.LookRotation(lookAtPoint - transform.position, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);

    }


    public void OnAnimatorIK(int layerIndex)
    {
        if (weapon != null)
        {
            
            if (weapon.rightHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
            //Same for left hand
            if (weapon.leftHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }


        }


    }

    public void StartRagdoll()
    {
        // Turn off my animator
        anim.enabled = false;
        // Turn off the big capsule collider
        topCollider.enabled = false;
        // Turn off the big rigidbody
        topRigidbody.isKinematic = true;
        // Turn on the ragdoll colliders
        foreach(Collider currentCollider in ragdollColliders)
        {
            currentCollider.enabled = true;
        }
        // Turn on the ragdoll rigidbodies
        foreach(Rigidbody currentRigidbody in ragdollRigidbodies)
        {
            currentRigidbody.isKinematic = false;
        }

    }

    public void StopRagdoll()
    {
        // Turn on the animator
        anim.enabled = true;
        // Turn on the top capsule collider
        foreach (Collider currentCollider in ragdollColliders)
        {
            currentCollider.enabled = false;
        }
        // Turn on the top rigidbody
        foreach (Rigidbody currentRigidbody in ragdollRigidbodies)
        {
            currentRigidbody.isKinematic = true;
        }
        // Turn off the ragdoll colliders
        topCollider.enabled = true;
        //Turn off the ragdoll rigidbodies
        topRigidbody.isKinematic = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
        
   


}
