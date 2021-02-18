using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Triggers : MonoBehaviour
{
    private Vector3 respawn;
    private int Coins;
    private int Lives = 3;
    public AudioClip coinClip;
    public AudioClip checkpointClip;
    public AudioSource playerAudio;
    public GameObject WarpOneTarget;

    public Text coinText;
    public Text livesText;

    private Vector3 StartPos;


    private void Start()
    {
        StartPos = this.gameObject.transform.position;
        respawn = this.gameObject.transform.position;
        Coins = 0;
    }
    private void Update()
    {
        coinText.text = "Coins: " + Coins.ToString();
        livesText.text = "Lives: " + Lives.ToString();
        if (Coins == 10)
        {
            Coins = 0;
            Lives = Lives + 1;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.GetComponent<CharacterController>().Move((-this.gameObject.transform.position) + new Vector3(0, 1, 0));
            this.gameObject.GetComponent<CharacterController>().Move((respawn));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
            this.gameObject.GetComponent<CharacterController>().Move((-this.gameObject.transform.position) + new Vector3(0,1,0));
            CheckLives();
            Lives = Lives - 1;
            this.gameObject.GetComponent<CharacterController>().Move((respawn));

        } else if(other.tag == "Checkpoint"){
            playerAudio.PlayOneShot(checkpointClip);
            respawn = this.gameObject.transform.position;
            other.gameObject.SetActive(false);
        } else if(other.tag == "Coin")
        {
            playerAudio.PlayOneShot(coinClip);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            Coins = Coins + 1;
        }
    }

    private void CheckLives()
    {
        if(Lives == 0)
        {
            respawn = StartPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<CharacterController>().Move((-this.gameObject.transform.position) + new Vector3(0, 1, 0));
            CheckLives();
            Lives = Lives - 1;
            this.gameObject.GetComponent<CharacterController>().Move((respawn));
        }
    }
}
