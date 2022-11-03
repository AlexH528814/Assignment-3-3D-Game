using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class upRight : MonoBehaviour
{
    [SerializeField]
    public GameObject GameObject;

    public float xAngle = 1, yAngle = 1, zAngle = 1;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.transform.rotation.x != 0)
        {
            ResetAngles();
        }

        if (GameObject.transform.rotation.y != 0)
        {
            ResetAngles();
        }

        if (GameObject.transform.rotation.z != 0)
        {
            ResetAngles();
        }
    }
   
    void ResetAngles()
    {
        GameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
    
}
