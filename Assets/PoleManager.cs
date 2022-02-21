using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoleManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
   // [SerializeField] private float growTimeout;
    
    [SerializeField] private float pushForce;
   // [SerializeField] private float moveSpeed;
    
    public GameObject[] _currentPole;
    private FixedJoint _joint;
    private bool _isFlying;
    private float _lastGrowTime;

    private Vector3 lastPos;
    public static PoleManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       // GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveSpeed);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !_isFlying)
        {
           // GrowPole();
           InitiateJump();
        }

       /* if (Input.GetMouseButtonUp(0) && !_isFlying)
        {
            
        }*/

        ClearCurrentPoleIfDetached();
    }

    private void OnCollisionEnter(Collision other)
    {
        //GameObject.FindWithTag("Trail").GetComponent<TrailRenderer>().emitting = false;
        if (other.collider.name.StartsWith("Platform") && _isFlying)
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // karakterin atladıktan sonraki açısal hızı, zero olmazsa yığılıyor.
           // GetComponent<Rigidbody>().velocity = new Vector3(0, 0, PlayerMovement.instance.moveSpeedZ);
            transform.rotation = Quaternion.Euler(0, 0 ,0); // böyle yapmazsak nasıl atladıysa platforma dokununca öyle kalıyor.
            _isFlying = false;
            
            DebugDistance();
        }
    }

   /* public void GrowPole()
    {
        if (null == _currentPole)
        {
            var position = transform.position;
            _currentPole = Instantiate(prefab, new Vector3(position.x, position.y + 1.5f, position.z+0.8f), Quaternion.identity, gameObject.transform); // Quaternion.identity = no rotation
            _currentPole.GetComponent<Rigidbody>().isKinematic = true;
            _currentPole.GetComponent<PoleRenderer>().InitPole();

        }

        if (Time.time * 1000 - _lastGrowTime > growTimeout || _lastGrowTime == 0)
        {
            _lastGrowTime = Time.time * 1000;
            _currentPole.GetComponent<PoleRenderer>().IncreasePoleSize();
        }
    }*/

    public void InitiateJump()
    {
        for (int i = 0; i < _currentPole.Length; i++)
        {
            _currentPole[i].GetComponent<Rigidbody>().isKinematic = false;   
            _joint.connectedBody = _currentPole[i].GetComponent<Rigidbody>();
            _currentPole[i].GetComponent<Rigidbody>().AddForce(_currentPole[i].transform.right * pushForce);
        }
        
        _isFlying = true;
        _joint = gameObject.AddComponent<FixedJoint>();
        
        lastPos = transform.position;
        
        //_currentPole.GetComponent<Renderer>().material.SetFloat("_Frequency", GetAnimationFrequency(_currentPole.GetComponent<PoleRenderer>().GetSize()));
    }

    private float GetAnimationFrequency(float size)
    {
        return -1.4f + (0.2f * (size - 1f)); // pole'un deforme olmaması için
    }

    private void ClearCurrentPoleIfDetached()
    {
        for (int i = 0; i < _currentPole.Length; i++)
        {
            if (null != _currentPole[i])
            {
                if (_currentPole[i].GetComponent<Pole>().IsDetached())
                {
                    StartCoroutine(destroyStick());
                }
            }    
        }
        
    }

    private void DebugDistance()
    {
//        GameObject.FindWithTag("DebugValue").GetComponent<Text>().text =
      //      Vector3.Distance(lastPos, transform.position).ToString();
     
        lastPos = transform.position;
    }

     IEnumerator destroyStick()
     {
         yield return new WaitForSeconds(.7f);
         for (int i = 0; i < _currentPole.Length; i++)
         {
             Destroy(_currentPole[i]);
             _currentPole[i] = null;     
         }

        
     }
    
}
