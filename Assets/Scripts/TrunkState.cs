using UnityEngine;

public class TrunkState : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    const int minWood = 3;
    const int maxWood = 5;
    public Outline outline;
    public bool IsInteractable { get; set; }
    public int WoodCount;
    void Start()
    {
        IsInteractable = true;
        WoodCount=Random.Range(minWood,maxWood+1);
        outline = GetComponent<Outline>();
        outline.enabled=false;
    }

    public void Interact()
    {
        if (IsInteractable)
        {
            IsInteractable = false;

            var beaver = GameObject.FindWithTag("Player");
            beaver.GetComponent<WoodCounter>().AddWood(WoodCount);

            //TODO: sound effect?
            gameObject.SetActive(false);
        }
        

    }

    public void Highlight(bool on)
    {
        if (on && IsInteractable)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
