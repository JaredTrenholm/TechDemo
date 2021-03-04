using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    private GameObject SpawnedCube;
    public GameObject OriginalCube;

    private bool HasSpawned = false;

    public Vector3 SpawnTarget;

    private bool Kill = false;

    private Vector3 StartPos;

    private bool Checkpoint;

    private bool Victory;
    private float VictoryTime = 3f;
    public Text VictoryText;
    public AudioClip VictoryClip;
    public float VtimePassed;

    public Text CheckpointText;
    private float DisplayTime = 1f;
    public float CtimePassed;

    private bool OnPlatform = false;


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
        if (Input.GetKeyDown(KeyCode.Q) || Kill == true)
        {
            if (OnPlatform == false)
            {
                this.gameObject.GetComponent<Collider>().enabled = false;
                this.gameObject.transform.position = respawn;
                this.gameObject.GetComponent<Collider>().enabled = true;
                Kill = false;
            }
        }

        if (Victory == true)
        {
            if(VtimePassed >= VictoryTime)
            {
                Victory = false;
                VictoryText.gameObject.SetActive(false);

            }
            else
            {
                VtimePassed = VtimePassed + Time.deltaTime;
            }
        }

        if (Checkpoint == true)
        {
            if (CtimePassed >= DisplayTime)
            {
                Checkpoint = false;
                CheckpointText.gameObject.SetActive(false);

            }
            else
            {
                CtimePassed = CtimePassed + Time.deltaTime;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
            CheckLives();
            Lives = Lives - 1;
            Kill = true;

        } else if(other.tag == "Checkpoint"){
            playerAudio.PlayOneShot(checkpointClip);
            respawn = this.gameObject.transform.position;
            other.gameObject.SetActive(false);

            CtimePassed = 0f;
            Checkpoint = true;
            CheckpointText.gameObject.SetActive(true);
        }
        else if (other.tag == "Victory")
        {
            playerAudio.PlayOneShot(VictoryClip);
            other.gameObject.SetActive(false);
            respawn = this.gameObject.transform.position;
            VtimePassed = 0f;
            Victory = true;
            VictoryText.gameObject.SetActive(true);
        }
        else if(other.tag == "Coin")
        {
            playerAudio.PlayOneShot(coinClip);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            Coins = Coins + 1;
        }
        else if (other.tag == "Spawner")
        {
            if(HasSpawned == true)
            {
            SpawnedCube.SetActive(false);
            }
            SpawnedCube = Instantiate(OriginalCube);
            SpawnedCube.gameObject.transform.position = SpawnTarget;
            HasSpawned = true;
        }
        else if (other.tag == "MovingPlatform")
        {
            OnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MovingPlatform")
        {
            OnPlatform = false;
        }
    }
    private bool CheckLives()
    {
        bool Dead = false;
        if(Lives == 0)
        {
            respawn = StartPos;
            Dead = true;
        }
        return Dead;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (CheckLives() == true)
            {
                Lives = 3;
                Coins = 0;
                respawn = this.gameObject.transform.position;
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                Lives = Lives - 1;
                Kill = true;
            }
        }
    }
}
