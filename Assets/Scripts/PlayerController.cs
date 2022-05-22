using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle, dirtParticle;
    public AudioClip jumpSound, crashSound;
    public float jumpForce = 10;
    public float soundVolume = 1.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public bool firstJump;
    public bool dash;
    public int playerScore = 0;

    private Vector3 walkPosition = new Vector3(-5, 0, 0);
    private Vector3 gamePosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && (isOnGround || firstJump))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, soundVolume);
            if (!firstJump)
            {
                playerAnim.SetTrigger("Jump_trig");
            }
            firstJump = !firstJump;
        }
        if ((Input.GetKey(KeyCode.RightArrow) && isOnGround) || (!isOnGround && dash))
        {
            dash = true;
            playerAnim.speed = 3;
        }
        else
        {
            dash = false;
            playerAnim.speed = 1;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            firstJump = false;
            if (!gameOver)
            {
                dirtParticle.Play();
            }
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isOnGround)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
            }
            else
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 2);
            }
            Debug.Log("Game Over! Your score: " + playerScore + " points.");
            gameOver = true;
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, soundVolume);
            dirtParticle.Stop();
        }
    }
}
