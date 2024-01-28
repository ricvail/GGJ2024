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
            if (i == 1)
            {
                AudioManager.Instance.PlaySFX("Mh");
            } 
            else if (i == 2)
            {
                AudioManager.Instance.PlaySFX("Applause");
            }
            else if (i == 3)
            {
                AudioManager.Instance.PlaySFX("Voice");
            }
            else if (i == 4)
            {
                AudioManager.Instance.StopMusic();
                AudioManager.Instance.PlaySFX("Scale");
                AudioManager.Instance.PlaySFX("Booing");
            }
            else if (i ==5)
            {
                AudioManager.Instance.PlaySFX("Grunt");
            }
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Theatre");
        AudioManager.Instance.PlaySFX("Happy");
    }

}
