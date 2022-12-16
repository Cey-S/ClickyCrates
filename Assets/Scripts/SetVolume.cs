using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource bgMusic;

    private float volume;

    private void Start()
    {
        bgMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);

        bgMusic.volume = volumeSlider.value = volume;
    }

    public void adjustVolume()
    {
        bgMusic.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
