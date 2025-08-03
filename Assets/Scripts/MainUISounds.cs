using UnityEngine;

public class MainUISounds : MonoBehaviour
{
    [SerializeField] AudioClip logStackSound;
    [SerializeField] AudioClip munchSound;

    AudioSource audioSource;

    public void PlayLogStackSound()
    {
        audioSource.PlayOneShot(logStackSound);
    }

    public void PlayMunchSound()
    {
        audioSource.PlayOneShot(munchSound);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
