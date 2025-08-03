using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform cam;
    public float Offset;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Make the sprite face the camera while maintaining the camera's up direction
        transform.rotation = Quaternion.LookRotation(
            transform.position - cam.position,
            cam.up
        );

        // Optional: Apply any offset you might want
        transform.rotation *= Quaternion.Euler(0, Offset, 0);
    }
}
