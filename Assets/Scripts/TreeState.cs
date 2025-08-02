using UnityEngine;

public class TreeState : MonoBehaviour, IInteractable
{
    public int Hitpoints;
    public WoodCounter woodCounter;
    public GameObject beaver;
    public bool FallenDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beaver = GameObject.Find("Beaver");
        woodCounter = beaver.GetComponent<WoodCounter>();
        Hitpoints = 4;
        FallenDown = false;
    }

    public void Interact()
    {
        if (FallenDown == false)
        {
            ChopTree();
            Debug.Log("Tree interacted with");
        }
        
    }

    public void ChopTree()
    {
        if (Hitpoints > 0)
        {
            Debug.Log("Chopping...");
            Hitpoints -= 1;
        }

        if (Hitpoints == 0)
        {
            TreeFall();
            CollectWood();

        }

    }
    public void TreeFall()
    {
        FallenDown = true;
        Debug.Log("Tree Fallen Down!");
    }
    public void CollectWood()
    {
        woodCounter.AddWood(4);
    }
}
