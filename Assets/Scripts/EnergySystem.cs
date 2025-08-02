using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public Slider energySlider;
    public float maxEnergy = 100f;
    public float currentEnergy;
    private void Start()
    {
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
        // Debug.Log(currentEnergy);
    }
    public void UseEnergy(float energyCost)
    {
        currentEnergy -= energyCost;
        // Debug.Log($"Used {energyCost} to beaver for total of {currentEnergy}");
        energySlider.value = currentEnergy;

    }

    public void AddEnergy(float energyAmount)
    {
        currentEnergy = Mathf.Min(currentEnergy + energyAmount, maxEnergy);
        Debug.Log($"Added {energyAmount} to beaver for total of {currentEnergy}");
        energySlider.value = currentEnergy;
    }
    public float EnergyValue()
    {
        return currentEnergy;
    }

    }