using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput inputActions;
    private Vector2 rawInput;
    private bool isInteracting;
    
    [SerializeField]
    private CharacterController characterController;

    
    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.performed += ctx => { isInteracting = true; };
        inputActions.Player.Interact.canceled += ctx => { isInteracting = false; };
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        rawInput = ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        Vector3 movement = new Vector3(rawInput.x, rawInput.y, 0);
        movement.Normalize();
        movement *= 5f * Time.deltaTime;
        characterController.Move(movement);
    }
}
