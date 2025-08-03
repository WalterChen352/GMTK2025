using UnityEngine;

public class Snail : MonoBehaviour, IInteractable
{
    [SerializeField] float minStartWaitTime;
    [SerializeField] float maxStartWaitTime;
    [SerializeField] float minWaitTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] float moveTime;
    [SerializeField] float moveSpeed;

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
            speechBubble.SetWords("Hello, I'm a snail!");
            speechBubble.Show();
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
        }
        else
        {
            if (!moving) {
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
