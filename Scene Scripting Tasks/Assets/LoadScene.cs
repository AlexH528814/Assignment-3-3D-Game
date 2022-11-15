using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ButtonClick()
    {
        PlayerPrefs.SetInt("score", 2500);
        SceneManager.LoadScene("Scene 2");
    }

    public void ButtonClick1()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
