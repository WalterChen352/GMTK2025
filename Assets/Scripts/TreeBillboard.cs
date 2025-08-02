using UnityEngine;

public class TreeBillboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform cam;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;
        if (cam == null) cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (parentTransform == null || cam == null) return;

        // Get parent's Y rotation only (for 2D-style sprites)
        float parentYRotation = parentTransform.eulerAngles.y;

        // Face the camera but maintain parent's Y rotation
        Vector3 lookDirection = cam.position - transform.position;
        lookDirection.y = 0; // Keep sprite upright

        transform.rotation = Quaternion.LookRotation(lookDirection) *
                            Quaternion.Euler(0, parentYRotation, 0);

        // Alternative: Full 3D alignment with parent
        // transform.rotation = parentTransform.rotation * 
        //                    Quaternion.LookRotation(lookDirection);
    }
}
