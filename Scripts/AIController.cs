using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform followTarget;
    private Pawn pawn;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pawn = GetComponent<Pawn>();
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 desiredMovement = agent.desiredVelocity;
        desiredMovement = this.transform.InverseTransformDirection(desiredMovement);
        // Remove the speed by normalizing to 1
        desiredMovement = desiredMovement.normalized;
        // 
        agent.SetDestination(followTarget.position);
        desiredMovement *= pawn.speed;
        anim.SetFloat("Forward", desiredMovement.z);
        anim.SetFloat("Right", desiredMovement.x);
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }

    public Transform GetFollowTarget()
    {
        Transform target;

        // Get the player 
        target = FindObjectOfType<HumanController>().transform;
        return target;
    }
}
