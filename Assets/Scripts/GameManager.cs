using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance=null;
    public enum GameStates
    {
        pause,lose,play,start
    }

    public GameStates gamestate;
    public GameObject pipu;
    public GameObject guroundo;
    public BoxCollider2D groundCollider;
    private float groundHorizontalLength;
    public float minPosBlk = -2, maxPosBlk = 2;
    public float spawnTime = 1.5f;
    [SerializeField]private int score = 0;
    public Text scoreText;
    public Text TutorialText;
    public GameObject PauseBtn;
    public GameObject ResumeBtn;
    public GameObject ReStartBtn;
    private AudioManager am;
    // private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance==null)
        {
            _instance = this;
            am = FindObjectOfType<AudioManager>();
        }
        else
        {
            Destroy(gameObject);
        }
        groundHorizontalLength = groundCollider.size.x;
        Instantiate(guroundo, new Vector3(-groundHorizontalLength *2, -5.5f, 0), Quaternion.identity);
        Instantiate(guroundo, new Vector3(-groundHorizontalLength, -5.5f, 0), Quaternion.identity);
        Instantiate(guroundo, new Vector3(0, -5.5f, 0), Quaternion.identity);
        Instantiate(guroundo, new Vector3(groundHorizontalLength, -5.5f, 0), Quaternion.identity);
        Instantiate(guroundo, new Vector3(groundHorizontalLength *2, -5.5f, 0), Quaternion.identity);
        StartCoroutine(genereeto(spawnTime));
        scoreText.text = "Score: " + score;
                scoreText.enabled = false;
                TutorialText.enabled = true;
                PauseBtn.SetActive(false);
                ResumeBtn.SetActive(false);
                ReStartBtn.SetActive(false);
        Time.timeScale = 0;
        _instance.gamestate = GameStates.start;
    }

    // Update is called once per frame
    void Update()
    {
        // UI handling
        // switch (gamestate)
        // {
        //     case GameStates.start:{
        //         scoreText.enabled = false;
        //         PauseBtn.SetActive(false);
        //         ResumeBtn.SetActive(false);
        //         ReStartBtn.SetActive(true);
        //         break;
        //     }
        //     case GameStates.play:{
        //         scoreText.enabled = true;
        //         PauseBtn.SetActive(true);
        //         ResumeBtn.SetActive(false);
        //         ReStartBtn.SetActive(false);
        //         break;
        //     }
        //     case GameStates.pause:{
        //         scoreText.enabled = false;
        //         PauseBtn.SetActive(false);
        //         ResumeBtn.SetActive(true);
        //         ReStartBtn.SetActive(false);
        //         break;
        //     }
        //     case GameStates.lose:{
        //         scoreText.enabled = true;
        //         PauseBtn.SetActive(false);
        //         ResumeBtn.SetActive(false);
        //         ReStartBtn.SetActive(false);
        //         break;
        //     }
        //     default:{
        //         scoreText.enabled = false;
        //         PauseBtn.SetActive(false);
        //         ResumeBtn.SetActive(false);
        //         ReStartBtn.SetActive(false);
        //         break;
        //     }
        // }
    }

    public void PauseGame()
    {
        _instance.gamestate = GameStates.pause;
        Time.timeScale = 0;
                scoreText.enabled = false;
                PauseBtn.SetActive(false);
                ResumeBtn.SetActive(true);
                ReStartBtn.SetActive(false);
    }

    public void ResumeGame()
    {
        _instance.gamestate = GameStates.play;
        Time.timeScale = 1;
                scoreText.enabled = true;
                TutorialText.enabled = false;
                PauseBtn.SetActive(true);
                ResumeBtn.SetActive(false);
                ReStartBtn.SetActive(false);
    }

    public void LoseGame()
    {
        _instance.gamestate = GameStates.lose;
        Time.timeScale = 0;
                scoreText.enabled = true;
                PauseBtn.SetActive(false);
                ResumeBtn.SetActive(false);
                ReStartBtn.SetActive(true);
    }

    public void RestartGame()
    {
        _instance.gamestate = GameStates.play;
        _instance.ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator genereeto(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Instantiate(pipu, new Vector3(0, Random.Range(minPosBlk,maxPosBlk), 0), Quaternion.identity);
        }
    }

    public void scoreUp(int x)
    {
        this.score += x;
        scoreText.text = "Score: " + score;
        am.Play("coin");
    }
}
