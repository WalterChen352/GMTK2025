using UnityEngine;

public class ObjectBillboard : MonoBehaviour
{
    Transform cam;
    [SerializeField] Transform sprite;
    void Start()
    {

    }

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CamForward = cam.forward;
        sprite.transform.forward = CamForward;
        CamForward.y = 0;
        transform.forward = CamForward;
        // cam.y = 0;
        // transform.LookAt(cam);
    }
}
