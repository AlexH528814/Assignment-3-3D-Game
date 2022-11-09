using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Vector3 portalNormal;
    private Portal otherPortal;
    private GameObject player;
    public Rigidbody rb;

    public bool hasMoved;

    float disableTimer;


    // Start is called before the first frame update
    void Start()
    {
        
        player = rb.gameObject;

        SetOtherPortal();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();

        if (other.gameObject == player && disableTimer <= 0 && hasMoved && otherPortal.hasMoved)
        {
            otherPortal.MovePlayerToThisPortal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void SetOtherPortal()
    {
        if (otherPortal == null)
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal != this)
                {
                    otherPortal = portal;
                }
            }
        }
    }

    public void MovePlayerToThisPortal()
    {
       
        disableTimer = 1;
        Vector3 exitVelocity = portalNormal * rb.velocity.magnitude;
        rb.velocity = exitVelocity;

        Vector3 exitPosition = transform.position + otherPortal.portalNormal * 2;
        player.transform.position = exitPosition;
    }

    public void MovePortal(RaycastHit raycastHit)
    {
        hasMoved = true;
        transform.position = raycastHit.point;
        portalNormal = raycastHit.normal;
        
    }
}
