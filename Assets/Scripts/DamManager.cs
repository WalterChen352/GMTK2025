
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamManager : MonoBehaviour
{
    public float waterLevel = 0f;
    public float damLevel = 5f;
    public float maxDam = 100f;
    private int spentWood;
    public int consumedWood = 0;
    private float maxWood;
    private float waterBaseHeight;
    public WoodCounter woodHolder;
    [Tooltip("Defined area for min and max water/dam levels.")]
    [SerializeField] private RectTransform damUIBounds;

    [SerializeField] private GameObject damLine;
    [SerializeField] private RectTransform waterBounds;
    [SerializeField] private TextMeshProUGUI woodCounter;

    private float uIHeight;

    void Start()
    {
        uIHeight = damUIBounds.sizeDelta.y;
        maxWood = woodHolder.currentWood;
        waterBaseHeight = waterBounds.rect.height;
        float relativeDist = damLevel * uIHeight / maxDam;
        MoveBar(relativeDist);
        //StartUp();
    }

    void MoveBar(float relDist)
    {
        damLine.transform.position += new Vector3(0.0f, relDist, 0.0f);
    }

    public void AddWood(int inputWood)
    {
        Debug.Log("Calling wood");
        if (CheckCanSpend(inputWood))
        {
            SpendWood(inputWood);
        }
        //damLine.transform.position += new Vector3(0f, relativeDist, 0f);
        //StartCoroutine(MoveBar(relativeDist, 0.2f));
    }
    void SpendWood(int inputWood)
    {
        Debug.Log($"Spending wood: {inputWood}");
        spentWood += inputWood;
        woodCounter.text = spentWood.ToString();
        woodHolder.AddWood(-1 * inputWood);
        float relativeDist = inputWood * uIHeight / maxDam;
        MoveBar(relativeDist);
    }
    bool CheckCanSpend(int inputWood)
    {
        if (woodHolder.currentWood - inputWood < 0)
        {
            Debug.Log("Failed because you are broke");
            return false;
        }
        else if (spentWood + inputWood < 0)
        {
            Debug.Log("Failed because you can't take more wood out of the dam");
            return false;
        }
        else
        {
            Debug.Log("Passed!");
            return true;
        }
        //If the player is trying to add wood to the pile but doesn't have wood to add, is trying to add wood back to their inventory or is trying to add wood from the pile
    }

    public void StartUp()
    {
        Debug.Log("Running Dam Startup");
        spentWood = 0;
        woodCounter.text = "0";
        maxWood = woodHolder.currentWood;
        UpdateWater(2, 5);
    }

    public void UpdateWater(int min, int max)
    {
        Debug.Log("Adding water");
        int addedWater =Random.Range(min, max);
        waterLevel += addedWater;
        float levelChange = waterLevel * uIHeight / maxDam;
        waterBounds.sizeDelta = new Vector2(waterBounds.sizeDelta.x, waterBaseHeight + levelChange);
        waterBounds.transform.position += new Vector3(0f, addedWater * uIHeight / maxDam * 0.5f);
        //waterBounds.sizeDelta = new Vector2(waterBounds.sizeDelta.x, 1000);

    }
    public void Confirm()
    {
        damLevel += spentWood;
        spentWood = 0;
        CheckLevel();
    }

    bool CheckLevel()
    {
        return true;
    }
}
