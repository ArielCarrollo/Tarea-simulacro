using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class Puntuacion : MonoBehaviour
{
    public TextMeshProUGUI xd;
    public GameObject enemigo1;
    public GameObject enemigo2;
    public GameObject enemigo3;
    int pun;
    private bool enemigo1Contado = false;
    private bool enemigo2Contado = false;
    private bool enemigo3Contado = false;

    void Update()
    {
        if (enemigo1 == null && !enemigo1Contado)
        {
            pun++;
            enemigo1Contado = true; 
        }

        if (enemigo2 == null && !enemigo2Contado)
        {
            pun= pun +3;
            enemigo2Contado = true; 
        }

        if (enemigo3 == null && !enemigo3Contado)
        {
            pun++;
            enemigo3Contado = true; 
        }
        xd.text = "Puntaje: " + pun.ToString();
    }
}
