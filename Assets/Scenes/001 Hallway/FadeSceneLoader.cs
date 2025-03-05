using System;
using Eflatun.SceneReference;
using UnityEngine;
using Object = UnityEngine.Object;

public class FadeSceneLoader : MonoBehaviour {
    [SerializeField] private SceneReference _sceneToLoad;
    [SerializeField] private float _fadeTime = 2f;
	[SerializeField] private float _fadeStartDistance = 0.4f;
	
	private bool _checkFadeDistance = false; 
	private float _fadeMaxByTime = -1f;
	private PlayerController _playerController;

	private FadeController _fadeController;
	private void Awake() {
		_playerController = GameObject.FindAnyObjectByType<PlayerController>();
		_fadeController = Object.FindFirstObjectByType<FadeController>();
		FadeController.OnFadeComplete += SceneChange;
	}

	private void OnDestroy() {
		FadeController.OnFadeComplete -= SceneChange;
	}

	private void Update() {
		if ( !_checkFadeDistance ) {
			return;
		}
		if (Vector3.Distance(_playerController.transform.position, transform.position) >= _fadeStartDistance) {
			_fadeMaxByTime = Time.realtimeSinceStartup + _fadeTime;
			return;
		}
		_fadeController.StartFade(_fadeMaxByTime,_fadeTime);
	}

	private void SceneChange() {
		GameManager.LoadLevel(_sceneToLoad);
	}

	public void StartFade() {
		_checkFadeDistance = true;
	}

}
