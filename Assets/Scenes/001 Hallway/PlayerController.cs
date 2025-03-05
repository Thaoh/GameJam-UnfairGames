using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {
	public AnimationState State {
		get { return _state; }
		set {
			_state = value;
			OnStateChanged?.Invoke(value);
		}
	}
	public Action<AnimationState> OnStateChanged { get; set; } = delegate { };
	
	[Header("References")]
	[SerializeField] private AnimationCheat _walkingAnimation;
	[SerializeField] private AnimationCheat _idleAnimation;
	[SerializeField] private AnimationCheat _flyingAnimation;
	[SerializeField] private Transform _modelTransform;
	[SerializeField] private AudioEvent _footstepEvent;
	
	[Header("Timing")]
	[SerializeField] private float _footStepMinDelay = 0.2f;
	[SerializeField] private float _footStepMaxDelay = 0.4f;
	
	private Vector3 _nextLocation;
	private Vector3 _targetPosition;
	
	private AnimationState _state = AnimationState.Idle;
	private float _nextFootStepTime;
	
	private Location[] _locations;
	private AudioSource _sfxSource;
	private void Awake() {
		OnStateChanged += HandleAnimationState;
		_locations = FindObjectsOfType<Location>();
	}

	private void Start() {
		_sfxSource = SoundPlayer.GetSFXSource;
	}

	private void OnDestroy() {
		OnStateChanged -= HandleAnimationState;
	}

	public void HandleAnimationState( AnimationState state ) {
		switch ( state ) {
			case AnimationState.Walking:
				_walkingAnimation.Animating = true;
				_idleAnimation.Animating = false;
				_flyingAnimation.Animating = false;
				_nextFootStepTime = Time.realtimeSinceStartup + Random.Range( _footStepMinDelay, _footStepMaxDelay );
				break;
			case AnimationState.Idle:
				_walkingAnimation.Animating = false;
				_idleAnimation.Animating = true;
				_flyingAnimation.Animating = false;
				break;
			case AnimationState.Flying:
				_walkingAnimation.Animating = false;
				_idleAnimation.Animating = false;
				_flyingAnimation.Animating = true;
				break;
		}
	}
	
#region Movement
	public void GoTo( Vector3 gotoPosition ) {
		Vector3 oldPosition = _nextLocation;
		
		_targetPosition = gotoPosition;

		if (transform.position == _targetPosition) {
			State = AnimationState.Idle;
			return;
		}
		
		float direction = (_targetPosition.x - transform.position.x);
		if ( direction < 0 ) {
			_modelTransform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		} else {
			_modelTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		
		float targetPositionDistance = Vector3.Distance(transform.position, _targetPosition);
		
		Location targetLocation = null;
		foreach ( var location in _locations ) {
			if ( location.transform.position == _targetPosition ) {
				targetLocation = location;

				break;
			}
		}

		if (targetLocation != null) {
			
			if ( targetLocation.LeftLocation != null) {
				if (Vector3.Distance(transform.position, targetLocation.LeftLocation.transform.position) < targetPositionDistance) {
					_nextLocation = targetLocation.LeftLocation.transform.position;
				}
			}

			if (targetLocation.RightLocation != null) {
				if (Vector3.Distance(transform.position, targetLocation.RightLocation.transform.position) < targetPositionDistance) {
					_nextLocation = targetLocation.RightLocation.transform.position;
				}
			}

			if (oldPosition == _nextLocation) {
				_nextLocation = targetLocation.transform.position;
			}
		}

		if (targetLocation == null) {
			_nextLocation = gotoPosition;
		}
		
		State = AnimationState.Walking;
	}

	private void Update() {
		if (State == AnimationState.Walking) {
			if ( Vector3.Distance( _nextLocation, transform.position ) <= 0.1f ) {
				State = AnimationState.Idle;

				if (Vector3.Distance( _targetPosition, transform.position ) >= 0.1f ) {
					GoTo( _targetPosition );
				}
				return;
			}
			
			transform.position = Vector3.MoveTowards( transform.position,_nextLocation, Time.deltaTime * 2 );
			PlayWalkingSound();
		}
	}

	private void PlayWalkingSound() {
		if (Time.realtimeSinceStartup > _nextFootStepTime) {
			_footstepEvent.Play( _sfxSource );
			_nextFootStepTime = Time.realtimeSinceStartup + Random.Range( _footStepMinDelay, _footStepMaxDelay );
		}
	}

#endregion
}


public enum AnimationState {
	Idle,
	Walking,
	Flying,
	None
}