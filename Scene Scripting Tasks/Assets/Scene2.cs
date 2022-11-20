using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scene2 : MonoBehaviour
{
    //public TextMeshPro scoreText1;
    public TMP_Text scoreText;
    void Start()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("score").ToString();
    }

}

    
