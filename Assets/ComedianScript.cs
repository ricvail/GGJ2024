using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComedianScript : MonoBehaviour
{

    public string nextJoke, nextScene;
    
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
        CutsceneController.nextSceneName = nextScene;
        CutsceneController.joke = nextJoke;
        SceneManager.LoadScene("Cutscene");
    }
}
