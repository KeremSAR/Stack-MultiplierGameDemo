using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoxContainerController : MonoBehaviour
{
    private Vector3 stdLocalScale = new Vector3(1.2f, 0.2083715f, 0.1976075f);
    private Vector3 stdOffset= new Vector3(0f, .23f, 0);
    public GameObject[] buildingStairsObjects;
    


    public Vector3 currentChildPos;
    public float waitTime = 0f;
    public int childCount;

    public GameObject ss;

    [SerializeField]
    private StickSize x;

    private Animator stairsAnim;
    public Material red;
    

   

    // Start is called before the first frame update
    void Start()
    {
        currentChildPos = Vector3.zero;
        stairsAnim = GameObject.Find("Stairs").GetComponent<Animator>();
    }
    

    // Update is called once per frame
   
    public void addNewStair(Transform newChild)
    {

        StartCoroutine(delayNewStair(newChild));
        
    }

   
    
    IEnumerator delayNewStair(Transform newChild)
        {
            yield return new WaitForSeconds(waitTime);
            newChild.parent = this.transform;
            newChild.localPosition = currentChildPos;
            newChild.localScale = stdLocalScale;
            newChild.localEulerAngles = Vector3.zero;
            currentChildPos += stdOffset;
            
        }

    public void bagToStairs()
    {   childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            //print("Remove Offset");
           // currentChildPos -= stdOffset;
           for (int i = 0; i <= 10; i++)
           {
                  
                           
           }

           StartCoroutine(delayForBuilding());

        }
        
    }

    IEnumerator delayForBuilding()
    {
        for (int i = 1; i <= 30; i++)
        {
            yield return new WaitForSeconds(.2f);
            currentChildPos -= stdOffset;
            GameObject created = this.transform.GetChild(childCount - i).gameObject;
            created.transform.DOMove(
                new Vector3(buildingStairsObjects[i-1].transform.position.x, buildingStairsObjects[i-1].transform.position.y,
                    buildingStairsObjects[i-1].transform.position.z), .33f);
            Destroy(buildingStairsObjects[i-1].gameObject.GetComponent<MeshRenderer>());
            created.transform.SetParent(buildingStairsObjects[i-1].transform);
            created.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
            created.tag = "Untagged";
        }
        /*yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        currentChildPos -= stdOffset;
        GameObject created = this.transform.GetChild(childCount - 1).gameObject;
        created.transform.DOMove(
            new Vector3(buildingStairsObjects[0].transform.position.x, buildingStairsObjects[0].transform.position.y,
                buildingStairsObjects[0].transform.position.z), .33f);
        Destroy(buildingStairsObjects[0].gameObject.GetComponent<MeshRenderer>());
       created.transform.SetParent(buildingStairsObjects[0].transform);
       created.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        GameObject created2 = this.transform.GetChild(childCount - 2).gameObject;
        created2.transform.DOMove(
            new Vector3(buildingStairsObjects[1].transform.position.x, buildingStairsObjects[1].transform.position.y,
                buildingStairsObjects[1].transform.position.z), .33f);
        Destroy(buildingStairsObjects[1].gameObject.GetComponent<MeshRenderer>());
        created2.transform.SetParent(buildingStairsObjects[1].transform);
        created2.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created.tag = "Untagged";
        created2.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created3 = this.transform.GetChild(childCount - 3).gameObject;
        created3.transform
            .DOMove(
                new Vector3(buildingStairsObjects[2].transform.position.x,
                    buildingStairsObjects[2].transform.position.y, buildingStairsObjects[2].transform.position.z), .33f)
            ;
        created3.transform.SetParent(buildingStairsObjects[2].transform);
        Destroy(buildingStairsObjects[2].gameObject.GetComponent<MeshRenderer>());
        created3.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created3.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created4 = this.transform.GetChild(childCount - 4).gameObject;
        created4.transform.DOMove(
            new Vector3(buildingStairsObjects[3].transform.position.x, buildingStairsObjects[3].transform.position.y,
                buildingStairsObjects[3].transform.position.z), .33f);
        created4.transform.SetParent(buildingStairsObjects[3].transform);
        Destroy(buildingStairsObjects[3].gameObject.GetComponent<MeshRenderer>());
        created4.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created4.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created5 = this.transform.GetChild(childCount - 5).gameObject;
        created5.transform.DOMove(
            new Vector3(buildingStairsObjects[4].transform.position.x, buildingStairsObjects[4].transform.position.y,
                buildingStairsObjects[4].transform.position.z), .33f);
        created5.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created5.transform.SetParent(buildingStairsObjects[4].transform);
        Destroy(buildingStairsObjects[4].gameObject.GetComponent<MeshRenderer>());
        created5.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created6 = this.transform.GetChild(childCount - 6).gameObject;
        created6.transform.DOMove(
            new Vector3(buildingStairsObjects[5].transform.position.x, buildingStairsObjects[5].transform.position.y,
                buildingStairsObjects[5].transform.position.z), .33f);
        created6.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created6.transform.SetParent(buildingStairsObjects[5].transform);
        Destroy(buildingStairsObjects[5].gameObject.GetComponent<MeshRenderer>());
        created6.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created7 = this.transform.GetChild(childCount - 7).gameObject;
        created7.transform.DOMove(
            new Vector3(buildingStairsObjects[6].transform.position.x, buildingStairsObjects[6].transform.position.y,
                buildingStairsObjects[6].transform.position.z), .33f);
        created7.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created7.transform.SetParent(buildingStairsObjects[6].transform);
        Destroy(buildingStairsObjects[6].gameObject.GetComponent<MeshRenderer>());
        created7.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created8 = this.transform.GetChild(childCount - 8).gameObject;
        created8.transform.DOMove(
            new Vector3(buildingStairsObjects[7].transform.position.x, buildingStairsObjects[7].transform.position.y,
                buildingStairsObjects[7].transform.position.z), .33f);
        created8.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created8.transform.SetParent(buildingStairsObjects[7].transform);
        Destroy(buildingStairsObjects[7].gameObject.GetComponent<MeshRenderer>());
        created8.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created9 = this.transform.GetChild(childCount - 9).gameObject;
        created9.transform.DOMove(
            new Vector3(buildingStairsObjects[8].transform.position.x, buildingStairsObjects[8].transform.position.y,
                buildingStairsObjects[8].transform.position.z), .33f);
        created9.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created9.transform.SetParent(buildingStairsObjects[8].transform);
        Destroy(buildingStairsObjects[8].gameObject.GetComponent<MeshRenderer>());
        created9.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created10 = this.transform.GetChild(childCount - 10).gameObject;
        created10.transform.DOMove(
            new Vector3(buildingStairsObjects[9].transform.position.x, buildingStairsObjects[9].transform.position.y,
                buildingStairsObjects[9].transform.position.z), .33f);
        created10.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created10.transform.SetParent(buildingStairsObjects[9].transform);
        Destroy(buildingStairsObjects[9].gameObject.GetComponent<MeshRenderer>());
        created10.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created11 = this.transform.GetChild(childCount - 11).gameObject;
        created11.transform.DOMove(
            new Vector3(buildingStairsObjects[10].transform.position.x, buildingStairsObjects[10].transform.position.y,
                buildingStairsObjects[10].transform.position.z), .33f);
        created11.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created11.transform.SetParent(buildingStairsObjects[10].transform);
        Destroy(buildingStairsObjects[10].gameObject.GetComponent<MeshRenderer>());
        created11.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created12 = this.transform.GetChild(childCount - 12).gameObject;
        created12.transform.DOMove(
            new Vector3(buildingStairsObjects[11].transform.position.x, buildingStairsObjects[11].transform.position.y,
                buildingStairsObjects[11].transform.position.z), .33f);
        created12.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created12.transform.SetParent(buildingStairsObjects[11].transform);
        Destroy(buildingStairsObjects[11].gameObject.GetComponent<MeshRenderer>());
        created12.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created13 = this.transform.GetChild(childCount - 13).gameObject;
        created13.transform.DOMove(
            new Vector3(buildingStairsObjects[12].transform.position.x, buildingStairsObjects[12].transform.position.y,
                buildingStairsObjects[12].transform.position.z), .33f);
        created13.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created13.transform.SetParent(buildingStairsObjects[12].transform);
        Destroy(buildingStairsObjects[12].gameObject.GetComponent<MeshRenderer>());
        created13.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created14 = this.transform.GetChild(childCount - 14).gameObject;
        created14.transform.DOMove(
            new Vector3(buildingStairsObjects[13].transform.position.x, buildingStairsObjects[13].transform.position.y,
                buildingStairsObjects[13].transform.position.z), .33f);
        created14.transform.SetParent(buildingStairsObjects[13].transform);
        Destroy(buildingStairsObjects[13].gameObject.GetComponent<MeshRenderer>());
        created14.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created14.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created15 = this.transform.GetChild(childCount - 15).gameObject;
        created15.transform.DOMove(
            new Vector3(buildingStairsObjects[14].transform.position.x, buildingStairsObjects[14].transform.position.y,
                buildingStairsObjects[14].transform.position.z), .33f);
        created15.transform.SetParent(buildingStairsObjects[14].transform);
        Destroy(buildingStairsObjects[14].gameObject.GetComponent<MeshRenderer>());
        created15.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created15.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created16 = this.transform.GetChild(childCount - 16).gameObject;
        created16.transform.DOMove(
            new Vector3(buildingStairsObjects[15].transform.position.x, buildingStairsObjects[15].transform.position.y,
                buildingStairsObjects[15].transform.position.z), .33f);
        created16.transform.SetParent(buildingStairsObjects[15].transform);
        Destroy(buildingStairsObjects[15].gameObject.GetComponent<MeshRenderer>());
        created16.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created16.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created17 = this.transform.GetChild(childCount - 17).gameObject;
        created17.transform.DOMove(
            new Vector3(buildingStairsObjects[16].transform.position.x, buildingStairsObjects[16].transform.position.y,
                buildingStairsObjects[16].transform.position.z), .33f);
        created17.transform.SetParent(buildingStairsObjects[16].transform);
        Destroy(buildingStairsObjects[16].gameObject.GetComponent<MeshRenderer>());
        created17.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created17.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created18 = this.transform.GetChild(childCount - 18).gameObject;
        created18.transform.DOMove(
            new Vector3(buildingStairsObjects[17].transform.position.x, buildingStairsObjects[17].transform.position.y,
                buildingStairsObjects[17].transform.position.z), .33f);
        created18.transform.SetParent(buildingStairsObjects[17].transform);
        Destroy(buildingStairsObjects[17].gameObject.GetComponent<MeshRenderer>());
        created18.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created18.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created19 = this.transform.GetChild(childCount - 19).gameObject;
        created19.transform.DOMove(
            new Vector3(buildingStairsObjects[18].transform.position.x, buildingStairsObjects[18].transform.position.y,
                buildingStairsObjects[18].transform.position.z), .33f);
        created19.transform.SetParent(buildingStairsObjects[18].transform);
        Destroy(buildingStairsObjects[18].gameObject.GetComponent<MeshRenderer>());
        created19.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created19.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created20 = this.transform.GetChild(childCount - 20).gameObject;
        created20.transform.DOMove(
            new Vector3(buildingStairsObjects[19].transform.position.x, buildingStairsObjects[19].transform.position.y,
                buildingStairsObjects[19].transform.position.z), .33f);
        created20.transform.SetParent(buildingStairsObjects[19].transform);
        Destroy(buildingStairsObjects[19].gameObject.GetComponent<MeshRenderer>());
        created20.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created20.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created21 = this.transform.GetChild(childCount - 21).gameObject;
        created21.transform.DOMove(
            new Vector3(buildingStairsObjects[20].transform.position.x, buildingStairsObjects[20].transform.position.y,
                buildingStairsObjects[20].transform.position.z), .33f);
        created21.transform.SetParent(buildingStairsObjects[20].transform);
        Destroy(buildingStairsObjects[20].gameObject.GetComponent<MeshRenderer>());
        created21.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created21.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created22 = this.transform.GetChild(childCount - 22).gameObject;
        created22.transform.DOMove(
            new Vector3(buildingStairsObjects[21].transform.position.x, buildingStairsObjects[21].transform.position.y,
                buildingStairsObjects[21].transform.position.z), .33f);
        created22.transform.SetParent(buildingStairsObjects[21].transform);
        Destroy(buildingStairsObjects[21].gameObject.GetComponent<MeshRenderer>());
        created22.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created22.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;    
        GameObject created23 = this.transform.GetChild(childCount - 23).gameObject;
        created23.transform.DOMove(
            new Vector3(buildingStairsObjects[22].transform.position.x, buildingStairsObjects[22].transform.position.y,
                buildingStairsObjects[22].transform.position.z), .33f);
        created23.transform.SetParent(buildingStairsObjects[22].transform);
        Destroy(buildingStairsObjects[22].gameObject.GetComponent<MeshRenderer>());
        created23.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created23.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created24 = this.transform.GetChild(childCount - 24).gameObject;
        created24.transform.DOMove(
            new Vector3(buildingStairsObjects[23].transform.position.x, buildingStairsObjects[23].transform.position.y,
                buildingStairsObjects[23].transform.position.z), .33f);
        created24.transform.SetParent(buildingStairsObjects[23].transform);
        Destroy(buildingStairsObjects[23].gameObject.GetComponent<MeshRenderer>());
        created24.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created24.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created25 = this.transform.GetChild(childCount - 25).gameObject;
        created25.transform.DOMove(
            new Vector3(buildingStairsObjects[24].transform.position.x, buildingStairsObjects[24].transform.position.y,
                buildingStairsObjects[24].transform.position.z), .33f);
        created25.transform.SetParent(buildingStairsObjects[24].transform);
        Destroy(buildingStairsObjects[24].gameObject.GetComponent<MeshRenderer>());
        created25.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created25.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created26 = this.transform.GetChild(childCount - 26).gameObject;
        created26.transform.DOMove(
            new Vector3(buildingStairsObjects[25].transform.position.x, buildingStairsObjects[25].transform.position.y,
                buildingStairsObjects[25].transform.position.z), .33f);
        created26.transform.SetParent(buildingStairsObjects[25].transform);
        Destroy(buildingStairsObjects[25].gameObject.GetComponent<MeshRenderer>());
        created26.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created26.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created27 = this.transform.GetChild(childCount - 27).gameObject;
        created27.transform.DOMove(
            new Vector3(buildingStairsObjects[26].transform.position.x, buildingStairsObjects[26].transform.position.y,
                buildingStairsObjects[26].transform.position.z), .33f);
        created27.transform.SetParent(buildingStairsObjects[26].transform);
        Destroy(buildingStairsObjects[26].gameObject.GetComponent<MeshRenderer>());
        created27.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created27.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created28 = this.transform.GetChild(childCount - 28).gameObject;
        created28.transform.DOMove(
            new Vector3(buildingStairsObjects[27].transform.position.x, buildingStairsObjects[27].transform.position.y,
                buildingStairsObjects[27].transform.position.z), .33f);
        created28.transform.SetParent(buildingStairsObjects[27].transform);
        Destroy(buildingStairsObjects[27].gameObject.GetComponent<MeshRenderer>());;
        created28.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        created28.tag = "Untagged";
        yield return new WaitForSeconds(.2f);
        currentChildPos -= stdOffset;
        GameObject created29 = this.transform.GetChild(childCount - 29).gameObject;
        created29.transform.DOMove(
            new Vector3(buildingStairsObjects[28].transform.position.x, buildingStairsObjects[28].transform.position.y,
                buildingStairsObjects[28].transform.position.z), .33f);
        created29.transform.SetParent(buildingStairsObjects[28].transform);
        Destroy(buildingStairsObjects[28].gameObject.GetComponent<MeshRenderer>());
        created29.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        currentChildPos -= stdOffset;
        GameObject created30 = this.transform.GetChild(childCount - 30).gameObject;
        created30.transform.DOMove(
            new Vector3(buildingStairsObjects[29].transform.position.x, buildingStairsObjects[29].transform.position.y,
                buildingStairsObjects[29].transform.position.z), .33f);
        created30.transform.SetParent(buildingStairsObjects[29].transform);
        Destroy(buildingStairsObjects[29].gameObject.GetComponent<MeshRenderer>());
        created30.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
        currentChildPos -= stdOffset;
        created29.tag = "Untagged";
        created30.tag = "Untagged";*/
    }
  

    public int getTotalCollectedStairPices()
    {
        return this.transform.childCount;
    }

    public void removeStair()
    {
        childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            print("Remove Offset");
            currentChildPos -= stdOffset;
            Destroy(this.transform.GetChild(childCount - 1).gameObject);
        }
       
    }

    public void woodObstacle()
    {
       // this.transform.GetChild(childCount - 1).gameObject.transform.parent = null;
       childCount = getTotalCollectedStairPices();
       if (childCount > 0)
       {
           this.transform.GetChild(childCount-1).gameObject.GetComponent<Rigidbody>().isKinematic = false;
           this.transform.GetChild(childCount - 1).gameObject.GetComponent<Collider>().isTrigger = false;
           StartCoroutine(delayForDestroying());
       }
    }

    public void bonusGround()
    {
        childCount = getTotalCollectedStairPices();
        if (childCount > 0)
        {
            /*for (int i = -1; i < childCount; i++)
            {
                this.transform.GetChild(childCount-1).gameObject.GetComponent<StairController>().popUP();
                this.transform.GetChild(childCount - 1).gameObject.transform.parent = ss.transform;

            }*/
            this.transform.GetChild(childCount-1).gameObject.GetComponent<StairController>().popUP();
            Destroy(this.transform.GetChild(childCount - 1));

        }
    }
    IEnumerator delayForDestroying()
    {
        yield return new WaitForSeconds(1.5f);
        removeStair();
    }

   
    
    private void Update()
    {
        
        
    }
}



