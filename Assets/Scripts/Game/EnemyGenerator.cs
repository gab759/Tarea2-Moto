using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>(); 
    private float time_to_create = 4f; 
    private float actual_time = 0f;
    
    [SerializeField] private AudioSource hitSfx; 
    private float limitSuperior;
    private float limitInferior;
    public List<GameObject> actual_enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        actual_time += Time.deltaTime;

        if (time_to_create <= actual_time)
        {
            GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)],
            new Vector3(transform.position.x, Random.Range(limitInferior, limitSuperior), 0f), Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);
            actual_time = 0f;
            actual_enemies.Add(enemy);
        }
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -(bounds.y * 0.9f); 
        limitSuperior = (bounds.y * 0.9f); 
    }

    public void ManageEnemy(EnemyController enemy_script, PlayerMovement player_script = null)
    {
        if (player_script == null)
        {
            Destroy(enemy_script.gameObject);
            return;
        }

        int lives = player_script.player_lives;

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        Destroy(enemy_script.gameObject);
    }
}