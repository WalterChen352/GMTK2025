using UnityEngine;

public class BushState : MonoBehaviour, IInteractable
{
    public FoodCounter foodCounter;
    public int BerryCount;
    public GameObject beaver;
    public Outline outline;
    public bool IsInteractable { get; set; }
    public void Start()
    {
        beaver = GameObject.Find("Beaver");
        foodCounter = beaver.GetComponent<FoodCounter>();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Highlight(bool on)
    {
        if (on && IsInteractable)
        {
            outline.enabled = true;
        } else {
            outline.enabled = false;
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
        IsInteractable = false;
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
