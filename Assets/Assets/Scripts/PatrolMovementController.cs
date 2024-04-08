using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    private float detectedVelocityModifier = 7f;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private float raycastDistance = 2f;
    private bool playerDetected = false;
    public HealthBarController health;
    private int vida = 100;
    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        CheckNewPoint();
        UpdateRaycast();
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        if (vida == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }
    private void UpdateRaycast()
    {
        Vector2 direction = myRBD2.velocity.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance);
        Debug.DrawRay(transform.position, direction * raycastDistance, Color.green);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }
    private void UpdateVelocity()
    {
        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * GetCurrentVelocityModifier();
    }
    private float GetCurrentVelocityModifier()
    {
        return playerDetected ? detectedVelocityModifier : velocityModifier;
    }
    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fuego"))
        {
            vida = vida - 20;
            health.UpdateHealth(-20);
        }
    }
}
