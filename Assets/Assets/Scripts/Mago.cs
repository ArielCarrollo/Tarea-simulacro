using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float Velocidad;
    private Vector2 posicionOriginal;
    private bool siguiendo = false;
    public Transform firePoint;
    private float nextFireTime;
    public HealthBarController health;
    private int vida = 100;
    private void Start()
    {
        posicionOriginal = transform.position;
        nextFireTime = Time.time;
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
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + 2f;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, posicionOriginal,  Velocidad * Time.deltaTime);
        }
        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            siguiendo = false;
        }
        if (collision.gameObject.CompareTag("Fuego"))
        {
            vida = vida - 20;
            health.UpdateHealth(-20);
        }
    }
    private void ShootProjectile()
    {
        Vector2 targetDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = targetDirection * projectileSpeed;
            Destroy(projectile, 5f);
        }
    }
}
