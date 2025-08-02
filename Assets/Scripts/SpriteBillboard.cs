using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerialzeField] Sprite sprite;
    public Vector3 CamFoward;
    void Start()
    {
        CamFoward = cam.foward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
