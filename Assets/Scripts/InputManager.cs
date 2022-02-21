using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    /*public GameObject startMenuUI;
    public GameObject ingameUI;
    public GameObject GameOverUI;
    public bool isUIActive;
    private Scene _scene;*/

    public float timeLeft = 5f;
    public bool timerIsRunning;
    private GameObject shieldUI;

    public GameObject stairSprite;
    public GameObject poleSprite;
    public GameObject shieldSprite;
    
    private void Start()
    {
        instance = this;
        timerIsRunning = true;
        shieldUI = GameObject.Find("ShieldOutline").gameObject;
        //_scene = SceneManager.GetActiveScene();
    }

    public void StairButton()
    {
        stairSprite.SetActive(true);
        poleSprite.SetActive(false);
        shieldSprite.SetActive(false);
    }
    public void PoleButton()
    {
        poleSprite.SetActive(true);
        stairSprite.SetActive(false);
        shieldSprite.SetActive(false);
    }
    public void ShieldButton()
    {
        shieldSprite.SetActive(true);
        stairSprite.SetActive(false);
        poleSprite.SetActive(false);
    }
    
    
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && isUIActive)
        {
            GameManager.Instance.isGameOn = true;
            ResumeGame();
            isUIActive = false;
        }*/

        if (DetectShapes.instance.shield.activeSelf)
        {
            if (timerIsRunning)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                }
                else
                {
                    timeLeft = 0;
                    timerIsRunning = false;
                }
            }

            if (timerIsRunning == false)
            {
                DetectShapes.instance.shield.SetActive(false); 
                
            }
            
            
        }
    }
    /*public void RestartGameButton()
    {
        Application.LoadLevel("SampleScene");
    }
    void ResumeGame()
    {
        GameManager.Instance.isGameOn = true;
        FindObjectOfType<PlayerMovement>().animator.SetBool("isGameOn", true);
        startMenuUI.SetActive(false);
        ingameUI.SetActive(true);
    }

    public void GameOver()
	{
        GameOverUI.SetActive(true);
        ingameUI.SetActive(false);
	}*/
    public void Shield()
    {
        DetectShapes.instance.shield.SetActive(true);
        shieldUI.gameObject.transform.DOJump(shieldUI.transform.position,20f,(int) 1f, 1f)
            .OnComplete(() => { shieldUI.gameObject.SetActive(false); });
        

    }
}