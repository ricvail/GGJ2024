using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTutorial : MonoBehaviour
{

    private Canvas canvas;
    private Vector3 initialPosition;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        
        initialPosition = PlayerController.Instance.transform.position;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if ((PlayerController.Instance.transform.position - initialPosition).magnitude>.5)
        {
            canvas.enabled = false;
        }
        
    }
}
