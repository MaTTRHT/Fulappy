using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 4;
    // public GameObject GameManager;
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    //Awake is called before Start.
    private void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<BoxCollider2D> ();
        groundHorizontalLength = groundCollider.size.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(-speed, 0);
    }

    //Update runs once per frame
    private void Update()
    {
        if (transform.position.x < -groundHorizontalLength*2.5)
        {
            RepositionBackground ();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(groundHorizontalLength * 5f, 0);
        transform.position = (Vector2) transform.position + groundOffSet;
    }
}
