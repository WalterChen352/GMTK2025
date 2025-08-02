using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    Transform cam;

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
        //CamForward.y = 0;
        transform.forward = CamForward;
        // cam.y = 0;
        // transform.LookAt(cam);
    }
}
