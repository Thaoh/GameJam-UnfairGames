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

	[SerializeField] private bool _repeating;
	[SerializeField] private bool _pingpong;
	
	private float _nextFrameTime;
	private float _frameTime;

	private int _renderIndex;
	
	private bool _isReturning;

	private int _renderFrame {
		get => _renderIndex;
		set {
			if (_pingpong) {
				_renderIndex = value;
				
				if (_isReturning) {
					if (_renderIndex < 0) {
						_renderIndex = 0;
						_isReturning = false;
					}
				}

				if (_renderIndex >= _sprites.Length) {
					_renderIndex = _renderIndex - 1;
					_isReturning = true;
				}
			} else if ( _repeating ) {
				_renderIndex = value >= _sprites.Length ? 0 : value;
			} 
		}
	}

	private void Awake() {
		_nextFrameTime = Time.realtimeSinceStartup + 1 / _framesPerSecond;
	}


	private void Update() {
		if (Animating && Time.realtimeSinceStartup >= _nextFrameTime) {
			if ( _isReturning ) {
				_renderFrame--;
			} else {
				_renderFrame++;
			}
			
			if (_image != null) {
				_image.sprite = _sprites[_renderFrame];
			}

			if (_spriteRenderer != null) {
				_spriteRenderer.sprite = _sprites[_renderFrame];
			}

			_nextFrameTime = Time.realtimeSinceStartup + 1 / _framesPerSecond;
		}
	}
}