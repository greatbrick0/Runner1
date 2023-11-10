using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthScript : MonoBehaviour
{
    private MovementScript movement;
    [SerializeField]
    private float bounceForce = 3;
    [SerializeField]
    private float bounceTime = 0.8f;

    private void Awake()
    {
        movement = GetComponent<MovementScript>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            movement.speed = -bounceForce;
            LeanTween.value(movement.speed, 0, bounceTime).setOnUpdate((value) => { movement.speed = value; });
        }
    }
}
