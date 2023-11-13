using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class HealthScript : MonoBehaviour
{
    [SerializeField] MenuManager mm;
    bool isDead;

    private MovementScript movement;
    [SerializeField]
    private float bounceForce = 3;
    [SerializeField]
    private float bounceTime = 0.8f;
    [SerializeField]
    private float returnTime = 0.8f;
    [SerializeField]
    private int health = 1;
    private float savedSpeed;

    private void Awake()
    {
        movement = GetComponent<MovementScript>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            savedSpeed = movement.speed;
            movement.canSwitch = false;
            movement.speed = -bounceForce;
            LeanTween.value(movement.speed, 0, bounceTime).setOnUpdate((value) => { movement.speed = value; });
            Invoke(nameof(HitBehaviour), bounceTime);
        }
    }

    private void HitBehaviour()
    {
        health -= 1;

        if(health == 0) mm.SetStateTwo(isDead = true);
        else
        {
            movement.canSwitch = true;
            LeanTween.value(movement.speed, savedSpeed, returnTime).setOnUpdate((value) => { movement.speed = value; });
        }
    }
}
