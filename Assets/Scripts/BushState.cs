using UnityEngine;

public class BushState : MonoBehaviour, IInteractable
{
    public FoodCounter foodCounter;
    public int BerryCount;
    public GameObject beaver;
    public void Start()
    {
        beaver = GameObject.Find("Beaver");
        foodCounter = beaver.GetComponent<FoodCounter>();
        if (foodCounter == null)
        {
            Debug.Log("NOOOO");
        }
    }

    public void Interact()
    {
        Debug.Log("Bush interacted with!");
        if (BerryCount > 0)
        {
            CollectBerries();

            Debug.Log($"Berry gave {BerryCount} berries to the beaver");
            BerryCount = 0;
        }

    }
    private void CollectBerries()
    {
        foodCounter.AddFood(BerryCount);
    }
    public void Initialize(int berryCount)
    {
        BerryCount = berryCount;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
