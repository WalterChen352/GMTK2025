using UnityEngine;
using UnityEngine.InputSystem;

public class SpeechBubbleTester : MonoBehaviour
{
    [SerializeField] private SpeechBubble speechBubble;
    bool isActive = false;

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("CONTEXTS!");
        if (context.started)
        {
            Debug.Log("Speech bubble!");
            if (isActive)
            {
                speechBubble.Hide();
                isActive = false;
            }
            else
            {
                speechBubble.SetWords($"The current time is {System.DateTime.Now:T}");
                speechBubble.Show();
                isActive = true;
            }
        }
    }
}
