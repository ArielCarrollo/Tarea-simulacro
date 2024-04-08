using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2 movementPlayer;
    public HealthBarController healthController;
    public GameObject enemigo1;
    public GameObject enemigo2;
    public GameObject enemigo3;
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f;
    private int vida = 100;
    public int ganaste;
    private void Awake()
    {
        //healthController = GetComponent<HealthBarController>();
    }
    private void Update() {

        myRBD2.velocity = movementPlayer * velocityModifier;
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);
    
        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Right Click");
            FireProjectile(mouseInput);
        }
        else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Left Click");
        }
        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
        ganar();
    }
    private void FireProjectile(Vector2 Fuego)
    {
        Vector2 direction = (Fuego - (Vector2)transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;
        Destroy(projectile, 3f);
    }
    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void Onmovement(InputAction.CallbackContext contex) 
    {
        movementPlayer = contex.ReadValue<Vector2>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            vida = vida - 20;
            healthController.UpdateHealth(-20);
        }
        if (collision.gameObject.CompareTag("e")) 
        {
            if (collision.gameObject == enemigo1 || collision.gameObject == enemigo2)
            {
                vida = vida - 20;
                healthController.UpdateHealth(-20); 
            }
        }
    }
    void ganar()
    {
        if (enemigo1 == null&& enemigo3 == null && enemigo2 == null)
        {
            ganaste = ganaste + 3;
            PlayerPrefs.SetInt("Puntaje", ganaste);
        }
       
        if(ganaste == 3)
        {
          SceneManager.LoadScene("Ganaste");
         
        }
    }
}
