using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WoodCounter : MonoBehaviour
{
    public static WoodCounter instance;
    public TMP_Text woodCounter;
    public int currentWood = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        woodCounter.text = "Wood: " + currentWood.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddWood(int v)
    {
        currentWood += v;
            woodCounter.text = "Wood: " + currentWood.ToString();
    }
}
