using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput inputActions;
    private Vector2 rawInput;
    private bool isInteracting;
    private CharacterController characterController;

    [SerializeField]
    public FloatReference InteractionDistance;

    [SerializeField]
    public Camera CurrentCamera;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new PlayerInput();
        CurrentCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.started += OnInteract;  
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Interact.canceled -= OnInteract;
        inputActions.Disable();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Vector2 mouseWorldPosition = CurrentCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (new Ray(characterController.transform.position, mouseWorldPosition)).direction;
        RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPosition, direction, InteractionDistance);
        
        if (hitInfo)
        {
            Debug.DrawLine(characterController.transform.position, mouseWorldPosition, Color.yellow, 10f);
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(gameObject);
            }
        }
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
