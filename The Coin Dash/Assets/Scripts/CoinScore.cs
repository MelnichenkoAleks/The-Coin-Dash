using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText; 

    void Start()
    {
        int coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
    }
}
