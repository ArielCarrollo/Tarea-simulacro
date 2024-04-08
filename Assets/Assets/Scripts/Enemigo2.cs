using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    public float Velocidad;
    private Vector2 posicionOriginal;
    private bool siguiendo = false;
    public HealthBarController health;
     private int vida= 100;
    private void Start()
    {
        posicionOriginal = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            siguiendo = true;
        }
    }
  
    private void Update()
    {
        if (siguiendo)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            GameObject.FindGameObjectWithTag("Player").transform.position,
            Velocidad * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,
            posicionOriginal, Velocidad * Time.deltaTime);
        }
        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            siguiendo = false;
        }
        if (collision.gameObject.CompareTag("Fuego"))
        {
            vida = vida - 20;
            health.UpdateHealth(-20);
        }
    }
}
