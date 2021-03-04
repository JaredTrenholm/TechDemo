using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPartDoor : MonoBehaviour
{
    public GameObject TopDoor;
    public GameObject BottomDoor;

    public AudioClip doorOpen;


    public Vector3 MovePath;
    private Vector3 TopMovePath;
    private Vector3 BottomMovePath;
    private Vector3 TopDestination;
    private Vector3 BottomDestination;
    private Vector3 TopStart;
    private Vector3 BottomStart;

    private bool TopOpened = false;
    private bool BottomOpened = false;
    private bool TopClosed = false;
    private bool BottomClosed = false;
    private bool opening = false;
    private bool open = false;
    private bool closing = false;
    private bool closed = true;

    private int AmountEntered = 0;

    private void Start()
    {
        TopMovePath = MovePath;
        BottomMovePath = -MovePath;
        TopStart = TopDoor.gameObject.transform.position;
        BottomStart = BottomDoor.gameObject.transform.position;
        TopDestination = TopDoor.gameObject.transform.position + TopMovePath;
        BottomDestination = BottomDoor.gameObject.transform.position + BottomMovePath;
        BottomMovePath = BottomMovePath * Time.deltaTime;
        TopMovePath = TopMovePath * Time.deltaTime;
        BottomMovePath = BottomMovePath * Time.deltaTime;
        TopMovePath = TopMovePath * Time.deltaTime;
    }

        private void Update()
        {
        if(opening == true || closing == true)
        {

            if (this.gameObject.GetComponent<AudioSource>().isPlaying == doorOpen)
            {

            }
            else
            {
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);
            }
        }
            if(BottomOpened == true && TopOpened == true)
            {
                opening = false;
                BottomOpened = false;
                TopOpened = false;
                open = true;
            }
            if (BottomClosed == true && TopClosed == true)
            {
                closing = false;
                BottomClosed = false;
                TopClosed = false;
                closed = true;
            }
            if (opening == true)
                {
                    if(TopDoor.transform.position.y < TopDestination.y)
                    {
                        TopDoor.transform.Translate(TopMovePath);

                    }
                    else
                    {
                        TopOpened = true;
                    }
                    if (BottomDoor.transform.position.y > BottomDestination.y)
                    {
                        BottomDoor.transform.Translate(BottomMovePath);
                    }
                    else
                    {
                        BottomOpened = true;
                    }
                }
        else if (closing == true)
        {
            if (TopDoor.transform.position.y > TopStart.y)
            {
                TopDoor.transform.Translate(-TopMovePath);

            }
            else
            {
                TopClosed = true;
            }
            if (BottomDoor.transform.position.y < BottomStart.y)
            {
                BottomDoor.transform.Translate(-BottomMovePath);
            }
            else
            {
                BottomClosed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


        AmountEntered = AmountEntered + 1;
            if (closed == true)
            {
                closed = false;
                opening = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);

            }
        } else if (other.tag == "SpawnedCube")
        {


            AmountEntered = AmountEntered + 1;
            if (closed == true)
            {
                closed = false;
                opening = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);

            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        AmountEntered = AmountEntered - 1;
        if (AmountEntered == 0)
        {


            if (other.tag == "Player")
            {



                open = false;
                opening = false;
                closing = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);


            }
            else if (other.tag == "SpawnedCube")
            {
                open = false;
                opening = false;
                closing = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);
            }
        }
    }

    private void EmergencyClose(Collider other)
    {
        if (other.tag == "Player")
        {


            if (open == true)
            {
                open = false;
                closing = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(doorOpen);

            }
        }
    }
}
