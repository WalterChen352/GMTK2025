using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeaverController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StandardInputs input;
    [SerializeField] private SphereCollider interactionZone;
    private readonly List<IInteractable> nearbyInteractables = new();
    private Vector2 moveInput;
    private Rigidbody rb;
    private EnergySystem energySystem;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        energySystem = GetComponent<EnergySystem>();

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
            if(nearbyInteractables.Count > 0)
            {
                nearbyInteractables[0].Interact();
            }
        }
    }

    public void FixedUpdate()
    {

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = move * speed; 
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

            nearbyInteractables.Add(interactable);
            Debug.Log($"Adding interactable. Interactables count is now {nearbyInteractables.Count}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leaving collision");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            
            nearbyInteractables.Remove(interactable);
            Debug.Log($"Removing interactable. Interactables count is now {nearbyInteractables.Count}");
        }
    }

}
