using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PlayerMovement : MonoBehaviour
{
   /* private SwerveInputSystem _swerveInputSystem;
    [SerializeField]
    private float swerveSpeed = 0.5f;
    [SerializeField]
    private float maxSwerveAmount = 1f;*/
    
    public float moveSpeedZ = 200f;
    public float moveSpeedY = 0;
    public float moveSpeedX = 0;
    public Rigidbody rb;
    public bool OnHeli=false;
    public bool IsAttached=false;
    private float jumpSpeed;
    private float ZaxisMove;
    public Transform target;
    public float step;
    private bool check;
    public Transform Heli;
    
    
    public bool isBalance;
    public Animator animator;
    private Animator stairsAnimator;
    private Animator poleAnimator;
    public GameObject middlePoints;
    public GameObject hangingStick;
    public GameObject parts;
    private bool moving;
    public ParticleSystem blood;
    public GameObject shield;

    public GameObject finishCube;
    public BoxContainerController _boxContainerController;
    public BoxContainerControllerR _boxContainerControllerR;
    public BoxContainerControllerL _boxContainerControllerL;
    public BoxContainerControllerU _boxContainerControllerU;
    private CoinsManager _coinsManager;
    public GameObject bonusPosition;
    public GameObject Will;
    public GameObject boat;
    public GameObject boatMissile;
    public GameObject shark;
    public GameObject screen;
    public GameObject waypoint;
    private bool yuruAmkBotu;

    public List<GameObject> list;
    public GameObject[] x4MultiplierPoles;
    public GameObject[] x3MultiplierPoles;
    public GameObject[] x2MultiplierPoles;
    public GameObject[] plus5adderPoles;
    public GameObject[] plus10adderPoles;
    public GameObject[] plus20adderPoles;
    public float PolesStackBendMult;
    
    
    Rigidbody rigidBody;
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;
    

    private readonly int idPickUp = Animator.StringToHash("PickUp");
    public event Action OnPick;
   // public Text MultiplierText;

   /*public bool mult2= false;
   public bool mult3= false;
   public bool mult4= false;
   public bool plus5= false;
   public bool plus10= false;*/
   
   public TMP_Text text;


   //TODO: ÇARPMA VEYA TOPLAMA İŞLEMLERİNDE +1 YERİNE +(NE KADAR ALDIYSA) GİBİ ŞEYLER YAZILACAK.


   public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
       // _swerveInputSystem = GetComponent<SwerveInputSystem>();
        GameManager.Instance.isGameOn = true;
        animator.SetBool("isGameOn", true);
        
        rigidBody = GetComponent<Rigidbody>();

        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    /*
    private int waypointIndex = 0;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    public bool go = false;
    */
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GameObject.Find("untitled").GetComponent<Animator>();
        stairsAnimator = GameObject.Find("Stairs").GetComponent<Animator>();
       // poleAnimator = GameObject.Find("Pole").GetComponent<Animator>();
        _coinsManager = FindObjectOfType<CoinsManager>();
        list = new List<GameObject>();
        

    }


    void Update()
    {
        //moving = FindObjectOfType<GameManager>().StopMoving;
        /*float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount,0,0);*/

        if (OnHeli)
        {
            

            if (!IsAttached)
            {
               // transform.position = Vector3.MoveTowards(transform.position, target.position, step*Time.deltaTime);
               

               StartCoroutine(HeliJumpDelay());
               rb.velocity= new Vector3(0,0,0);
               
                
            }

            if (IsAttached)
            {    
                rb.velocity=new Vector3(0,0,0);
                this.transform.parent = Heli.transform;
            }

            
           // rb.velocity= new Vector3(0,moveSpeedY,3);

        }
        
        if (Input.GetMouseButtonDown(0) )
        {
           /* Debug.Log("mouse1");
            for (int i = 0; i < stick.Count; i++)
            {
                _boxContainerController.addNewStair(stick[i].transform);    
                _coinsManager.RemoveCoins(1);
            }
            
GameManager.Instance.isGameOn = true;
           animator.SetBool("isGameOn", true);
           gameObject.GetComponent<Rigidbody>().isKinematic = false;
           */
        }

        
        //TODO: LADDER I DENEMEK İÇİN POLE LARI VE BURAYI KAPATTIM AÇMAYI UNUTMA.
        //Çantadakilerin metaryallerini değiştirme
       /* if (list.Count > 15)
        {
            PolesMaterials.instance._transparency = true;

        }
        else
        {
            PolesMaterials.instance._transparency = false;
        }*/

        //TODO: KODU YAZILMADIĞI İÇİN ŞU AN BÖYLE İLERİDE YAZILINCA BU SİLİNECEK. ÇANTADA POLE KALMAYINCA MERDİVEN ANİMASYONU DURUYOR.
        if (list.Count < 1)
        {
            StartCoroutine(stopAnim());
        }
        else
        {
            stairsAnimator.enabled = true;
        }
        
    }

    void stepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f,0,1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f,0,1), out hitUpper45, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f,0,1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f,0,1), out hitUpperMinus45, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }
    }
    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(.55f);
        stairsAnimator.enabled = false;
    }
    private void FixedUpdate()
    {
        if (!OnHeli)
        {
            moveSpeedY = FindObjectOfType<DetectShapes>().verticalVelocity ;
            moveSpeedX = FindObjectOfType<DetectShapes>().HorizontalVelocity;
            moveSpeedZ =FindObjectOfType<DetectShapes>().ZaxisVelocity;
            rb.velocity= new Vector3(0,0,5);
            stepClimb();
        }

        if (yuruAmkBotu == true)
        {
           // boat.transform.position = Vector3.MoveTowards(boat.transform.position, waypoint.transform.position, 10f * Time.deltaTime);
           
            boatMissile.transform.position = Vector3.MoveTowards(boatMissile.transform.position, shark.transform.position, 15f * Time.deltaTime);
            /*screenTarget = Camera.main.ScreenToWorldPoint(Vector3.zero);
            screenTarget.z = transform.position.z;
            screen.transform.position = Vector3.MoveTowards(screen.transform.position, screenTarget, 5f * Time.deltaTime);*/

        }

        BendPoleStack();


    }
    
    private void OnEnable()
    {
        OnPick += SetPickUp;
    }

    private void OnDisable()
    {
        OnPick -= SetPickUp;
    }

    internal void BendPoleStack()
    {
        var cos = Mathf.Cos(Time.time);
        Vector3 newPos;
        int polesStackBendOffset = (int) 0.15;  
        
        for (int i = polesStackBendOffset; i < list.Count; i++)
        {
            newPos = list[i].transform.localPosition;
            var j = i - polesStackBendOffset;
            newPos.x = PolesStackBendMult * cos * j * j;   // Default => 0.00015f = PolesStackBendMult    
            list[i].transform.localPosition = newPos;
        }
    }

    private void SetPickUp()
    {
        animator.SetTrigger(idPickUp);
    }

  

    IEnumerator HeliJumpDelay()
    {
        yield return new WaitForSeconds(0.45f);
       // FindObjectOfType<DetectShapes>().anim.SetTrigger("next");
        Vector3 desiredPosition = transform.position = Vector3.MoveTowards(transform.position, target.position, step*Time.deltaTime);
        Vector3 moveDelta = new Vector3(desiredPosition.x,desiredPosition.y,desiredPosition.z)-transform.position;
        transform.position = desiredPosition;
        jumpSpeed = 2f;
        ZaxisMove = 4f;
        check = false;
    }

    private void Move()
    { 
        /*
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1 && go == false)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);
            Quaternion targetrotation = Quaternion.Euler(waypoints[waypointIndex].transform.eulerAngles.x,waypoints[waypointIndex].transform.eulerAngles.y, waypoints[waypointIndex].transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetrotation,moveSpeed*Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                // if (transform.position == waypoints[waypoints.Length-1].transform.position)
                
            }
        }*/
    }

    IEnumerator silbunu()
    {
        yield return new WaitForSeconds(6.5f);
        animator.SetBool("Build", false);
      //  DetectShapes.instance.isRunnig = true;
       // DetectShapes.instance.ZaxisVelocity = 5f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
        //    DetectShapes.instance.isRunnig = false;
         //   DetectShapes.instance.ZaxisVelocity = 0f;
           _boxContainerController.bagToStairs();
         //  stairsAnimator.SetTrigger("StairsBuilding");
           //animator.SetBool("Build", true);
          // StartCoroutine(silbunu());
        }

        if (other.gameObject.CompareTag("PoleJump"))
        {
            poleAnimator.SetTrigger("pole");
        }

        if (other.gameObject.CompareTag("pats"))
        {
            GameObject.Find("parts").GetComponent<Player>().yRotation = 90f;    
            
            Debug.Log("Why");
        }
        
        if (other.gameObject.CompareTag("GroundBalance"))
        {
            isBalance = true;
            StartCoroutine(BreakTheBalance());
            animator.SetBool("isBalance", true);
            for (int i = 0; i < DoorController.instance.points.Length; i++)
            {
                DoorController.instance.points[i].SetActive(false);
            }
            
            middlePoints.SetActive(true);
            
            
            //todo: hız biraz düşmesi lazım 

        }
        
        if (other.gameObject.CompareTag("ground")) 
        {
            
            isBalance = false;
            animator.SetBool("isBalance", false);
            animator.SetBool("middleStick", false);
            if (DetectShapes.instance.middleStick.activeSelf)
            {
                DetectShapes.instance.middleStick.SetActive(false);
            }
            for (int i = 0; i < DoorController.instance.points.Length; i++)
            {
                DoorController.instance.points[i].SetActive(true);
            }
            middlePoints.SetActive(false);
            gameObject.transform.SetParent(parts.transform);
            OutGroundParentChanged(!true);
            moving = false;
        }

        if (other.gameObject.CompareTag("Boat"))
        {
            animator.SetBool("isGameOn", false);
            OutGroundParentChanged(!true);
            gameObject.transform.SetParent(Will.transform);
            transform.position = Will.transform.position;
            DetectShapes.instance.isRunnig = false;
            GameManager.Instance.StopMoving = true;
            Main.Instance.State = State.Finish; 
            yuruAmkBotu = true;
           

        }
        
        

        if (other.gameObject.CompareTag("DeadZone"))
        {
          //  gameObject.GetComponent<Rigidbody>().useGravity = true;
          gameObject.GetComponent<Collider>().isTrigger = false;
          rb.constraints = RigidbodyConstraints.FreezePositionZ;
            animator.SetBool("middleStick", false);
            animator.SetBool("isBalance", false);
            animator.SetBool("DeadZone", true);
            if (DetectShapes.instance.middleStick.activeSelf)
            {
                DetectShapes.instance.middleStick.GetComponent<Rigidbody>().isKinematic = false;    
            }
            
            //todo: 1.5 saniye sonra lose UI girecek.
            
        }

        if (other.gameObject.CompareTag("HangingZone"))
        {
            
            OutGroundParentChanged(true);
           gameObject.transform.SetParent(hangingStick.transform);
            other.gameObject.GetComponent<MinusOne>().Collectt();
           
        }

        /* if (other.gameObject.CompareTag("finish"))
         {
             Debug.Log("finish");
             for (int i = 0; i < 4; i++)
             {
                 Instantiate(finishCube, gameObject.transform.position, Quaternion.identity, gameObject.transform);  
             }
             
         }*/

        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.GetComponent<StairController>().popUP();
            
            _boxContainerController.addNewStair(other.gameObject.transform);
            list.Add(other.gameObject);
            other.GetComponent<StairController>().Collect();
            OnPick?.Invoke();

            //_coinsManager.AddCoins(1);
          
           //StartCoroutine(destroy());

        }
        /*if (other.gameObject.CompareTag("CollectableR"))
        {
            other.gameObject.GetComponent<StairController>().popUP();
            _boxContainerControllerR.addNewStairR(other.gameObject.transform);
            other.gameObject.SetActive(false);
            //_coinsManager.AddCoins(1);
          
            //StartCoroutine(destroy());

        }
        if (other.gameObject.CompareTag("CollectableL"))
        {
            other.gameObject.GetComponent<StairController>().popUP();
            _boxContainerControllerL.addNewStairL(other.gameObject.transform);
            other.gameObject.SetActive(false);
           // _coinsManager.AddCoins(1);
          
            //StartCoroutine(destroy());

        }
        if (other.gameObject.CompareTag("CollectableU"))
        {
            other.gameObject.GetComponent<StairController>().popUP();
            _boxContainerControllerU.addNewStairU(other.gameObject.transform);
            other.gameObject.SetActive(false);
            // _coinsManager.AddCoins(1);
            
            //StartCoroutine(destroy());

        }
       
        
        
        IEnumerator destroy()
        {
            yield return new WaitForSeconds(.5f);
            Destroy(other.gameObject);

        
        }*/

        if (other.gameObject.CompareTag("GroundBonus"))
        {
            moving = true;
            Debug.Log("bonus");
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            animator.SetBool("Idle", true);
            for (int i = 0; i < list.Count; i++)
            {
                Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 2f, gameObject.transform.position.z ), Quaternion.identity, gameObject.transform);
               
            }
            
            /*for (int i = 0; i < stick.Count; i++)
            {
                stick[i].SetActive(true);
            }*/
          
            
            /*for (int i = 0; i < _coinsManager.Amount; i++)
            {
                Instantiate(finishCube, gameObject.transform.position, Quaternion.identity, gameObject.transform);   
            }*/
            
        }

        if (other.gameObject.name == "x2Multiplier")
        {
            for (int i = 0; i < list.Count * 2; i++)
            {
                x2MultiplierPoles[i].SetActive(true);
                //Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
            }
            // mult2 = true;
            text.text = "+" + (list.Count * 2).ToString();
           
        }
        else if (other.gameObject.name == "x3Multiplier")
        {
            for (int i = 0; i < list.Count * 3; i++)
            {
                x3MultiplierPoles[i].SetActive(true);
               // Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
            }
             text.text = "+" + (list.Count * 3).ToString();

           // mult3 = true;
        }
        else if (other.gameObject.name == "x4Multiplier")
        {
            
            for (int i = 0; i < list.Count * 4; i++)
            {
                x4MultiplierPoles[i].SetActive(true);
              // Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
                
            }
            // text.text = "+" + (list.Count * 4).ToString();

          //  mult4 = true;
        }
        else if (other.gameObject.name == "+5Adder")
        {
            for (int i = 0; i < 5; i++)
            {
                plus5adderPoles[i].SetActive(true);
              // Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
            }
              text.text = "+5";

           // plus5 = true;
        }
        else if (other.gameObject.name == "+10Adder")
        {
            for (int i = 0; i < 10; i++)
            {
               plus10adderPoles[i].SetActive(true);
               // Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
               
            }
           
             text.text = "+10";
               //  plus10 = true;

        }
        else if (other.gameObject.name == "+20Adder")
        {
            for (int i = 0; i < 20; i++)
            {
                plus20adderPoles[i].SetActive(true);
                // Instantiate(finishCube, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity, _boxContainerController.transform);
               
            }
           
            text.text = "+20";
            //  plus10 = true;

        }

        
        
        

        //TODO: BONUSGROUND OLAYI BURADA YAZILI, ŞU ANLIK KAPATIYOM
        /*if (other.gameObject.CompareTag("finalCube"))
        {    
            BonusController.instance.addNewStack(other.gameObject.transform);    
        }*/
        
        if (other.gameObject.CompareTag("GroundObstacle") && DetectShapes.instance.shield.activeSelf == false)
        {
            
            animator.SetBool("jog", true);
            DetectShapes.instance.isRunnig = false;
            _boxContainerController.woodObstacle();
            list.RemoveAt(list.Count-1);
            StartCoroutine(delayForWood());
            other.gameObject.GetComponent<MinusOne>().Collectt();
            
        }
        
        
        


        if (other.gameObject.CompareTag("Axe") && shield.activeSelf.Equals(false))
        {
            blood.Play();
            GameManager.Instance.StopMoving = true;
            DetectShapes.instance.isRunnig = false;
            Debug.Log("gg");
            //todo: Ölüm Animasyonu girecek -> GameManager.Instance.OnGameFinish();
            animator.SetBool("isGameOn", false);
            animator.SetBool("gg", true);
            /*var y = destroyedVersionOfPlayer.transform.position;
            y.y += .5f;
            var x = Instantiate(destroyedVersionOfPlayer, new Vector3(destroyedVersionOfPlayer.transform.position.x, y.y, destroyedVersionOfPlayer.transform.position.z), transform.rotation);
            Destroy(x, 1f);
            body.SetActive(false);*/
        }

    }

    

    private IEnumerator delayForWood() 
    {
        DetectShapes.instance.ZaxisVelocity = 3f;
        yield return new WaitForSeconds(1f);
        DetectShapes.instance.isRunnig = true;
       

    }

    private IEnumerator delayForPoles()
    {
        
        yield return new WaitForSeconds(.5f);
    }

    public IEnumerator BreakTheBalance() 
    {
        float elapsedTime = 0f;
        float TempoSalita = 4f;
        float LeftOrRight = Random.Range(0, 2);
        yield return null;
        while (elapsedTime < TempoSalita && isBalance == true)
        {
            if (LeftOrRight == 0)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x - 0.015f, transform.position.y, transform.position.z), (elapsedTime / TempoSalita));
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x + 0.015f, transform.position.y, transform.position.z), (elapsedTime / TempoSalita));
            }
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    public void OutGroundParentChanged(bool isTrue)
	{
		
        hangingStick.GetComponent<Collider>().enabled = isTrue;
        hangingStick.GetComponent<Rigidbody>().isKinematic = !isTrue;
        hangingStick.GetComponent<Rigidbody>().useGravity = isTrue;
        animator.SetBool("isHanging", isTrue);
        GameManager.Instance.StopMoving = isTrue;
        hangingStick.SetActive(isTrue);    
        gameObject.GetComponent<Rigidbody>().useGravity = isTrue;
        gameObject.GetComponent<Rigidbody>().isKinematic = isTrue;
        _boxContainerController.removeStair();
        
    }

    
   
}
