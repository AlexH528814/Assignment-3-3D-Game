using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class upRight : MonoBehaviour
{
    [SerializeField]
    public GameObject GameObject;


    // Update is called once per frame
    void Update()
    {
        if (GameObject.transform.rotation.x == 0 && GameObject.transform.rotation.z == 0)
        {
            return;
        }

        else
        {
            Invoke("ResetAngles", 0.5f);
        }


        

        
    }

    void ResetAngles()
    {
        GameObject.transform.rotation = Quaternion.EulerRotation(0, 0, 0);
    }

}