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

        woodCounter.text = currentWood.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool AddWood(int v)
    {
        if (currentWood + v >= 0)
        {
            Debug.Log($"Adding wood: {v}");
            currentWood += v;
            woodCounter.text = currentWood.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
}
