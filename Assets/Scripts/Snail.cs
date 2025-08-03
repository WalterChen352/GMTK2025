using Unity.VisualScripting;
using UnityEngine;

public enum snailStates {Idle, Wanting, Waiting, Offering}
public class Snail : MonoBehaviour, IInteractable
{
    [SerializeField] float minStartWaitTime;
    [SerializeField] float maxStartWaitTime;
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] float moveTime;
    [SerializeField] float moveSpeed;
    public snailStates snailState;
    public int demand = 2;
    public int demandItem = 1; //0 is wood, 1 is food
    public int curDay = 0;
    public int nextShift = 2;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Outline outline;
    SpeechBubble speechBubble;

    bool moving = false;
    float timeTilMove = 0f;
    float timeTilStop = 0f;
    Vector3 moveDirection = Vector3.zero;

    public bool IsInteractable { get; set; } = true;

    public void Interact()
    {
        if (IsInteractable)
        {
            WoodCounter wc = FindObjectsByType(typeof(WoodCounter), FindObjectsSortMode.None)[0].GetComponent<WoodCounter>();
            FoodCounter fc = FindObjectsByType(typeof(FoodCounter), FindObjectsSortMode.None)[0].GetComponent<FoodCounter>();
            switch (snailState)
            {
                case snailStates.Wanting:

                    if (demandItem == 0 && wc.AddWood(-1 * demand))
                    {
                        snailState = snailStates.Waiting;
                        nextShift = curDay + Random.Range(1, 4);

                    }
                    else if (demandItem == 1 && fc.AddFood(-1 * demand))
                    {
                        snailState = snailStates.Waiting;
                        nextShift = curDay + Random.Range(1, 4);
                    }
                    return;
                case snailStates.Offering:
                    if (demandItem == 0 && wc.AddWood(3 * demand))
                    {
                        snailState = snailStates.Idle;
                        nextShift = curDay + Random.Range(1, 3);
                    }
                    else if (demandItem == 1 && fc.AddFood(3 * demand))
                    {
                        snailState = snailStates.Idle;
                        nextShift = curDay + Random.Range(1, 3);
                    }
                    return;
            }
        }
    }
    //Gets the day from the game manager
    public void GetDay(Component sender, object data)
    {
        if (data is int)
        {
            Debug.Log("Bushes heard day event");
            curDay = (int)data;
            switch (snailState)
            {
                case snailStates.Wanting:
                    demand = Random.Range(1, 4);
                    demandItem = Random.Range(0, 2);
                    return;
                case snailStates.Idle:
                    if (curDay >= nextShift)
                    {
                        snailState = snailStates.Wanting;
                    }
                    return;
                case snailStates.Waiting:
                    if (curDay >= nextShift)
                    {
                        snailState = snailStates.Offering;
                    }
                    return;
                
            }
        }
    }

    public void Highlight(bool on)
    {
        if (on && IsInteractable)
        {
            animator.SetBool("Moving", false);
            timeTilMove = float.MaxValue;
            moving = false;
            outline.enabled = true;
            if (snailState == snailStates.Wanting && demandItem == 0)
            {
                speechBubble.SetWords($"{demand}x Wood to spare?");
                speechBubble.Show();
            }
            else if (snailState == snailStates.Wanting && demandItem == 1)
            {
                speechBubble.SetWords($"{demand}x Food to spare?");
                speechBubble.Show();
            }
            else if (snailState == snailStates.Offering && demandItem == 0)
            {
                speechBubble.SetWords($"Thank you! Here's {3 * demand} Food for your trouble!");
                speechBubble.Show();
            }
            else if (snailState == snailStates.Offering && demandItem == 1)
            {
                speechBubble.SetWords($"Thank you! Here's {3*demand} Wood for your trouble!");
                speechBubble.Show();
            }
        }
        else
        {
            if (!moving)
            {
                timeTilMove = Random.Range(minWaitTime, maxWaitTime);
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            }
            outline.enabled = false;
            speechBubble.Hide();
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        outline = GetComponentInChildren<Outline>();
        outline.enabled = false;
        speechBubble = GetComponentInChildren<SpeechBubble>();
        speechBubble.Hide();
        timeTilMove = Random.Range(minStartWaitTime, maxStartWaitTime);
        snailState = snailStates.Wanting;
    }

    private void Update()
    {
        if (moving)
        {
            timeTilStop -= Time.deltaTime;
            if (timeTilStop <= 0f)
            {
                animator.SetBool("Moving", false);
                timeTilMove = Random.Range(minWaitTime, maxWaitTime);
                moving = false;
            }
            else
            {
                if (moveDirection.x > 0f)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
                transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
            }
        }
        else
        {
            timeTilMove -= Time.deltaTime;
            if (timeTilMove <= 0f)
            {
                animator.SetBool("Moving", true);
                timeTilStop = moveTime;
                moving = true;
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            }
        }
    }
}
