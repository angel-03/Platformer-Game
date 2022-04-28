using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontalMovement = 0f;
    private bool isRight;
    private int count=0;
    int score=0;
    
    public float speed = 5f;
    public float jumpForce=7f;
    public Text scoreText;

    public GameObject startPanel;
    public GameObject gameOverPanel;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f) 
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);   
        }
    }

    void FixedUpdate()
    {
        Movement();      
    }

    void Movement()
    {
        transform.position += new Vector3(horizontalMovement, 0,0) * speed * Time.deltaTime;
        if(isRight && horizontalMovement > 0f)
        {
            transform.localScale = new Vector3(5,5,1);
            isRight = false;
        }
        else if(!isRight && horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-5,5,1);
            isRight = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
     if( collision.gameObject.tag == "Bullets" || collision.gameObject.tag == "Spikes")
     {
        Time.timeScale = 0;
        //Destroy(this.gameObject);
        gameOverPanel.SetActive(true);
        //Application.Quit();
     }
     else if( collision.gameObject.tag == "Gems")
     {
        Destroy(collision.gameObject);
        count++;
        score = count * 20;
        Debug.Log(count);
        scoreText.text = "Score: " + score;
        if(count == 6)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Debug.Log("You Won!!!");
            //Application.Quit();
        }
     }
    }

    public void OnClickStartButton()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);

    }
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
