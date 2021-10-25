using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Variables")]
    public float playerX = 0;
    public float playerY = 0;
    public float moveSpeed = 15.0f;
    
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        GameManager.Instance.StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        playerX = 0;
        playerY = 0;
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f) 
        {
            playerY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5) 
        {
            playerX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        }

        body.velocity = new Vector2(playerX , playerY).normalized * moveSpeed;

    }
}
