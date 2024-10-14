using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage = 1;
    void Update()
    {
        if (transform.position.x <= -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x)
        {
            EnemyGenerator.instance.ManageEnemy(this);
        }
    }
}