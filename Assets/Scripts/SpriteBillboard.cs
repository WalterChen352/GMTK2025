using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] Transform cam;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CamForward = cam.forward;
        CamForward.y = 0;
        transform.forward = CamForward;
        // cam.y = 0;
        // transform.LookAt(cam);
    }
}
