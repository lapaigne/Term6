using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput inputActions;
    private CharacterController characterController;
    private PlayerData playerData;
    private Vector2 rawInput;

    private Vector2 mouseWorldPosition;
    private Vector2 mouseDirection;
    
    [SerializeField]
    public Camera currentCamera;

    [SerializeField]
    public float cameraLerpFactor;
    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        characterController = GetComponent<CharacterController>();
        inputActions = new PlayerInput();
        currentCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.started += OnInteract;
        inputActions.Player.Shoot.started += OnShoot;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Shoot.canceled -= OnShoot;
        inputActions.Player.Interact.canceled -= OnInteract;
        inputActions.Disable();
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        // Manual navigation rocket
        Transform child = transform.Find("Hand");
        if (child != null)
        {
            child.transform.SetParent(null);
            child.transform.rotation = Quaternion.identity;
            child.GetComponent<HandController>().Launch(mouseWorldPosition - (Vector2)child.transform.position);
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        // can use this for shooting instead
        RaycastHit2D hitInfo = Physics2D.Raycast(
            characterController.transform.position, 
            mouseDirection, 
            playerData.interactionDistance
            );
        
        if (hitInfo)
        {
            Debug.DrawLine(characterController.transform.position, mouseWorldPosition, Color.yellow, playerData.interactionDistance);
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
        Vector2 movement = rawInput;
        movement.Normalize();
        movement *= playerData.speed * Time.deltaTime;
        characterController.Move(movement);

        mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = mouseWorldPosition - (Vector2)characterController.transform.position;

        if (mouseDirection.magnitude > 0.1f)
        {
            float rotationAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        // TODO: improve lerping
        Vector2 newCameraPosition = Vector2.Lerp(currentCamera.transform.position, gameObject.transform.position, cameraLerpFactor);
        currentCamera.transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, -10);
    }
}
