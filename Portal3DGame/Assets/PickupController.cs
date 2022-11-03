using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Pickup Settings")]
    [SerializeField] private Transform holdParent;
    private GameObject heldObj;
    private Rigidbody heldObjRB;


    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 50.0f;

    private void Update()
    {
        if (Input.GetKeyDown(playerMovement.pickUpKey))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }

                else
                {
                    return;
                }

            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        Vector3 moveDirection = (holdParent.position - heldObj.transform.position);

        if (heldObj != null)
        {
            heldObjRB.useGravity = true;
            heldObjRB.AddForce(moveDirection * pickupForce);
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObjRB.transform.parent = null;
            heldObj = null;
        }

        else return;

    }

}
