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

    [SerializeField]
    private AudioClip woodSound;
    [SerializeField]
    private AudioClip metalSound;
    private AudioSource audioPlayer;

    private void Awake()
    {
        movement = GetComponent<MovementScript>();
        audioPlayer = gameObject.AddComponent<AudioSource>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.CompareTag("Enemy")) Bounce();

        else if (hitObject.CompareTag("WoodEnemy"))
        {
            audioPlayer.clip = woodSound;
            audioPlayer.Play();
        }
        else if (hitObject.CompareTag("MetalEnemy"))
        {
            audioPlayer.clip = metalSound;
            audioPlayer.Play();
        }
    }

    private void Bounce()
    {
        savedSpeed = movement.speed;
        movement.canSwitch = false;
        movement.speed = -bounceForce;
        LeanTween.value(movement.speed, 0, bounceTime).setOnUpdate((value) => { movement.speed = value; });
        Invoke(nameof(HitBehaviour), bounceTime);
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
