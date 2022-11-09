using UnityEngine;
using System.Collections;

/* 
 * Released under the creative commons attribution license.
 * Do whatever you like with the code, just give credit to Stuart Spence.
 * https://creativecommons.org/licenses/by/3.0/
 */ 

public class PortalGun : MonoBehaviour
{
    public float gunRange = 1000;
    public AudioClip shootSound, teleportSound;
    private Portal[] portals;
    private Portal portal;

    public LayerMask portalableSurface;


    void Start()
    {
        portals = GameObject.FindObjectsOfType<Portal>();
        if (portals.Length != 2)
            Debug.LogError("You need to attach the Portal script to exactly two gameobjects in the world.");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Vector3 position = Camera.main.transform.position;
            RaycastHit rayCastHit = new RaycastHit();
            if (Physics.Linecast(position, position + Camera.main.transform.forward * gunRange, out rayCastHit, portalableSurface))
            {

                int index = Input.GetMouseButtonDown(0) ? 0 : 1;
                portals[index].MovePortal(rayCastHit);

            }
        }
    }
}
