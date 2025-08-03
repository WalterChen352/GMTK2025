using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public Transform attachTransform;
    public Vector3 offset;

    [SerializeField] float easeSpeed;
    [SerializeField] TextMeshProUGUI wordbox;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.position = attachTransform.position;
        gameObject.SetActive(true);
    }

    public void SetWords(string words)
    {
        wordbox.text = words;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = attachTransform.position + offset;
        // Exponential easing
        transform.position += (targetPosition - transform.position) * (1.0f - Mathf.Exp(-easeSpeed * Time.deltaTime));
    }
}
