using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Distance : MonoBehaviour
{
    private float timer = 0f;
    public TextMeshProUGUI contadorText;
    void Update()
    {
        timer += Time.deltaTime;
        contadorText.text = "Distancia: " + timer.ToString("F0");
    }
}
