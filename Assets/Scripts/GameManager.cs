using UnityEngine;

enum gameStates {Active, SemiPaused, Paused} //May want to add a LineOn state so we can lerp the camera towards the bobber

public class GameManager : MonoBehaviour
{
    gameStates gameState;
    [Header("UI")]
    [SerializeField] Animator uIAnimator;
    int fadeInHash;
    int fadeOutHash;
    bool flipFlop = false;
    void Start()
    {
        gameState = gameStates.Active;
        if (uIAnimator != null)
        {
            fadeInHash = Animator.StringToHash("Base Layer.EnableFade");
            fadeOutHash = Animator.StringToHash("Base Layer.DisableFade");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerFade(flipFlop);
            if (flipFlop)
            {
                flipFlop = false;
            }
            else
            {
                flipFlop = true;
            }
        }
    }

    void TriggerFade(bool input)
    {
        Debug.Log("Triggering Fade: " + input);
        if (input)
        {
            uIAnimator.Play(fadeInHash, 0, 0f);
        }
        else
        {
            uIAnimator.Play(fadeOutHash, 0, 0f);
        }
    }
}
