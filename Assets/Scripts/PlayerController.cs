using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MoveableObject
{
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject weaponContainer;
    [SerializeField] Animator playerAnim;

    private Vector3 forceDirection;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float movementForce = 1f;
    private Vector2 m_move;

    public int playerHealth = 5;

    private void Awake()
    {
        thisRb = this.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        forceDirection += m_move.x * movementForce * GetCameraRight(playerCamera);
        forceDirection += m_move.y * movementForce * GetCameraForaward(playerCamera);

        if(playerHealth != 0) 
            Move(forceDirection);
        forceDirection = Vector3.zero;
        LookAt();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_move = context.ReadValue<Vector2>();
    }
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForaward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(IsOnGround())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }
    private bool IsOnGround()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.3f))
            return true;
        else
            return false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(IsOnGround())
            {
                playerAnim.SetTrigger("Attack");
            }
            else
            {
                forceDirection -= Vector3.up * jumpForce * 5;
                playerAnim.SetTrigger("Attack");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && playerHealth > 0)
        {
            playerHealth--;
        }
    }
}
