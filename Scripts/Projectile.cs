using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Weapon Damage;
    public Rigidbody Rigidbody;
    public float Lifetime = 2.0f;

    private void Awake()
    {
        Destroy(this.gameObject, Lifetime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
