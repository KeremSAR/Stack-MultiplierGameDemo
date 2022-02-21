using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField]
    private GameObject fallMarkerPrefab = null;
    [SerializeField]
    private Vector3 fallDirection = Vector3.down;
    private GameObject fallMarkerInstance = null;
    
    
    public static Marker instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (fallMarkerPrefab != null)
        {
            
            RaycastHit hit;
            if (Physics.Raycast(new Ray(transform.position, fallDirection), out hit))
            {
                Vector3 fallPosition = hit.point;
                fallMarkerInstance = Instantiate(fallMarkerPrefab, fallPosition - 0.05f * fallDirection,
                    Quaternion.identity);
            }
        }
        
    }

    private void OnDestroy()
    {
        if (fallMarkerInstance != null)
        {
            Destroy(fallMarkerInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
