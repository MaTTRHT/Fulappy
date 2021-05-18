using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 4;
    // public GameObject GameManager;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(-speed, 0);
    }

    private void FixedUpdate() 
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        
        // if(GameManager._instance.gamestate != GameManager.GameStates.play)
        // {
            
        //     rb.velocity = new Vector2(0, 0);
        //     GameManager._instance.PauseGame();
        // }
        // else
        // {
        //     rb.velocity = new Vector2(-speed, 0);
        // }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
