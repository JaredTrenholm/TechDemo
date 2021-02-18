using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("AI Movement")]
    public Vector3 MovePath;
    void Start()
    {
        MovePath.x = MovePath.x * Time.deltaTime;
            MovePath.y = MovePath.y * Time.deltaTime;
            MovePath.z = MovePath.z * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

            this.gameObject.transform.Translate(MovePath, Space.World);
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyWall")
        {
            MovePath = -MovePath;
        }
    }
}
