using UnityEngine;

public class BushTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var bushes = FindObjectsByType<BushState>(FindObjectsSortMode.None);
        Debug.Log($"Found {bushes.Length} bushes in the scene.");
        if (bushes.Length > 0)
        {
            bushes[0].Initialize(5);
        }
        if (bushes.Length > 1)
        {
            bushes[1].Initialize(2);
        }
    }
}
