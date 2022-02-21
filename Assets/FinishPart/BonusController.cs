using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    private Vector3 stdLocalScale = new Vector3(1f, 0.01f, 0.1976075f);
    private Vector3 stdOffset= new Vector3(0f, .01f, 0);
    
    public Vector3 currentChildPos;
    public float waitTime = 0f;
    public int childCount;

    public static BonusController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentChildPos = Vector3.zero;
    }

    public void addNewStack(Transform newChild)
    {

        StartCoroutine(delayNewStack(newChild));
        
    }
    
    IEnumerator delayNewStack(Transform newChild)
    {
        yield return new WaitForSeconds(waitTime);
        newChild.parent = this.transform;
        newChild.localPosition = currentChildPos;
        newChild.localScale = stdLocalScale;
        newChild.localEulerAngles = Vector3.zero;
        currentChildPos += stdOffset;
    
    }
    
    public int getTotalCollectedStairPices()
    {
        return this.transform.childCount;
    }
    
    public void bonusGround()
    {
        childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            for (int i = -1; i < childCount; i++)
            {
                this.transform.GetChild(childCount-1).gameObject.GetComponent<StairController>().popUP();
                addNewStack(this.transform.GetChild(childCount-1).gameObject.transform);
            }
            
            
            
        }
    }
}
