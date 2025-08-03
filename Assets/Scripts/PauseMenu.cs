using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    static PauseMenu instance;

    Canvas canvas;

    [SerializeField] AudioMixerGroup masterGroup;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup sfxGroup;
    [SerializeField] AudioMixerGroup uiGroup;

    private AudioSource audioSource;

    public static PauseMenu GetInstance()
    {
        return instance;
    }

    public void PlayUISound()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        PlayUISound();
        Time.timeScale = 0f;
        canvas.enabled = true;
    }

    public void Unpause()
    {
        PlayUISound();
        Time.timeScale = 1f;
        canvas.enabled = false;
    }

    public void SetMasterVolume(float volume)
    {
        masterGroup.audioMixer.SetFloat("MasterVolume", VolumeToDecibel(volume));
    }

    public void SetMusicVolume(float volume)
    {
        musicGroup.audioMixer.SetFloat("MusicVolume", VolumeToDecibel(volume));
    }

    public void SetSFXVolume(float volume)
    {
        sfxGroup.audioMixer.SetFloat("SFXVolume", VolumeToDecibel(volume));
    }

    public void SetUIVolume(float volume)
    {
        uiGroup.audioMixer.SetFloat("UIVolume", VolumeToDecibel(volume));
    }

    public void Quit()
    {
        PlayUISound();
        Application.Quit();
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    float VolumeToDecibel(float volume)
    {
        const float minDb = -80.0f;
        if (volume <= 0.0001f) // avoid log(0)
            return minDb;

        float db = 20.0f * (float)Math.Log10(volume);
        return Math.Max(db, minDb);
    }
}
