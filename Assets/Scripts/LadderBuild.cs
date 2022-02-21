using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LadderBuild : MonoBehaviour
{
   public int ladderCount;
   public Transform ladder;
   public float secondsBetweenLadderSpawns;
   public Vector3 ladderSpawnOffset;

   private int spawnLadders = 0;
   

   private Transform characterParentTransform;
   private bool transformEfected;

   private Animator playerAnimator;
   private bool ladderAborted;

   public BoxContainerController boxContainerController;

   private void Start()
   {
      playerAnimator = GameObject.Find("untitled").GetComponent<Animator>();
      ladderAborted = false;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         if (!transformEfected)
         {
            playerAnimator.SetTrigger("LadderStart");
            characterParentTransform = other.transform;
            transformEfected = true;
//            sil2Ghost.instance.ChangePos();

            StartCoroutine(BuildLadder());
         }
      }
   }

   private IEnumerator BuildLadder()
   {
      DetectShapes.instance.isRunnig = false;
      DetectShapes.instance.ZaxisVelocity = 0f;
      for (int i = 0; i < ladderCount; i++)
      {
         if (!ladderAborted)
         {
            Instantiate(ladder, characterParentTransform.position + ladderSpawnOffset, Quaternion.identity);
            
           
            characterParentTransform.DOMoveY(characterParentTransform.position.y + (ladderSpawnOffset.y),
               secondsBetweenLadderSpawns);

            spawnLadders++;
            boxContainerController.removeStair();
            PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);

            if (spawnLadders % 6 == 0)
            {
               if (PlayerMovement.instance.list.Count == 0)
               {
                  ladderAborted = true;
                  Debug.Log("ÇANTADA POLE KALMADI AMK");
                  
                  //TODO: ÇANTADA POLE OLMAYIP MERDİVEN KURMAYA ÇALIŞINCA NE OLACAK?
                  
                  yield break;
               }
            }
            yield return new WaitForSeconds(secondsBetweenLadderSpawns);
         }
      } 
      
      playerAnimator.SetTrigger("LadderFinished");
      DetectShapes.instance.isRunnig = true;
      DetectShapes.instance.ZaxisVelocity = 5f;
     
     
      
      
   }
}
