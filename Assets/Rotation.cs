using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotate;

    private bool collected;
    private float respawnTime = 2f;
    public float timePassed;

    private void Start()
    {
       rotate.x = rotate.x * Time.deltaTime;
       rotate.y = rotate.y * Time.deltaTime;
       rotate.z = rotate.z * Time.deltaTime;
    }

    private void Update()
    {
        
            this.gameObject.transform.Rotate(rotate, Space.World);
        if(collected == true)
        {
            if(timePassed > respawnTime)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<SphereCollider>().enabled = true;
                collected = false;
                timePassed = 0;
            }
            else
            {
                timePassed = timePassed + Time.deltaTime;
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            collected = true;
        }
    }
}
