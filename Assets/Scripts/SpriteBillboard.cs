using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Sprite sprite;
    public Vector3 CamFoward;
    void Start()
    {
        
    }

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     sprite.Transform.LookAt(cam);
    //     sprite.Transform.Rotate(0, 180, 0);
    // }
}
