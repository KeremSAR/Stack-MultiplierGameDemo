using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace poleview
{
    public class PoleView : MonoBehaviour
    {
        //[SerializeField] public float heightIncriseAmount;
        [SerializeField] public float currentHeight;
        [SerializeField] float increaseTime;
        
    
        public void IncriseHeight(float multiply = 1f)
        {
            currentHeight =  currentHeight + multiply;
        }
        public void DecriseHeight(float multiply = 1f)
        {
            if(currentHeight > 1)
            {
                currentHeight -=  currentHeight + multiply;
            }
            /*else
            {
                Controller.instance.gameController.Restart();
            }*/
        }
        private void Update()
        {
            if(transform.localScale.y != currentHeight)
            {
              //  transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, currentHeight, transform.localScale.z), increaseTime * Time.deltaTime);
                
            }
        }
    }

}
