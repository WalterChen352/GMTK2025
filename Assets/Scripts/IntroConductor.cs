using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroConductor : MonoBehaviour
{
    [SerializeField] float boatDrivingTime = 2f;
    [SerializeField] Sprite scene2;
    [SerializeField] AudioClip damCrash;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        Image image = GetComponentInChildren<Image>();
        yield return new WaitForSeconds(1f);
        audioSource.Play();
        yield return new WaitForSeconds(5f);
        image.sprite = scene2;
        audioSource.clip = damCrash;
        audioSource.Play();
        yield return new WaitForSeconds(boatDrivingTime);
        image.color = Color.black;
        yield return new WaitForSeconds(11f - boatDrivingTime);
        SceneManager.LoadScene("MainMenu");
    }
}
