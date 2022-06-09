using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWalletManager : MonoBehaviour
{
    [Header("General Configuration")]
    [SerializeField]
    private int startingCoins;
    [Header("Required Components")]
    [SerializeField]
    private TextMeshProUGUI coinsText;
    


    public int Coins { get; private set; }

    private void Start()
    {
        Coins = startingCoins;
        coinsText.text = Coins.ToString() + "G";
    }

    public void Purchase(int cost)
    {
        Coins -= cost;
        coinsText.text = Coins.ToString() + "G";
    }

    public bool CanPurchase(int cost)
    {
        if (cost <= Coins)
            return true;
        return false;
    }

    public void AddCoins(int coinsToAdd)
    {
        Coins += coinsToAdd;
        coinsText.text = Coins.ToString() + "G";
    }
}
