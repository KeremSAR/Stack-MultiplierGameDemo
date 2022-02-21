using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainerControllerR : MonoBehaviour
{
    private Vector3 stdLocalScale = new Vector3(1.2f, 0.2083715f, 0.1976075f);
 
    private Vector3 stdOffsetR = new Vector3(-.53f, 0,0);
   


    public Vector3 currentChildPos;
    public float waitTime = 0f;
   

    public int amounttt;

    // Start is called before the first frame update
    void Start()
    {
        currentChildPos = Vector3.zero;
    }

    // Update is called once per frame
   
   

    public void addNewStairR(Transform newChild)
    {
        StartCoroutine(delayNewStairR(newChild));
    }
  
    
   

    IEnumerator delayNewStairR(Transform newChild)
    {
        yield return new WaitForSeconds(waitTime);
        newChild.parent = this.transform;
        newChild.localPosition = currentChildPos;
        newChild.localScale = stdLocalScale;
        newChild.localEulerAngles = Vector3.zero;
        currentChildPos += stdOffsetR;

    }
    

    public int getTotalCollectedStairPices()
    {
        return this.transform.childCount;
    }

   /* public void removeStairR()
    {
        int childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            print("Remove Offset");
           // currentChildPos -= stdOffsetR;
            //Destroy(this.transform.GetChild(childCount - 1).gameObject);
            for (int i = 0; i <= DetectShapes.instance.pointsRightAdded; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);    
            }
              
        }

    }
*/
}
