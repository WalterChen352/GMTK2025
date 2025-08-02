using UnityEngine;
using UnityEngine.InputSystem;

public class BeaverController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StandardInputs input;
    [SerializeField] private SpherecastCommand interactionZone;
    private Vector2 moveInput;
    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }

    public void FixedUpdate()
    {

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = move * speed;
    }

}
