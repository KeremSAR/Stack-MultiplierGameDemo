using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public GameObject destroyedVersionOfSaw;
    public GameObject destroyedVersionOfAxe;
    public GameObject destroyedVersionOfSawblade;
    public GameObject destroyedVersionOfBlade;
    public GameObject destroyedVersionOfCutter;
    public GameObject[] holders;
    public GameObject[] spears;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "saw") 
        {
            Instantiate(destroyedVersionOfSaw, transform.position, transform.rotation);
            holders[0].GetComponent<Rigidbody>().isKinematic = false;
            holders[1].GetComponent<Rigidbody>().isKinematic = false;
            // destroyedVersion.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "GreatAxe")
        {
            Instantiate(destroyedVersionOfAxe, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "SawBlade")
        {
            Instantiate(destroyedVersionOfSawblade, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "blade3")
        {
            var x = other.gameObject.transform.rotation;
            x.x = 0f;
            Instantiate(destroyedVersionOfBlade, other.transform.position, x);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "Cutter")
        {
            Instantiate(destroyedVersionOfCutter, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Door") && other.gameObject.name=="door")
        {
                Explosion.instance.explode();
                for (int i = 0; i < spears.Length; i++)
                {
                    spears[i].GetComponent<Rigidbody>().isKinematic = false;
                }
            
        }
        
      
    }

    
    /*  void Start()
    {
        if (GetComponent<Renderer>())
        {
            mat = GetComponent<Renderer>().sharedMaterial;
        }

    }

    void Update()
    {

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
            mat.SetFloat("_HitTime", hitTime);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        
        
        for (int i = 0; i < _collisionTag.Length; i++)
        {

            if (_collisionTag.Length > 0 || collision.transform.CompareTag(_collisionTag[i]))
            {
                //Debug.Log("hit");
                ContactPoint[] _contacts = collision.contacts;
                for (int i2 = 0; i2 < _contacts.Length; i2++)
                {
                    mat.SetVector("_HitPosition", transform.InverseTransformPoint(_contacts[i2].point));
                    hitTime = 500;
                    mat.SetFloat("_HitTime", hitTime);
                }
            }
        }
        
    }*/
}

