using UnityEngine;
using UnityEngine.InputSystem;

public class BeaverController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StandardInputs input;
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


    public void OnMove(InputAction.CallbackContext context)
    {
        //Button Clicked
        if (context.phase == InputActionPhase.Started)
        {
            //Debug.Log(context.ReadValue<Vector2>());
        }
        moveInput = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = move * speed;
        if (moveInput != Vector2.zero) //it moved
        {
            energySystem.UseEnergy(.25f);
            foodCounter.AddFood(1);
        }
    }

}
