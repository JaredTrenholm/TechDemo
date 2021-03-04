using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 movePath = new Vector3(0f,0f,0f);
    public GameObject player;

    public AudioSource playerSource;
    public AudioClip elevatorMusic;
    public AudioSource background;

    private Vector3 OriginalMovePath;

    bool isMoving = false;
    bool isReturning = false;
    bool Activated = false;
    void Start()
    {
        movePath = movePath * Time.deltaTime;
        OriginalMovePath = movePath;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            this.gameObject.transform.Translate(movePath);
            player.GetComponent<CharacterController>().Move(movePath);
        }

        if (isReturning)
        {
            this.gameObject.transform.Translate(movePath);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MovingPlatformZ") {
            isMoving = false;
            isReturning = false;
        } else if (other.gameObject.tag == "Player")
        {
            if(Activated == false)
            {
                playerSource.PlayOneShot(elevatorMusic);
                background.Stop();
                movePath = OriginalMovePath;
            isMoving = true;
                isReturning = false;
            Activated = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Activated == true)
            {
                playerSource.Stop();
                background.Play();
                movePath = -movePath;
                isMoving = false;
                isReturning = true;
                Activated = false;
            }
        }
    }
}
