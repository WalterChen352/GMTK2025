using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        if (currentEnergy <= 0)
        {
            Debug.Log("Run death behavior");
            Death();
        }
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

    public void SetEnergy(float energyAmount)
    {
        currentEnergy = energyAmount;
        Debug.Log($"Set energy to {currentEnergy}");
        energySlider.value = currentEnergy;
    }
    public void Death()
    {
        //Deathscreen
        SceneManager.LoadScene(4);
    }
}

