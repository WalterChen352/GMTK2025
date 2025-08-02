using UnityEngine;

public class TreeState : MonoBehaviour, IInteractable
{
    public int Hitpoints;
    public WoodCounter woodCounter;
    public GameObject beaver;
    public bool IsInteractable { get; set; }
    public Outline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beaver = GameObject.Find("Beaver");
        woodCounter = beaver.GetComponent<WoodCounter>();
        outline = GetComponent<Outline>();
        Hitpoints = 4;
        IsInteractable = true;
        outline.enabled = false;
    }

    public void Interact()
    {
        if (IsInteractable == true)
        {
            ChopTree();
            Debug.Log("Tree interacted with");
        }
        
    }
    public void Highlight(bool on)
    {
        if (on  && IsInteractable)
        {
            outline.enabled = true;
        } else {
            outline.enabled = false;
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
        IsInteractable = false;
        outline.enabled = false;
        Debug.Log("Tree Fallen Down!");
    }
    public void CollectWood()
    {
        woodCounter.AddWood(4);
    }
}
