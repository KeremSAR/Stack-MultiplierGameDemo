using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sil2Ghost : MonoBehaviour
{
    public static sil2Ghost instance;
    public Transform[] ghostPoses;

    private int index = 0;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePos()
    {
        index++;

        if (ghostPoses.Length>index)
        {
            transform.position = ghostPoses[index].transform.position;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
