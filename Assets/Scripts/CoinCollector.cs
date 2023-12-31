using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [field: SerializeField]
    public int coins { get; private set; } = 0;

    [Header("Coin Boost")]
    [SerializeField]
    private int coinBoostCount = 3;
    private int coinCounter = 0;
    [SerializeField]
    private float coinBoostAmount = 0.75f;

    [Header("Sounds")]
    [SerializeField]
    private List<AudioClip> collectSounds;
    [SerializeField]
    private AudioClip boostSound;
    private AudioSource audioPlayer;
    private AudioSource boostPlayer;

    private void Awake()
    {
        audioPlayer = gameObject.AddComponent<AudioSource>();
        boostPlayer = gameObject.AddComponent<AudioSource>();
        boostPlayer.clip = boostSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Coin"))
        {
            coins += 1;
            obj.SetActive(false);
            audioPlayer.clip = RandomCollectSound();
            audioPlayer.Play();

            Boost();
        }
    }

    private void Boost()
    {
        coinCounter += 1;
        if (coinCounter >= coinBoostCount)
        {
            GetComponent<MovementScript>().speed += coinBoostAmount;
            coinCounter = 0;
            boostPlayer.Play();
        }
    }

    private AudioClip RandomCollectSound()
    {
        return collectSounds[Random.Range(0, collectSounds.Count-1)];
    }
}
