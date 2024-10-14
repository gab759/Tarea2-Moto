using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    private Vector2 moveInput;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void AddPoints(int cantidad)
    {
        score += cantidad;
        scoreText.text = "Puntaje: " + score.ToString("F0");
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
    }
}
