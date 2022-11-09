using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public float portalRange = 1000f;

    private Portal[] portals;

    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindObjectsOfType<Portal>();
        if (portals.Length != 2)
        {
            Debug.Log("You need to have 2 Portals");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Vector3 position = Camera.main.transform.position;
            RaycastHit raycastHit = new RaycastHit();
            if (Physics.Linecast(position, position + Camera.main.transform.position, out raycastHit, layerMask))
            {
                int index = Input.GetMouseButtonDown(0) ? 0 : 1;
                portals[index].MovePortal(raycastHit);
            }
        }
    }
}
