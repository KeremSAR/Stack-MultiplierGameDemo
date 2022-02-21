using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sil : MonoBehaviour
{
    public GameObject hangingStick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HangingZone"))
        {
            OutGroundParetChanged(true);
            gameObject.transform.SetParent(hangingStick.transform);
        }
    }

    public void OutGroundParetChanged(bool isTrue)
    {
        hangingStick.GetComponent<Collider>().enabled = isTrue;
        hangingStick.GetComponent<Rigidbody>().isKinematic = !isTrue;
        hangingStick.GetComponent<Rigidbody>().useGravity = isTrue;
        GameManager.Instance.StopMoving = isTrue;
        hangingStick.SetActive(isTrue);
        gameObject.GetComponent<Rigidbody>().useGravity = isTrue;
        gameObject.GetComponent<Rigidbody>().isKinematic = isTrue;
    }
}
