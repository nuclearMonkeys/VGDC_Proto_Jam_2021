using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

	public static AudioManager instance;

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
		Play("Menu");
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
	

}
