using UnityEngine;

public class DamEntrance : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public Outline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsInteractable = true;
        outline = GetComponentInChildren<Outline>();
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
        Debug.Log("Ending day...");

    }


}
