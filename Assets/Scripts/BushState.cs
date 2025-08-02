using UnityEngine;

public class BushState : MonoBehaviour, IInteractable
{
    public int BerryCount;
    public void Interact()
    {
        Debug.Log("Bush interacted with!");
        if(BerryCount > 0)
        {
            //give berries to the beaver

            
            Debug.Log($"Berry gave {BerryCount} berries to the beaver");
            BerryCount = 0;
        }

    }

    public void Initialize(int berryCount)
    {
        BerryCount = berryCount;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
