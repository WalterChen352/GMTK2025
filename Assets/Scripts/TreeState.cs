using UnityEngine;

public class TreeState : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Health;
    void Start()
    {
        
    }

    public void Initialize(int health)
    {
        Health = health;
    }

    public void Interact()
    {
        Debug.Log("Tree interacted with");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
