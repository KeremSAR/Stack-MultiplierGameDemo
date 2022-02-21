using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class StairController : MonoBehaviour
{
    //Serialized Data
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private string color;
    //Private Data
    private Vector3 localPostion;
    public bool canbeCollected = true;
    public UnityEvent onCollect;
   
    // Start is called before the first frame update
    void Start()
    {
        localPostion = this.transform.localPosition;
        
    }

    // Update is called once per frame
    
    public void popUP()
    {
         //StairsManager.instance.makeNewChild(localPostion,"Yellow");
        anim.SetBool("PopUp", true);
        Invoke("d", 3f);
    }

    public void d()
    {
        anim.SetBool("PopUp", false);
    }

    public void Collect()
    {
        onCollect?.Invoke();
       }
    
    
    
    
    public string getColor()
    {
        return color;
    }

    
}
