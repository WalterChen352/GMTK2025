using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeaverController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private StandardInputs input;
    [SerializeField] private SphereCollider interactionZone;

    [SerializeField] Transform cam;

    [SerializeField] Sprite towardsSprite;
    [SerializeField] Sprite towardsLeftSprite;
    [SerializeField] Sprite towardsRightSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite awayLeftSprite;
    [SerializeField] Sprite awayRightSprite;
    [SerializeField] Sprite awaySprite;

    private Vector3 move;
    private readonly List<IInteractable> nearbyInteractables = new();
    private Vector2 moveInput;
    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private EnergySystem energySystem;
    private WoodCounter woodCounter;
    private FoodCounter foodCounter;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            if (-157.5 <= angle && angle < -112.5)
            {
                spriteRenderer.sprite = towardsLeftSprite;
            }
            else if (-112.5 <= angle && angle < -67.5)
            {
                spriteRenderer.sprite = towardsSprite;
            }
            else if (-67.5 <= angle && angle < -22.5)
            {
                spriteRenderer.sprite = towardsRightSprite;
            }
            else if (-22.5 <= angle && angle < 22.5)
            {
                spriteRenderer.sprite = rightSprite;
            }
            else if (22.5 <= angle && angle < 67.5)
            {
                spriteRenderer.sprite = awayRightSprite;
            }
            else if (67.5 <= angle && angle < 112.5)
            {
                spriteRenderer.sprite = awaySprite;
            }
            else if (112.5 <= angle && angle < 157.5)
            {
                spriteRenderer.sprite = awayLeftSprite;
            }
            else if (157.5 <= angle || angle < -157.5)
            {
                spriteRenderer.sprite = leftSprite;
            }
        }
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

