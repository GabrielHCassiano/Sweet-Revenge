using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Config : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider SliderMaster;
    public Slider SliderMusic;
    public Slider SliderEffects;

    // Start is called before the first frame update
    void Start()
    {
        SliderMaster.value = PlayerPrefs.GetFloat("Master", 0.5f);
        SliderMusic.value = PlayerPrefs.GetFloat("Music", 0.5f);
        SliderEffects.value = PlayerPrefs.GetFloat("Effects", 0.5f);
    }

    // Update is called once per frame
    public void SetMaster(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);  
    }
    public void SetMusic(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetEffects(float volume)
    {
        PlayerPrefs.SetFloat("Effects", volume);
        audioMixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
    }
}
