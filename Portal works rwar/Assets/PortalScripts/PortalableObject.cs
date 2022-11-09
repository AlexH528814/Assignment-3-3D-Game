using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalableObject : MonoBehaviour
{
    public GameObject GameObject;
    public Rigidbody rb;
    public Collider coll;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }


}
