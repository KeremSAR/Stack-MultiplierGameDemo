using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LockControl : MonoBehaviour
{
    public static LockControl instance;
    private PointsScript points;
    

    private int codeLength;
    public int placeInCode;

    public string code = "";
    public string attemptedCode;

    public GameObject[] array;
    public List<String> arx = new List<string>();
    private List<int> z = new List<int>();
    public string[] variables;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        codeLength = code.Length;
    }
    

    public void SetValue(string value)
    {
        placeInCode++;

        if (placeInCode <= codeLength)
        {
            attemptedCode += value;
        }
        

        if (placeInCode == codeLength)
        {
            CheckCode();

            attemptedCode = "";
            placeInCode = 0;
        }
        
    }

    private void CheckCode()
    {
        if (attemptedCode == code)
        {
            Debug.Log("True");
           
        }
        else
        {
            Debug.Log("False");
            
        }
    }
}