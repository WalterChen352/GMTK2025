using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FoodCounter : MonoBehaviour
{
    public static FoodCounter instance;
    public TMP_Text foodCounter;
    public int currentFood = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foodCounter.text = currentFood.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AddFood(int v)
    {
        currentFood += v;
        foodCounter.text = currentFood.ToString();
        if (currentFood+ v >= 0)
        {
            Debug.Log($"Adding wood: {v}");
            currentFood += v;
            foodCounter.text = currentFood.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
}
