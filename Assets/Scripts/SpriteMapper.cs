using UnityEngine;

[ExecuteAlways]
public class SpriteMapper : MonoBehaviour
{
    public GameObject target3DObject; // Reference to your 3D object
    public float sizeMultiplier = 0.001f; // Additional scaling factor

    void Update()
    {
        if (target3DObject == null) return;

        // Get the 3D object's bounds
        Renderer renderer3D = target3DObject.GetComponent<Renderer>();
        if (renderer3D == null) return;

        // Get the sprite's bounds
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        // Calculate the scale needed to match widths
        float targetWidth = renderer3D.bounds.size.x;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        float scale = (targetWidth / spriteWidth) * sizeMultiplier;

        // Apply uniform scale
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
