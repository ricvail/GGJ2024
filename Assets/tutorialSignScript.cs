using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialSignScript : MonoBehaviour
{

    public Canvas canvas;
    public Animator curtains;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Music_Menu");
    }

    private void Update()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
        if (PlayerController.Instance.isThrowing)
        {
            canvas.gameObject.SetActive(true);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("LET'S GOOO");
            curtains.SetBool("isGameOver", true);
            StartCoroutine(delayedLoadScene(3));
        }
    }

    IEnumerator delayedLoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Animation over");
        SceneManager.LoadScene("Level1");
    }
}
