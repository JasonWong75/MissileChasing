using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour {

    private AudioSource m_AudioSource;
	// Use this for initialization
	void Start () {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        Debug.Log(PlayerPrefs.GetInt("Music"));
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Pause();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
