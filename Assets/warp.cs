using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public GameObject target;
    public AudioSource player;
    public AudioClip Teleport;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.transform.position = target.transform.position;
            other.gameObject.GetComponent<Collider>().enabled = true;
            player.PlayOneShot(Teleport);

        }
    }
}
