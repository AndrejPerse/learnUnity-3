using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;
    private float dashSpeed = 30;
    private float leftBound = -10;
    private float scorePosition = -1;
    private bool alreadyScored;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.dash)
            {
                transform.Translate(dashSpeed * Time.deltaTime * Vector3.left);
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
            }
        }
        if (transform.position.x < scorePosition && !alreadyScored && gameObject.CompareTag("Obstacle"))
        {
            alreadyScored = true;
            playerControllerScript.playerScore++;
            if (playerControllerScript.dash)
            {
                playerControllerScript.playerScore++;
            }
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
