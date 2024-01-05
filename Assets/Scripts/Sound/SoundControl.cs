using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {
    private AudioSource m_AudioSource;
    private GameObject soundButton;
    private GameObject soundIconOn;
    private GameObject soundIconOff;
    private int isMusicOn;
	// Use this for initialization
	void Start () {
        isMusicOn = PlayerPrefs.GetInt("Music",1);
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        soundButton = GameObject.Find("soundicon");
        soundIconOn = GameObject.Find("AudioOn");
        soundIconOff = soundIconOn = GameObject.Find("AudioOff");
        SetMusic(isMusicOn);
        UIEventListener.Get(soundButton).onClick = SwitchSoundOnOff;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SwitchSoundOnOff(GameObject go)
    {
        if (isMusicOn == 1)
        {
            m_AudioSource.Pause();
            soundIconOn.SetActive(false);
            soundIconOff.SetActive(true);
            isMusicOn = 0;
            PlayerPrefebSetSound(isMusicOn);


        }
        else
        {
            m_AudioSource.Play();
            soundIconOn.SetActive(true);
            soundIconOff.SetActive(false);
            isMusicOn = 1;
            PlayerPrefebSetSound(isMusicOn);

        }
    }

    public void PlayerPrefebSetSound(int music)
    {
        PlayerPrefs.SetInt("Music", music);
    }

    private void SetMusic(int music)
    {
        if (music == 0)
        {
            m_AudioSource.Pause();
            soundIconOn.SetActive(false);
            soundIconOff.SetActive(true);
        }
        else
        {
            m_AudioSource.Play();
            soundIconOn.SetActive(true);
            soundIconOff.SetActive(false);
        }
    }
}
