using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComedianScript : MonoBehaviour
{

    public Animator curtains;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            curtains.SetBool("isGameOver", true);
            StartCoroutine(delayedLoadScene(3));
        }
    }

    IEnumerator delayedLoadScene(float seconds)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(seconds);
        Debug.Log("Animation over");
        CutsceneController.nextSceneName = "Level2";
        CutsceneController.joke = "I am addicted to drinking brake fluid! But it's okay, I can stop any time.";
        SceneManager.LoadScene("Cutscene");
    }
}
