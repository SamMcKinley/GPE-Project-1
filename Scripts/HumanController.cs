using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public Pawn pawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the direction my stick (or keys) are pressed
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Make sure that direction can't go further than a direction of 1
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);

        pawn.anim.SetFloat("Forward", animationDirection.z * pawn.speed);
        pawn.anim.SetFloat("Right", animationDirection.x * pawn.speed);
        if (pawn.playerCamera != null)
        {
            pawn.RotateToMousePointer();
        }


        if (Input.GetButtonDown("Fire1"))
        {
            pawn.weapon.OnTriggerPull();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pawn.weapon.OnTriggerRelease();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pawn.EquipWeapon(pawn.testWeapon);
        }
    }
}
