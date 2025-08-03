using System.Collections;
using UnityEngine;

public enum gameStates {Active, SemiPaused, Paused}
public enum dayStates {Forage, Dam, Eat}
public class GameManager : MonoBehaviour
{
    public int day = 0;
    public gameStates gameState;
    public dayStates dayState;
    [Header("UI")]
    [SerializeField] Animator uIAnimator;
    [Header("Managers")]
    [SerializeField] DamManager damManager;
    [SerializeField] RestManager restManager;
    [SerializeField] EnergySystem energySystem;
    public GameEvent currentDay;
    int enableFadeHash;
    int disableFadeHash;
    int swapHash;
    void Start()
    {
        gameState = gameStates.SemiPaused;
        dayState = dayStates.Dam;
        damManager.StartUp();
        if (uIAnimator != null)
        {
            enableFadeHash = Animator.StringToHash("Base Layer.EnableFade");
            disableFadeHash = Animator.StringToHash("Base Layer.DisableFade");
            swapHash = Animator.StringToHash("Base Layer.SwapToRest");
            uIAnimator.Play(enableFadeHash, 0, 0f);
        }
    }

    public void AnnounceEvent(Component sender, object data)
    {
        if (data is int)
        {
            Debug.Log($"Current day: {data}");
        }
    }

    public void AdvanceState(Component sender, object data)
    {
        AdvanceState();
    }

    public void AdvanceState()
    {
        switch (dayState)
        {
            case dayStates.Forage:
                gameState = gameStates.SemiPaused;
                uIAnimator.Play(enableFadeHash, 0, 0f);
                damManager.StartUp();
                dayState = dayStates.Dam;
                break;

            case dayStates.Dam:
                StartCoroutine(DrainEnergy());
                //restManager.StartUp();
                //dayState = dayStates.Eat;
                break;

            case dayStates.Eat:
                gameState = gameStates.Active;
                uIAnimator.Play(disableFadeHash);
                dayState = dayStates.Forage;
                AdvanceDay();
                break;
        }
    }

    public void AdvanceDay()
    {
        day += 1;
        currentDay.Raise(this, day);
    }
    IEnumerator DrainEnergy()
    {
        while (energySystem.currentEnergy > 4)
        {
            energySystem.UseEnergySafe(0.5f);
            yield return null;
        }

        uIAnimator.Play(swapHash, 0, 0f);
        restManager.StartUp();
        dayState = dayStates.Eat;
    }

    void TriggerFade(bool input)
    {
        Debug.Log("Triggering Fade: " + input);
        if (input)
        {
            uIAnimator.Play(enableFadeHash, 0, 0f);
        }
        else
        {
            uIAnimator.Play(disableFadeHash, 0, 0f);
        }
    }
}
