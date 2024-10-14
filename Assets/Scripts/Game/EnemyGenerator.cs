using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator instance;
    public List<GameObject> Enemies = new List<GameObject>(); 
    private float time_to_create = 4f; 
    private float actual_time = 0f;
    private float limitSuperior;
    private float limitInferior;
    public List<GameObject> actual_enemies = new List<GameObject>();
    [SerializeField] private AudioSource sfxEnemyHurt;
    [SerializeField] private AudioSource sfxEnemyHit;
    [SerializeField] SoundConfig sfxHurt;
    [SerializeField] SoundConfig sfxHit;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
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
            //SceneManager.LoadScene("GameOver");
        }
        sfxEnemyHurt.clip = sfxHurt.SoundClip;
        sfxEnemyHit.clip = sfxHit.SoundClip;
        sfxEnemyHurt.Play();
        sfxEnemyHit.Play();
        Destroy(enemy_script.gameObject);
    }
}