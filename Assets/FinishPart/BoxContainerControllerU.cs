using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainerControllerU : MonoBehaviour
{
    private Vector3 stdLocalScale = new Vector3(1.2f, 0.2083715f, 0.1976075f);
 
    private Vector3 stdOffsetU = new Vector3(.53f, 0,0);

    public bool isActive;
    
   


    public Vector3 currentChildPos;
    public float waitTime = 0f;
   

    public int amounttt;

    // Start is called before the first frame update
    void Start()
    {
        currentChildPos = Vector3.zero;
    }

    // Update is called once per frame
   
   

    public void addNewStairU(Transform newChild)
    {
        StartCoroutine(delayNewStairU(newChild));
    }
  
    
   

    IEnumerator delayNewStairU(Transform newChild)
    {
        yield return new WaitForSeconds(waitTime);
        newChild.parent = this.transform;
        newChild.localPosition = currentChildPos;
        newChild.localScale = stdLocalScale;
        newChild.localEulerAngles = Vector3.zero;
        currentChildPos += stdOffsetU;

    }
    

    public int getTotalCollectedStairPices()
    {
        return this.transform.childCount;
    }

  /*  public void removeStairU()
    {
        int childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            print("Remove Offset");

            for (int i = 0; i <= DetectShapes.instance.pointsAdded; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                
                if (this.transform.GetChild(i).gameObject.activeSelf)
                {
                    
                }
              
                if (this.transform.GetChild(i).gameObject.activeSelf && DetectShapes.instance.anim2.isActiveAndEnabled)
                {
                   // StartCoroutine(destroyAnimatedStick());
                }
                
                
            }

        }*/

        IEnumerator destroyAnimatedStick(GameObject q)
        {
            yield return new WaitForSeconds(3f);
            Destroy(q.gameObject);
            
        }

    }


