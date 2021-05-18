using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS : MonoBehaviour
{
    public float jumpuP = 50;
    Rigidbody2D rb;
    public float rotSpeed = 5f;
    public float fallSpeed = 3f;
    public float jumpDesiredRot = 50;
    public float fallDesiredRot = -80;
    Animator animator;
    private AudioManager am;
    // private bool rotating = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start() {
        GameManager._instance.gamestate = GameManager.GameStates.start;
        animator.Play("Base Layer.fall", 0, 0.70f);
        Time.timeScale = 0;
        // rb.velocity = new Vector2(2.5f, rb.velocity.y);
        jumpDesiredRot += transform.eulerAngles.z;
        am = FindObjectOfType<AudioManager>();
        // animator.Play("Base Layer.fall", 0, 0.71f);
    }

    private void FixedUpdate() {
        // Debug.Log(GameManager._instance.gamestate);
        // if(rotating)
        // {
        //     var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, jumpDesiredRot);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.fixedDeltaTime * rotSpeed);
        //     StartCoroutine(jumpDone(Time.fixedDeltaTime * rotSpeed *2));
        // }
        // else
        // {
        //     var desiredRotF = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, fallDesiredRot);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotF, Time.fixedDeltaTime * fallSpeed);
        // }
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetButtonDown("Jump")) && GameManager._instance.gamestate == GameManager.GameStates.start)
        {
            GameManager._instance.ResumeGame();
            // jumpu();
        }
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetButtonDown("Jump")) && GameManager._instance.gamestate == GameManager.GameStates.play)
        {
            jumpu();
        }
        if (Input.GetButtonDown("Retry") && GameManager._instance.gamestate == GameManager.GameStates.lose)
        {
            GameManager._instance.RestartGame();
        }
        if (Input.GetButtonDown("Pause"))
        {
            if(GameManager._instance.gamestate == GameManager.GameStates.play)
            {
                GameManager._instance.PauseGame();
            }
            else if(GameManager._instance.gamestate == GameManager.GameStates.pause)
            {
                GameManager._instance.ResumeGame();
            }
        }
    }

    public void jumpu()
    {
        // rotating = true;

        
        // animator.SetBool("jumpu", true);
        if(transform.rotation.z < 0)
        {
            animator.Play("Base Layer.fall", 0, 0);
        }
        else
        {
            animator.Play("Base Layer.fall", 0, 0.09f);
        }
        am.Play("jump");

        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpuP));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != LayerMask.NameToLayer("ScoreLine"))
        {
            GameManager._instance.LoseGame();
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ScoreLine"))
        {
            GameManager._instance.scoreUp(1);
        }
        
    }


    // IEnumerator jumpDone(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     animator.SetBool("jumpu", false);
    //     // rotating = false;
    // }
}
