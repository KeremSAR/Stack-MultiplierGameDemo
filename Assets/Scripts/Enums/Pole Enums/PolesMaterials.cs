using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolesMaterials : MonoBehaviour
{
   /* internal Material currentPolesMaterial;
    internal Material polesMaterialOpaque;
    internal Material polesMaterialTransparent;*/
    //public GameObject yellowStair;
    public Material transparentMaterial;

    public Material opaque;
    public GameObject gameObject;
    public bool _transparency = false;

    public static PolesMaterials instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = opaque;
    }

    private void Update()
    {
        if (_transparency == true)
        {
            foreach (var poles in PlayerMovement.instance.list)
            {
                poles.GetComponentInChildren<MeshRenderer>().material = transparentMaterial;
            }
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = opaque;
        }
        
    }


    /*public enum PolesMaterial
    {
        Opaque,
        Transparent
    }

    private void Start()
    {
        currentPolesMaterial = polesMaterialOpaque = yellowStair.gameObject.GetComponent<MeshRenderer>().material;
        polesMaterialTransparent = transparentMaterial;

    }

    public void ApplyNewMaterial(PolesMaterial polesMaterial)
    {
        currentPolesMaterial =
            polesMaterial == PolesMaterial.Opaque ? polesMaterialOpaque : polesMaterialTransparent;

        foreach (var poles in PlayerMovement.instance.list)
        {
            poles.GetComponent<MeshRenderer>().material = currentPolesMaterial;

        }
    }*/


}
