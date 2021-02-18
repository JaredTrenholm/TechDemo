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
            other.gameObject.GetComponent<CharacterController>().Move(-other.gameObject.transform.position);
            other.gameObject.GetComponent<CharacterController>().Move(target.transform.position);
            player.PlayOneShot(Teleport);

        }
    }
}
