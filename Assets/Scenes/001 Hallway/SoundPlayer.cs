using System;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundPlayer : MonoBehaviour {
	[SerializeField] private Ambiances[] _ambiances;

	[SerializeField] private AudioSource _sfxSource;
	[SerializeField] private AudioSource _ambienceSource;

	private Dictionary<string, AudioClip> _ambiancesDictionary = new();

	public static SoundPlayer Instance;
	
	private void Awake() {
		SceneManager.sceneLoaded += ChangeAmbiance;
		
		foreach (Ambiances ambiance in _ambiances) {
			_ambiancesDictionary.Add(ambiance.Scene.Name,ambiance.Ambience);
		}

		if ( Instance == null ) {
			Instance = this;
		} else {
			Destroy( this.gameObject );
		}
	}

	private void OnDestroy() {
		SceneManager.sceneLoaded -= ChangeAmbiance;
	}

	private void ChangeAmbiance(Scene arg0, LoadSceneMode arg1) {
		if (_ambiancesDictionary.TryGetValue(arg0.name, out var value)) {
			_ambienceSource.clip = value;
			_ambienceSource.Play();
		}
	}

	public static AudioSource GetSFXSource {
		get {
			return Instance._sfxSource;
		}
	}
}

[Serializable]
public struct Ambiances {
	public SceneReference Scene;
	public AudioClip Ambience;
}