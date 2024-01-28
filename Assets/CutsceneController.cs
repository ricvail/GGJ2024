using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public static string nextSceneName;
    public static string joke;

    public Image image;

    public List<Sprite> scenes;
    public int jokeScene;
    public TMP_Text jokeText;

    private int i;
    public void advanceCutscene()
    {
        jokeText.text = joke;
        i++;
        if (i < scenes.Count)
        {
            image.sprite = scenes[i];
            jokeText.gameObject.SetActive(i == jokeScene);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
}
