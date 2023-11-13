using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [field: SerializeField]
    public int coins { get; private set; } = 0;

    [SerializeField]
    private int coinBoostCount = 3;
    private int coinCounter = 0;
    [SerializeField]
    private float coinBoostAmount = 0.75f;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Coin"))
        {
            coins += 1;
            obj.SetActive(false);
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
            print("boost");
        }
    }
}
