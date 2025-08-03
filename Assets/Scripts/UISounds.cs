using UnityEngine;

public class UISounds : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUISound()
    {
        audioSource.Play();
    }
}
