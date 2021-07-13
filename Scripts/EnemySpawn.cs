using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(1, 2, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float boxOffsetY = boxSize.y / 2;
        Gizmos.DrawCube(transform.position + (boxOffsetY * Vector3.up), boxSize);
        Gizmos.DrawRay(transform.position + (boxOffsetY * Vector3.up), transform.forward);
    }
}
