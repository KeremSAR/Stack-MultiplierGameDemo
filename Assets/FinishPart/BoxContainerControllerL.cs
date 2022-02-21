using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainerControllerL : MonoBehaviour
{
    private Vector3 stdLocalScale = new Vector3(1.2f, 0.2083715f, 0.1976075f);
  
    private Vector3 stdOffsetL = new Vector3(.53f,0,0);


    public Vector3 currentChildPos;
    public float waitTime = 0f;


    public int amounttt;

    // Start is called before the first frame update
    void Start()
    {
        currentChildPos = Vector3.zero;
    }

    // Update is called once per frame
   
  
    public void addNewStairL(Transform newChild)
    {
        StartCoroutine(delayNewStairL(newChild));
    }
    
 
    
    IEnumerator delayNewStairL(Transform newChild)
    {
        yield return new WaitForSeconds(waitTime);
        newChild.parent = this.transform;
        newChild.localPosition = currentChildPos;
        newChild.localScale = stdLocalScale;
        newChild.localEulerAngles = Vector3.zero;
        currentChildPos += stdOffsetL;

    }

   
    

    public int getTotalCollectedStairPices()
    {
        return this.transform.childCount;
    }

   /* public void removeStairL()
    {
        int childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            print("Remove Offset");
            //currentChildPos -= stdOffsetL;
            //Destroy(this.transform.GetChild(childCount - 1).gameObject);
            for (int i = 0; i <= DetectShapes.instance.pointsLeftAdded; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);    
            }
    
        }

    }*/

}
