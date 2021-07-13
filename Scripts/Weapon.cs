using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isTriggerPulled = false;
    public Transform rightHandPoint;
    public Transform leftHandPoint;
    public Transform barrel;
    public float timeNextShotIsReady;
    public float shotsPerMinute;


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isTriggerPulled)
        {
            if (Time.time > timeNextShotIsReady)
            {
                Shoot();
                timeNextShotIsReady = Time.time + (60.0f / shotsPerMinute);
            }
        }
        
    }

    private void Awake()
    {
        timeNextShotIsReady = Time.time;
    }



    public abstract void Shoot();
    public abstract void OnTriggerPull();
    public abstract void OnTriggerRelease();
    public abstract void OnTriggerHold();
}
