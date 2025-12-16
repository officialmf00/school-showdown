using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
public abstract class MovementMain : MonoBehaviour
{
    public enum Player
    {
        P1, P2
    }

    [SerializeField] protected float walkSpeed;

    [SerializeField] protected Player player;

    [SerializeField] protected CharacterMain character;

    private float maxRaycastDistance = 1;

    [SerializeField] protected GameObject charVisual;

    private float savedMovementSpeed;

    private Controls controls;
    protected Vector2 walkInput;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (player == Player.P1)
        {
            controls.Fighting.WalkP1.performed += WalkPerformed;
            controls.Fighting.WalkP1.canceled += WalkCanceled; // released

            controls.Fighting.WalkP1.Enable();
        }
        else if (player == Player.P2)
        {
            controls.Fighting.WalkP2.performed += WalkPerformed;
            controls.Fighting.WalkP2.canceled += WalkCanceled; // released

            controls.Fighting.WalkP2.Enable();
        }


    }

    private void OnDisable()
    {
        controls.Fighting.WalkP1.performed -= WalkPerformed;
        controls.Fighting.WalkP1.canceled -= WalkCanceled; // released
        controls.Fighting.WalkP1.Disable();

        controls.Fighting.WalkP2.performed -= WalkPerformed;
        controls.Fighting.WalkP2.canceled -= WalkCanceled; // released
        controls.Fighting.WalkP2.Disable();
    }

    protected virtual void WalkPerformed(InputAction.CallbackContext ctx)
    {
        walkInput = ctx.ReadValue<Vector2>(); // (-0.71, 0.71)

        Animator animator = character.GetAnimator();

        animator.SetBool("WalkBool", true);

        print(walkInput);

    }

    private void WalkCanceled(InputAction.CallbackContext ctx)
    {
        walkInput = Vector2.zero;

        Animator animator = character.GetAnimator();

        animator.SetBool("WalkBool", false);
    }

    public void changeMovementSpeed(float speed, bool revertSpeed)
    {

       print("walkSpeed is " + walkSpeed);

        if (revertSpeed == true)
        {
            walkSpeed = savedMovementSpeed;
        }
        else
        {
            savedMovementSpeed = walkSpeed;
            walkSpeed = speed;
        }
    }

    private void Update()
    {

        Vector3 movementVector = new(walkInput.x, 0, walkInput.y);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRaycastDistance))
        {

            if (movementVector.z > 0 && hit.collider.gameObject.tag == "Player")
            {
                movementVector.z = 0;
            }

    


        }
        else if (Physics.Raycast(transform.position, transform.forward * -1, out hit, maxRaycastDistance))
        {

            if (movementVector.z < 0 && hit.collider.gameObject.tag == "Player")
            {
                movementVector.z = 0;
            }


        }
        transform.Translate(movementVector * walkSpeed * Time.deltaTime);

    }


}
