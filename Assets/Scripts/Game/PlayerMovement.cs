using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    [SerializeField]
    private float speed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LifeText;
    private Vector2 moveInput;
    private bool isInvulnerable = false;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangeScene();
    }
    void AddPoints(int cantidad)
    {
        score += cantidad;
        scoreText.text = "Puntaje: " + score.ToString("F0");
    }
    void UpdateLife(int damage)
    {
        player_lives -= damage;
        LifeText.text = "Vida: " + player_lives.ToString("F0");
    }
    private void Movement()
    {
        if (moveInput.y > 0 && transform.position.y < limitSuperior)
        {
            myRB.velocity = new Vector2(0f, moveInput.y * speed);
        }
        else if (moveInput.y < 0 && transform.position.y > limitInferior)
        {
            myRB.velocity = new Vector2(0f, moveInput.y * speed);
        }
        else
        {
            myRB.velocity = Vector2.zero;
        }
    }
    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            CandyController candy = other.gameObject.GetComponent<CandyController>();
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
            AddPoints(candy.points);
        }
        else if (other.tag == "Enemy" && !isInvulnerable)
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            EnemyGenerator.instance.ManageEnemy(other.gameObject.GetComponent<EnemyController>(), this);
            UpdateLife(enemy.damage);
            StartCoroutine(Invulnerable());
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
    private void ChangeScene()
    {
        if(player_lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    private IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f); 
        isInvulnerable = false;
    }
}
