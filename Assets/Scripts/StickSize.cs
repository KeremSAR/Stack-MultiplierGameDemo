using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSize : MonoBehaviour
{
    [SerializeField] float heightIncriseAmount;
    [SerializeField] public float currentHeight;
    [SerializeField] float incriseTime;
    
    void Start()
    {
        
    }
    
    public void IncriseHeight(float multiply = 1f)
    {
        currentHeight += heightIncriseAmount * multiply;
    }

    
    private void Update()
    {
        if(transform.localScale.y != currentHeight)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, currentHeight, transform.localScale.z), incriseTime * Time.deltaTime);
        }
      
    }
}
