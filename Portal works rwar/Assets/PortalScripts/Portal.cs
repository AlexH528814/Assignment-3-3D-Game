using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * Released under the creative commons attribution license.
 * Do whatever you like with the code, just give credit to Stuart Spence.
 * https://creativecommons.org/licenses/by/3.0/
 */

public class Portal : MonoBehaviour
{
    private PortalableObject po;
    private Rigidbody rb;

    // the other portal that this will teleport to/render
    public GameObject linkedPortal;

    // used to help prevent us from infinitely teleporting back and forth
    private bool portalActive = true;
    private Vector3 portalNormal;

    public bool hasMoved = false;
    public void MovePortal(RaycastHit raycastHit)
    {
        hasMoved = true;
        transform.SetPositionAndRotation(raycastHit.point, Quaternion.FromToRotation(Vector3.forward, raycastHit.normal));
        portalNormal = raycastHit.normal;

        
    }

    void OnTriggerEnter(Collider other)
    {

        if (portalActive)
        {

            rb = other.GetComponent<Rigidbody>();

            Vector3 vector3 = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            Vector3 vector = new Vector3(0, 0,0);

            rb.velocity = vector;

            // make other portal not teleport us and our current one enabled
            linkedPortal.GetComponent<Portal>().Toggle();

            // OnExit never gets called after teleportation from a portal, so we
            // need to toggle manually
            Toggle();

            // cache player rotation to revert after teleport
            float xRot = other.transform.rotation.x;
            float zRot = other.transform.rotation.z;

            // set the player's position and rotation to the other portal's
            other.transform.SetPositionAndRotation(linkedPortal.transform.position,
                Quaternion.identity);
            other.transform.rotation = linkedPortal.transform.rotation;



            rb.velocity = vector3;
            



            // Y rotation from portal
            float yRot = linkedPortal.transform.rotation.y;

            // combine previously cached axes with new Y to get new rotation
            other.transform.eulerAngles = new Vector3(xRot, yRot, zRot);

          

            
        }
    }

    void OnTriggerExit(Collider other)
    {

        // re-enable this portal for teleportation after we've exited
        // (teleporting into it)
        Toggle();
    }

    public void Toggle()
    {

        // whether we can actually use this portal to teleport
        portalActive = !portalActive;
    }
}
