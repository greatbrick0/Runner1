using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [field: SerializeField]
    public int coins { get; private set; } = 0;

    [SerializeField]
    int coinBoostCount = 3;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Coin"))
        {
            coins += 1;
            obj.SetActive(false);
        }
    }
}
