using UnityEngine;

public class BushState : MonoBehaviour, IInteractable
{
    const int plentifulThreshold = 3;

    public FoodCounter foodCounter;
    public int BerryCount;
    public GameObject beaver;
    public Outline outline;
    public bool IsInteractable { get; set; }
    public int nextRipe = -2;
    public int curDay = 0;

    [SerializeField] Sprite plentifulSprite;
    [SerializeField] Sprite someSprite;
    [SerializeField] Sprite emptySprite;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Initialize(4);
    }

    //Gets the day from the game manager
    public void GetDay(Component sender, object data)
    {
        if (data is int)
        {
            Debug.Log("Bushes heard day event");
            curDay = (int)data;
            if (curDay == nextRipe)
            {
                Initialize(4);
            }
        }
    }

    public void Start()
    {
        IsInteractable = true;
        beaver = GameObject.Find("Beaver");
        foodCounter = beaver.GetComponent<FoodCounter>();
        outline = GetComponentInChildren<Outline>();
        outline.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void Highlight(bool on)
    {
        Debug.Log( $"{IsInteractable} that I am interactable");
        if (on && IsInteractable)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
    public void Interact()
    {
        Debug.Log("Bush interacted with!");
        if (BerryCount > 0)
        {
            audioSource.Play();
            spriteRenderer.sprite = emptySprite;
            CollectBerries();

            Debug.Log($"Berry gave {BerryCount} berries to the beaver");
            nextRipe = curDay + Random.Range(2, 5);
            BerryCount = 0;
        }

    }
    private void CollectBerries()
    {
        IsInteractable = false;
        Highlight(false);
        foodCounter.AddFood(BerryCount);
    }
    public void Initialize(int berryCount)
    {
        Debug.Log($"Bush initialized with {berryCount} berries.");
        IsInteractable = true;
        BerryCount = berryCount;
        if (BerryCount >= plentifulThreshold)
        {
            spriteRenderer.sprite = plentifulSprite;
        }
        else
        {
            spriteRenderer.sprite = someSprite;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
