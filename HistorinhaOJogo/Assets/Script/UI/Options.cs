using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    // private const string MasterVolume = "MasterVolume";
    // private const string MusicVolume = "MusicVolume";
    // private const string SFXVolume = "SFXVolume";
    // [SerializeField] private AudioMixer audioMixer;

     [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSFX;

    float volumeMaster, volumeMusic, volumeSFX;


    public delegate void OptionClose();
    public static event OptionClose OnOptionClose;

    public void VolumeMaster(float volume)
    {
        volumeMaster = volume;
        AudioListener.volume = volumeMaster;
    }
        public void VolumeMusic(float volume)
    {
        volumeMusic = volume;
        GameObject[] Musics = GameObject.FindGameObjectsWithTag("music");
        for(int i = 0; i< Musics.Length; i++)
        {
            Musics[i].GetComponent<AudioSource>().volume = volumeMusic;
        }
    }
        public void VolumeSFX(float volume)
    {
        volumeSFX = volume;
        GameObject[] Sfxs = GameObject.FindGameObjectsWithTag("sfx");
        for(int i = 0; i<Sfxs.Length; i++)
        {
            Sfxs[i].GetComponent<AudioSource>().volume = volumeSFX;
        }
    }

    // //Scrool precisa de 0.0001 atÃ© 1
    // public void SetMasterVolume() => SetVolume(sliderMaster, MasterVolume);
    // public void SetSFXVolume() => SetVolume(sliderSFX, SFXVolume);
    // public void SetMusicVolume() => SetVolume(sliderMusica, MusicVolume);

    // private void SetVolume(Slider slider, string source)
    // {
    //     var level = slider.value;
    //     Debug.Log($"Set {source}: {level}");
    //     audioMixer.SetFloat(source, math.log10(level) * 20f);
    //     PlayerPrefs.SetFloat(source, level);
    // }

    public void Close()
    {
        PlayerPrefs.Save();
        gameObject.SetActive(false);
        OnOptionClose?.Invoke();
    }

     public void ResetOptions()
    {
        sliderMaster.value = 1f;
        sliderMusica.value = 1f;
        sliderSFX.value = 1f;
    //     sliderMusica.value = 1f;
    //     sliderSFX.value = 1f;
        //VolumeMaster(volume);
    //     SetMusicVolume();
    //     SetSFXVolume();
    }

     internal void Open()
     {
        //  sliderMaster.value = PlayerPrefs.GetFloat(MasterVolume, 1);
        //  sliderMusica.value = PlayerPrefs.GetFloat(MusicVolume, 1);
        //  sliderSFX.value = PlayerPrefs.GetFloat(SFXVolume, 1);
         gameObject.SetActive(true);
    }
}
