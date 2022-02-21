using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Animator sharkAnimator;
    public ParticleSystem particleSystem;
    void Start()
    {
        sharkAnimator = GameObject.Find("PA_Shark").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe") && other.gameObject.name == "PA_Shark")
        {
            sharkAnimator.SetBool("Caught", true);
            var x = particleSystem.transform.localRotation;
            x.x = 1f;
            x.y = 0f;
            x.z = 0f;
            Instantiate(particleSystem, sharkAnimator.gameObject.transform.position, x);
            EnemyAiTutorial.instance._fieldOfView.GetComponent<MeshRenderer>().enabled = false;
            Time.timeScale = .4f;
            StartCoroutine(slowTime());
        }
    }

    IEnumerator slowTime()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Main.Instance.State = State.Menu;
        
    }
}
