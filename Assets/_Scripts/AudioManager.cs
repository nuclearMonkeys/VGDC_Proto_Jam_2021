using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

	public static AudioManager instance;

	private float fadeDuration = 0.6f;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;

		}
	}
	private void Start()
	{
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		LevelSpecificPlay(sceneIndex);
	}
	private void LevelSpecificPlay(int sceneIndex)
	{
		switch(sceneIndex)
		{
			case 1:
				Play("level-1");
				break;
			case 2:
				Play("level-2");
				break;
			case 3:
				Play("level-3");
				break;
			case 4:
				Play("level-4");
				break;
			case 5:
				Play("level-5");
				break;
		}
	}
	
    
    public void SetVolume (float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }

	public void Play(string name)
	{

		
		Sound s = Array.Find(sounds, sound => sound.name == name);


		if (s != null)
			s.source.Play();
		else
			Debug.LogWarning("audioclip " + name + " is NULL");

		Debug.Log("playing " + name + "...");
	}
	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s != null)
			s.source.Stop();
		else
			Debug.LogWarning("audioclip " + name + " is NULL");
	}
	public void OnTransitionScene(int currentSceneIndex,int nextSceneIndex)
	{
		switch (currentSceneIndex)
		{
			case 1:
				StartCoroutine(FadeOut("level-1", fadeDuration));
				StartCoroutine(FadeIn("level-2", fadeDuration));
				break;
			case 2:
				StartCoroutine(FadeOut("level-2", fadeDuration));
				StartCoroutine(FadeIn("level-3", fadeDuration));
				break;
			case 3:
				StartCoroutine(FadeOut("level-3", fadeDuration));
				StartCoroutine(FadeIn("level-4", fadeDuration));
				break;
			case 4:
				StartCoroutine(FadeOut("level-4", fadeDuration));
				StartCoroutine(FadeIn("level-5", fadeDuration));
				break;
			
		} 
		
	}
	public void FadeIn(string name)
	{
		StartCoroutine(FadeIn(name, fadeDuration));
	}
	public void FadeOut(string name)
	{
		StartCoroutine(FadeOut(name, fadeDuration));
	}
	
	public IEnumerator FadeOut(string name, float FadeTime)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if(s != null)
		{
			float startVolume = s.source.volume;
			while(s.source.volume > 0)
			{
				s.source.volume -= startVolume * Time.deltaTime / FadeTime;
				yield return null;
			}
			s.source.Stop();
		}
	}

	public IEnumerator FadeIn(string name, float FadeTime)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s != null)
		{
			s.source.Play();
			s.source.volume = 0f;

			while(s.source.volume < 1)
			{
				s.source.volume += Time.deltaTime / FadeTime;
				yield return null;
			}
		}

	}


}
