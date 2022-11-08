using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform obj;
    public Transform reciever;

    private bool objectIsOverlapping;

    // Update is called once per frame
    void Update()
    {
        if (objectIsOverlapping)
        {
            Vector3 portalToPlayer = obj.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            
            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;

                obj.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                obj.position = reciever.position + positionOffset;

                objectIsOverlapping = false;

                 
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Object")
        {
            objectIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Object")
        {
            objectIsOverlapping = false;
        }
    }
}
