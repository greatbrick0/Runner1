using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [field: SerializeField]
    public int coins { get; private set; } = 0;
}
