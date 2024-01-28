using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private bool prompt = true;
    
    private Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if (prompt && PlayerController.Instance.throwable != null)
        {
            canvas.enabled = true;
        }

        if (PlayerController.Instance.isThrowing)
        {
            prompt = false;
            canvas.enabled = false;
        }
    }

    
}
