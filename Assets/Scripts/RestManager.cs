
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RestManager : MonoBehaviour
{

    [Tooltip("How much energy each food gives")]
    public float foodValue = 2.0f;
    private int spentFood; //How much food the player is spending this turn
    [SerializeField]
    private EnergySystem playerEnergy;
    [SerializeField]
    private FoodCounter playerFood;
    [SerializeField]
    private TextMeshProUGUI foodCounter;

    public void AddFood(int inputFood)
    {
        Debug.Log($"Calling food for {inputFood} food.");
        if (CheckCanSpend(inputFood))
        {
            SpendFood(inputFood);
        }
        //damLine.transform.position += new Vector3(0f, relativeDist, 0f);
        //StartCoroutine(MoveBar(relativeDist, 0.2f));
    }

    void Start()
    {
        //StartUp();
    }

    //DELETE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartUp();
        }
    }

    //Removes or adds food to the big bar to generate energy, removing the food from their inventory
    void SpendFood(int inputFood)
    {
        Debug.Log($"Spending food: {inputFood}");
        spentFood += inputFood;
        foodCounter.text = spentFood.ToString();
        playerFood.AddFood(-1 * inputFood); //Takes food from player inventory
        MoveBar(inputFood * foodValue);
    }

    //No behavior for negative energy rn, but shouldn't be able to go negative
    void MoveBar(float inputEnergy)
    {
        playerEnergy.AddEnergy(inputEnergy);
    }
    bool CheckCanSpend(int inputFood)
    {
        if (playerFood.currentFood - inputFood < 0)
        {
            Debug.Log("Failed because you are broke");
            return false;
        }
        else if (spentFood + inputFood < 0)
        {
            Debug.Log("Failed because you can't make negative food");
            return false;
        }
        else
        {
            Debug.Log("Passed!");
            return true;
        }
        //If the player is trying to add wood to the pile but doesn't have wood to add, is trying to add wood back to their inventory or is trying to add wood from the pile
    }

    public void StartUp()
    {
        playerEnergy.SetEnergy(0);
        spentFood = 0;
        foodCounter.text = spentFood.ToString();
    }
    public void Confirm()
    {
        spentFood = 0;
        BeginDay();
    }

    bool BeginDay()
    {
        return true;
    }
}
