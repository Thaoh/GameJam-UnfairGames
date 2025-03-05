using Eflatun.SceneReference;
using UnityEngine;

public class FadeSceneLoader : MonoBehaviour {
    [SerializeField] private SceneReference _sceneToLoad;
    [SerializeField] private float _fadeTime = 2f;
    [SerializeField] private AnimationCurve _fadeCurve;
    [SerializeField] private SpriteRenderer _fadeOverlay;
	[SerializeField] private float _fadeStartDistance = 0.4f;
	
	private bool _checkFadeDistance = false; 
	private float _fadeMaxByTime = -1f;
	private PlayerController _playerController;

	private void Awake() {
		_playerController = GameObject.FindAnyObjectByType<PlayerController>();
	}

	private void Update() {
		if ( !_checkFadeDistance ) {
			return;
		}
		
		if (Vector3.Distance(_playerController.transform.position, transform.position) >= _fadeStartDistance) {
			_fadeMaxByTime = Time.realtimeSinceStartup + _fadeTime;
			return;
		}
		
		if (Time.realtimeSinceStartup <= _fadeMaxByTime ) {
			float fadeStart = _fadeMaxByTime - _fadeTime;
			float fadeRatio = (Time.realtimeSinceStartup - fadeStart) / _fadeTime;

			float alpha = _fadeCurve.Evaluate(fadeRatio);
			SetAlpha(alpha);
		} else {
			SetAlpha(1f);
			NextScene();
		}
    }

	private void SetAlpha( float alpha ) {
		_fadeOverlay.color = new Color(0f, 0f, 0f, alpha);
	}

	public void StartFade() {
		_checkFadeDistance = true;
	}

	private void NextScene() {
        GameManager.LoadLevel(_sceneToLoad);
    }
}
