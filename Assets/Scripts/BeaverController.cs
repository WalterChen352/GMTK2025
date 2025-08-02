using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeaverController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StandardInputs input;
    [SerializeField] private SphereCollider interactionZone;

    [SerializeField] Transform cam;
    private Vector3 move;
    private readonly List<IInteractable> nearbyInteractables = new();
    private Vector2 moveInput;
    private Rigidbody rb;
    private EnergySystem energySystem;
    private WoodCounter woodCounter;
    private FoodCounter foodCounter;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        energySystem = GetComponent<EnergySystem>();
        woodCounter = GetComponent<WoodCounter>();
        foodCounter = GetComponent<FoodCounter>();

    }
    public void Start()
    {

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        //Button Clicked
        if (context.phase == InputActionPhase.Started)
        {
            //Debug.Log(context.ReadValue<Vector2>());
        }
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Beaver interacting!");
            if (nearbyInteractables.Count > 0)
            {
                nearbyInteractables[0].Interact();
            }
        }
    }

    public void Update()
    {

    }
    public void FixedUpdate()
    {
        Vector3 moveDir = calcDirRelToCam(cam, moveInput);
        rb.linearVelocity = new Vector3(moveDir.x, 0, moveDir.z) * speed;
        if (moveInput != Vector2.zero) //it moved
        {
            energySystem.UseEnergy(.25f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering collision");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Highlight(true);
            nearbyInteractables.Add(interactable);
            Debug.Log($"Adding interactable. Interactables count is now {nearbyInteractables.Count}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leaving collision");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Highlight(false);
            nearbyInteractables.Remove(interactable);
            Debug.Log($"Removing interactable. Interactables count is now {nearbyInteractables.Count}");
        }
    }
    Vector3 calcDirRelToCam(Transform transform, Vector3 moveInput)
    {
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;
        move = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 camForward = transform.forward;
        Vector3 camRight = transform.right;
        camForward.y = 0;
        camRight.y = 0;
        Vector3 forwardRelative = camForward * verticalInput;
        Vector3 rightRelative = camRight * horizontalInput; ;
        return forwardRelative + rightRelative;
}
}

