using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DetectShapes : MonoBehaviour
{
    /*[TabGroup("pointsAdded")] public int pointsAdded = 0,
        pointsToCollect = 0,
        pointsLeftAdded = 0,
        pointsRightAdded = 0,
        pointsDownAdded = 0,
        pointsForLockSystem = 0,
        pointsMiddleAdded = 0,
        pointsWiresAdded = 0,
        pointsShieldAdded = 0;
    public List<PointsScript> points = new List<PointsScript>();
    [TabGroup("pointsParent")] public Transform pointsUPParent,
        pointsLeftParent,
        pointsRightParent,
        pointsDownParent,
        pointsMiddleParent,
        pointsWiresParent,
        pointsShieldParent;
    public LineRenderer LinePrefab;
    public int lineLength = 0, totalLineLength;*/
    [TabGroup("GameObjects")] public GameObject rightPart;
    public Animator anim;
    public Animator anim2;
    private Animator anim3;
    private Animator anim4;
    public bool isRunnig = true;
    [TabGroup("GameObjects")] public GameObject Player;
    public Rigidbody PlayerRigidbody;
    private bool ToLeft = false;
    private bool ToRight = false;
    private bool ToDown = false;
    private bool IsPoleAnim = false;
    public float HorizontalVelocity, ZaxisVelocity;
    public static DetectShapes instance;

    public BoxContainerController boxContainerController;
    public BoxContainerControllerU boxContainerControllerU;
    public BoxContainerControllerR boxContainerControllerR;
    public BoxContainerControllerL boxContainerControllerL;
    public CoinsManager coinsManager;

    [SerializeField]
    [TabGroup("StickSizes")] private StickSize _stickSizeUp;
    [SerializeField]
    [TabGroup("StickSizes")] private StickSize _stickSizeRight;
    [SerializeField]
    [TabGroup("StickSizes")] private StickSize _stickSizeLeft;

    private Animator FakeUpStickAnimation;
    private Animator FakeRightStickAnimation;
    private Animator FakeLeftStickAnimation;

    public Transform transform;


    public float verticalVelocity;
    private float gravity = 11.5f;
    private float jumpForce = 300.0f;
    private bool isPeak;
    private float toleft=7f;
    private float toright=7f;
    

    [TabGroup("GameObjects")] public GameObject middleStick;
    [TabGroup("GameObjects")] public GameObject shield;
    // public LockControl lockControl;

    public bool _connected;
    private new Camera camera;

    public bool poleBending = false;

    private void Awake()
    {
        instance = this;
        camera = Camera.main;
    }



    void Start()
    {
        anim = GameObject.Find("untitled").GetComponent<Animator>();
        /*anim2 = GameObject.Find("points1").GetComponent<Animator>();
        anim3 = GameObject.Find("pointsLeft1").GetComponent<Animator>();
        anim4 = GameObject.Find("pointsRight5").GetComponent<Animator>();*/
//        FakeUpStickAnimation = GameObject.Find("Pole").GetComponent<Animator>();
 //       FakeRightStickAnimation = GameObject.Find("PoleRight").GetComponent<Animator>();
  //       FakeLeftStickAnimation = GameObject.Find("PoleLeft").GetComponent<Animator>();

        /*foreach (Transform P in pointsUPParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsLeftParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsRightParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsDownParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsMiddleParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsWiresParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }

        foreach (Transform P in pointsShieldParent)
        {
            points.Add(P.GetComponent<PointsScript>());
        }
        */


    }


    void FixedUpdate()
    {

        if (EnergySystem.instance.currentEnergy == 0)
        {
            //todo Lose UI
            //todo Lose Anim
            ZaxisVelocity = 0f;
            isRunnig = false;
        }

        //rayMethod(Input.mousePosition);
        if (isRunnig)
        {
            anim.SetBool("Pole", false);
            anim.SetBool("PoleLeft", false);
            anim.SetBool("PoleRight", false);
            anim.SetBool("Slide", false);
            verticalVelocity = 0f;
            HorizontalVelocity = 0f;
            ZaxisVelocity = 0f;
        }

        else if (!isRunnig)
        {
            if (IsPoleAnim)
            {
                
                
                verticalVelocity += gravity * Time.deltaTime;
                if (!isPeak)
                {
                    verticalVelocity = -jumpForce * Time.deltaTime;

                }

                if (ToLeft)
                {
                    HorizontalVelocity += -toleft * Time.deltaTime;
                }

                if (ToRight)
                {
                    HorizontalVelocity += toright * Time.deltaTime;
                }
            }
            
            
            if (ToDown)
            {
                ZaxisVelocity = 7f;
            }


        }

        if (poleBending == true)
        {
            PlayerMovement.instance.PolesStackBendMult = 0.003f;
        }


    }



    /*void rayMethod(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);


        if (Input.GetMouseButton(0))
        {

            if (hit.collider && hit.collider.CompareTag("UP"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                // PlayerMovement.instance._boxContainerControllerU.transform.GetChild(PlayerMovement.instance._boxContainerControllerU.transform.childCount).gameObject.SetActive(true);


                if (!pS.isTouched)
                {
                    if (_stickSizeUp.currentHeight <= 1.5f)
                    {
                        gravity = 5f;
                        jumpForce = 125.0f;
                    }
                    else if (_stickSizeUp.currentHeight > 1.5f)
                    {
                        gravity = 11.5f;
                        jumpForce = 300.0f;
                    }
                    
                    
                    boxContainerController.removeStair();
                    PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
                    // boxContainerControllerU.removeStairU();
                    if (boxContainerController.childCount > 0)
                    {
                       
                        _stickSizeUp.IncriseHeight(4);
                    }

                     //Marker.instance.InstantiateMarker();
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsAdded++;

                }
            }

            if (hit.collider && hit.collider.CompareTag("LEFT"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    boxContainerController.removeStair();
                    PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
                    //boxContainerControllerL.removeStairL();
                    if (boxContainerController.childCount > 0)
                    {
                        
                        _stickSizeLeft.IncriseHeight(4);
                    }

                    
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsLeftAdded++;


                }
            }

            if (hit.collider && hit.collider.CompareTag("RIGHT"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    boxContainerController.removeStair();
                    PlayerMovement.instance.list.RemoveAt(PlayerMovement.instance.list.Count-1);
                    //boxContainerControllerR.removeStairR();
                    if (boxContainerController.childCount > 0)
                    {
                        
                        _stickSizeRight.IncriseHeight(4);
                    }

                    
                    pS.isTouched = true;

                    print(hit.collider.name);
                    pointsRightAdded++;


                }
            }

            /*
            if (hit.collider && hit.collider.CompareTag("DOWN"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsDownAdded++;
                }
            }
            #1#

            if (hit.collider && hit.collider.CompareTag("MIDDLE"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsMiddleAdded++;
                }
            }

            if (hit.collider && hit.collider.CompareTag("Port"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsWiresAdded++;
                }
            }

            if (hit.collider && hit.collider.CompareTag("Circular"))
            {
                PointsScript pS = hit.transform.GetComponent<PointsScript>();
                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsShieldAdded++;
                }
            }

            if (hit.collider && hit.collider.CompareTag("LockNumber"))
            {
                /*PointsScript pS = hit.transform.GetComponent<PointsScript>();

                if (!pS.isTouched)
                {
                    pS.isTouched = true;
                    print(hit.collider.name);
                    pointsForLockSystem++;
                    lockControl = hit.transform.gameObject.GetComponentInParent<LockControl>();
                    string value = hit.collider.name;
                    LockControl.instance.SetValue(value);

                    if (lockControl != null)
                    {

                    }
                }#1#


                /* if (hit.collider && hit.collider.CompareTag("LockNumber"))
                 {
                     lockControl = hit.transform.gameObject.GetComponentInParent<LockControl>();
 
                     if (lockControl != null)
                     {
 
                         print(hit.collider.name);
                         hit.transform.gameObject.SetActive(false);
                         string value = hit.collider.name;
                         LockControl.instance.SetValue(value);
                     }
                 }#1#

            }

        }

    }





    public void CheckPointMethod()
    {
        if (pointsAdded < pointsToCollect || lineLength > totalLineLength)
        {
            //TODO: MESAFEYE GÖRE ANIMASYONLAR YAZILACAK.
            
            pointsAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsAdded = 0;
            lineLength = 0;
           // poleBending = true;
            
            // anim2.SetBool("UPP", true);
            if (boxContainerController.childCount > 0)
            {
                anim.SetBool("Pole", true);
                FakeUpStickAnimation.SetBool("a", true);
                isPeak = true;
                IsPoleAnim = true;
                StartCoroutine(delayForPeak());
                isRunnig = false;
                StartCoroutine(delayForPoleAnim());
                if (FakeUpStickAnimation.isActiveAndEnabled)
                {
                    StartCoroutine(destroyAnimatedSticks());
                }
              
            }

            
           rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
           // StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
        }

        if (pointsLeftAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsLeftAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsLeftAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsLeftAdded = 0;
            lineLength = 0;
          //  poleBending = true;
            
            if (boxContainerController.childCount > 0)
            {
                IsPoleAnim = true;
                ToLeft = true;
                isPeak = true;
                StartCoroutine(delayForPeak());
                isRunnig = false;
                StartCoroutine(delayForPoleAnim());
                anim.SetBool("PoleLeft", true);
                FakeLeftStickAnimation.SetBool("c", true);
                if (FakeLeftStickAnimation.isActiveAndEnabled)
                {
                    StartCoroutine(destroyAnimatedSticks());
                }
            }

            
//            anim3.SetBool("LEFT", true);
            rightPart.SetActive(true);


            // anim.SetBool("WallRun",true);
            //StartCoroutine(GameManager.Instance.GameCompleteMethod());
            EnergySystem.instance.UseEnergy(10);

        }

        if (pointsRightAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsRightAdded = 0;
            lineLength = 0;

            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsRightAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsRightAdded = 0;
            lineLength = 0;
            //poleBending = true;
            
           
            if (boxContainerController.childCount > 0)
            {
                anim.SetBool("PoleRight", true); 
                FakeRightStickAnimation.SetBool("b", true); 
                IsPoleAnim = true; 
                isPeak = true;
                ToRight = true; 
                StartCoroutine(delayForPeak());
                isRunnig = false;
                StartCoroutine(delayForPoleAnim());
                if (FakeRightStickAnimation.isActiveAndEnabled)
                {
                    StartCoroutine(destroyAnimatedSticks());
                }
            }

            
           // anim4.SetBool("RIGHT", true);

             rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
        }

        /*if (pointsDownAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsDownAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsDownAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsDownAdded = 0;
            lineLength = 0;
            ToDown = true;
            // isPeak = true;
            //ToRight = true;
            //StartCoroutine(delayForPeak());
            isRunnig = false;
            StartCoroutine(delayForWallRunAnim());
            anim.SetBool("Slide", true);
            //rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            StartCoroutine(GameManager.Instance.GameCompleteMethod());
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
        }#1#

        if (pointsMiddleAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsDownAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsMiddleAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsDownAdded = 0;
            lineLength = 0;
            // isPeak = true;
            //ToRight = true;
            //StartCoroutine(delayForPeak());

            /*if (PlayerMovement.instance.isBalance == true)
            {
                PlayerMovement.instance.animator.SetBool("isBalance", false);
                anim.SetBool("middleStick",true);    
                PlayerMovement.instance.isBalance = false;

            }#1#
            PlayerMovement.instance.animator.SetBool("isBalance", false);
            anim.SetBool("middleStick", true);
            boxContainerController.removeStair();
            StopCoroutine(PlayerMovement.instance.BreakTheBalance());
            PlayerMovement.instance.isBalance = false;
            
            //rightPart.SetActive(true);
            //rightPart.GetComponent<SkinnedMeshRenderer>().enabled = true;
            middleStick.SetActive(true);
            // pointsParent.gameObject.SetActive(false);
            EnergySystem.instance.UseEnergy(10);
        }

        if (pointsWiresAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsWiresAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;
                _connected = false;

            }
        }
        else if (pointsWiresAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsWiresAdded = 0;
            lineLength = 0;
            _connected = true;

        }

        if (pointsShieldAdded < pointsToCollect || lineLength > totalLineLength)
        {
            pointsShieldAdded = 0;
            lineLength = 0;
            for (int i = 0; i < points.Count; i++)
            {
                points[i].isTouched = false;

            }
        }
        else if (pointsShieldAdded > pointsToCollect || lineLength < totalLineLength)
        {
            pointsShieldAdded = 0;
            lineLength = 0;
            EnemyAiTutorial.instance.agent.isStopped = true;
            RaycastHit hit;
            if (Physics.Raycast (camera.ScreenPointToRay (Input.mousePosition), out hit))
            {
                GrappleEffect.instance.transform.LookAt (hit.point);
                GrappleEffect.instance.DoGrapple ();
            }
            
            
            if (hit.collider.name == "PA_Shark")
            {
                Invoke("aq", .9f);
                GrappleEffect.instance.StopGrapple();
            }
            else if (hit.collider.name == "PA_Shark2")
            {
                Invoke("aq2", .9f);
            }
            

            
           

        }
    }*/

    public void aq()
    {
        GrappleEffect.instance.net[0].SetActive(true);
    }

    public void aq2()
    {
        GrappleEffect.instance.net[1].SetActive(true);
    }
    

    IEnumerator colliderFixed()
    {
        yield return new WaitForSeconds(2.044f);
        Player.GetComponent<BoxCollider>().isTrigger = true;


    }

    IEnumerator delayForPoleAnim()
    {
        yield return new WaitForSeconds(2.043f);
        isRunnig = true;
        rightPart.SetActive(false);
        IsPoleAnim = false;
        ToLeft = false;
        ToRight = false;

    }

    IEnumerator delayForPeak()
    {
        yield return new WaitForSeconds(1.024f);
        isPeak = false;

    }

    IEnumerator destroyAnimatedSticks()
    {
        
        yield return new WaitForSeconds(1f);
        FakeUpStickAnimation.SetBool("a", false);
        FakeRightStickAnimation.SetBool("b", false);
        FakeLeftStickAnimation.SetBool("c", false);
        _stickSizeUp.gameObject.SetActive(false);
        _stickSizeRight.gameObject.SetActive(false);
        _stickSizeLeft.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _stickSizeUp.currentHeight = 0f;
        _stickSizeRight.currentHeight = 0f;
        _stickSizeLeft.currentHeight = 0f;
        _stickSizeUp.gameObject.SetActive(true);
        _stickSizeRight.gameObject.SetActive(true);
        _stickSizeLeft.gameObject.SetActive(true);
        poleBending = false;
        PlayerMovement.instance.PolesStackBendMult = 0.001f;

        /*for (int i = 0; i < DoorController.instance.points.Length; i++)
        {
            DoorController.instance.points[i].SetActive(true);
        }*/
    }

    private void Update()
    {
        transform.Translate(0f,0f, Time.deltaTime * ZaxisVelocity);
        //Toplanan tüm stickler harcanmışsa 
        if (boxContainerController.childCount == 1 && _stickSizeUp.currentHeight >= 0f && PlayerMovement.instance.list.Count == 0 || boxContainerController.childCount == 1 && _stickSizeRight.currentHeight >= 0f && PlayerMovement.instance.list.Count == 0 || boxContainerController.childCount == 1 && _stickSizeLeft.currentHeight  >= 0f && PlayerMovement.instance.list.Count == 0)
        {
            for (int i = 0; i < DoorController.instance.points.Length; i++)
            {
                DoorController.instance.points[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < DoorController.instance.points.Length; i++)
        {
            DoorController.instance.points[i].SetActive(true);
            //TODO: KUTUDA HİÇ POLE KALMADIĞINDA VE OYUNCU HALA ÇİZMEYE ÇALIŞIRSA EKRANA THERE IS NO POLES IN THE BOX YAZILACAK.
        }
        }

    }
}