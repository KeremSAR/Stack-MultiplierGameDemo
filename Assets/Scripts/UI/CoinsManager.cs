using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TMP_Text coinUIText;

    public int Amount = 0;

    public int Coins
    {
        get { return Amount; }
        set
        {
            Amount = value;
            coinUIText.text = Coins.ToString();
        }
        
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void RemoveCoins(int amo)
    {
        Coins -= amo;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
