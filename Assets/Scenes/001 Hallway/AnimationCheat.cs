using UnityEngine;
using UnityEngine.UI;

public class AnimationCheat : MonoBehaviour {
	// This string is just here for us to easily be able to tell what the animation is doing.
	[SerializeField] private string _animationName;
	
	public bool Animating = false;

	[Header("Timing")]
	[SerializeField] private float _framesPerSecond;
	[Header("References")]
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private Image _image;
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private float _nextFrameTime;
	private float _frameTime;

	private int _renderIndex;

	private int _renderFrame {
		get => _renderIndex;
		set {
			var newIndex = _renderIndex + value;
			_renderIndex = newIndex >= _sprites.Length ? 0 : newIndex;
		}
	}

	private void Awake() {
		_nextFrameTime = Time.realtimeSinceStartup + 1 / _framesPerSecond;
	}


	private void Update() {
		if (Animating && Time.realtimeSinceStartup >= _nextFrameTime) {
			if (_image != null) {
				_image.sprite = _sprites[_renderFrame++];
			}

			if (_spriteRenderer != null) {
				_spriteRenderer.sprite = _sprites[_renderFrame++];
			}

			_nextFrameTime = Time.realtimeSinceStartup + 1 / _framesPerSecond;
		}
	}
}