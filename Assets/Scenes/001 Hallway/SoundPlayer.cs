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
	private void Awake() {
		SceneManager.sceneLoaded += ChangeAmbiance;
		
		foreach (Ambiances ambiance in _ambiances) {
			_ambiancesDictionary.Add(ambiance.Scene.Name,ambiance.Ambience);
		}
	}

	private void ChangeAmbiance(Scene arg0, LoadSceneMode arg1) {
		if (_ambiancesDictionary.TryGetValue(arg0.name, out var value)) {
			Debug.Log($"Ambiance changed {value.name}");
			_ambienceSource.clip = value;
		}
	}
}

[Serializable]
public struct Ambiances {
	public SceneReference Scene;
	public AudioClip Ambience;
}