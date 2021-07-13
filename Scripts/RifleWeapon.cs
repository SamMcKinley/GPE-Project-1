using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    public Projectile ProjectilePrefab;

    public override void Shoot()
    {
        Projectile projectile = Instantiate(ProjectilePrefab, barrel.position, barrel.rotation) as Projectile;
    }
    public override void OnTriggerHold()
    {
        Shoot();
    }

    public override void OnTriggerPull()
    {
        isTriggerPulled = true;
    }

    public override void OnTriggerRelease()
    {
        isTriggerPulled = false;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}

    
