using UnityEngine;

public class TreeBillboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform cam;
    public float Offset;
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
