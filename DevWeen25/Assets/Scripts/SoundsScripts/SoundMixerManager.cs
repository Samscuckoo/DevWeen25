using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] string exposedParam = "MasterVolume";   // Nome do parâmetro no AudioMixer
    public Slider slider;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        if (audioMixer.GetFloat(exposedParam, out float valueInDb))
        {
            slider.value = DbToLinear(valueInDb);
        }
    }

    


    public void setMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20);
    }

    public void setSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20);
    }

    public void setMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20);
    }

    public void setAmbienceVolume(float level)
    {
        audioMixer.SetFloat("AmbienceVolume", Mathf.Log10(level) * 20);
    }



    private float DbToLinear(float db)
    {
        return Mathf.Pow(10f, db / 20f);
    }
}
