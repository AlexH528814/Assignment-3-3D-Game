using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimator : MonoBehaviour
{
    const float smoothSpeed = .1f;

    PlayerMovement PlayerMovement;
    Animator animator;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = rb.velocity.magnitude / PlayerMovement.speed;
        animator.SetFloat("speedPercent", speedPercent, smoothSpeed, Time.deltaTime);
    }
}
