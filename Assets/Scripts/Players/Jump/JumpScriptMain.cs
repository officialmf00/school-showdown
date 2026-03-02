using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.TextCore.Text;

public abstract class JumpScriptMain : MonoBehaviour
{
    public enum Player
    {
        P1, P2
    }

    [SerializeField] protected float jumpHeight;

    [SerializeField] protected Player player;

    private Controls controls;

    private Rigidbody rb;

    private bool canJump;

    [SerializeField] protected float cooldownTime;

    [SerializeField] protected float doubleJumpActivateTime;

    private bool doubleJumpActive = true;

    [SerializeField] private Animator charAnimator;

    private void Awake()
    {
        canJump = true;
        controls = new Controls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {

        if (player == Player.P1)
        {
            controls.Fighting.JumpP1.performed += Jump;
            controls.Fighting.JumpP1.Enable();
        }
        else if (player == Player.P2)
        {
            controls.Fighting.JumpP2.performed += Jump;
            controls.Fighting.JumpP2.Enable();
        }

    }

    private void OnDisable()
    {
        controls.Fighting.JumpP1.Disable();
        controls.Fighting.JumpP2.Disable();
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        if (doubleJumpActive == true && canJump == false)
        {
            StartCoroutine(UseDoubleJump());
        }
        if (canJump == true)
        {
            canJump = false;
            if (charAnimator != null)
            {
                charAnimator.SetTrigger("Jump");
            }
            rb.AddForce(Vector3.up * jumpHeight / 2, ForceMode.Impulse);
            StartCoroutine(JumpCooldown());
        }
    }



    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canJump = true;
        doubleJumpActive = true;
    }
    private IEnumerator UseDoubleJump()
    {
        doubleJumpActive = false;
        yield return new WaitForSeconds(doubleJumpActivateTime);
        if (doubleJumpActive == false)
        {
            //float turn = Input.GetAxis("Horizontal");
            if (charAnimator != null)
            {
                charAnimator.SetTrigger("DoubleJump");
            }
            rb.AddForce(Vector3.up * jumpHeight / 1.25f, ForceMode.Impulse);
        }
    }
}
