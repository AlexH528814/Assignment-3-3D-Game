using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3f;

    public Transform interactionTransform;

    bool isFocus = false;
    public Transform player;

    bool hasInteracted = false;

    private void Update()
    {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (Input.GetMouseButtonDown(0) && distance <= radius)
            {
                hasInteracted = true;
                Interact();
            } 
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {

    }

}
