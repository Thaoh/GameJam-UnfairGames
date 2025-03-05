using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {
	public AnimationState State {
		get { return _state; }
		set {
			_state = value;
			OnStateChanged?.Invoke( value );
		}
	}

	public Action<AnimationState> OnStateChanged { get; set; } = delegate { };

	[Header( "References" )]
	[SerializeField] private AnimationCheat _walkingAnimation;

	[SerializeField] private AnimationCheat _idleAnimation;
	[SerializeField] private AnimationCheat _flyingAnimation;
	[SerializeField] private Transform _modelTransform;
	[SerializeField] private AudioEvent _footstepEvent;

	[Header( "Timing" )]
	[SerializeField] private float _footStepMinDelay = 0.2f;

	[SerializeField] private float _footStepMaxDelay = 0.4f;

	private Vector3 _nextLocation;
	private Vector3 _targetPosition;

	private AnimationState _state = AnimationState.Idle;
	private float _nextFootStepTime;

	private Location[] _locations;
	private AudioSource _sfxSource;

	private Location _targetLocation;
	private Vector3 _startPosition;

	private Dictionary<Vector3, Location> _locationsDictionary = new();

	private void Awake() {
		OnStateChanged += HandleAnimationState;

		SceneManager.sceneLoaded += Initialize;
	}

	private void Initialize(Scene arg0, LoadSceneMode arg1) {
		_locations = Object.FindObjectsByType<Location>( FindObjectsSortMode.None );
		_locationsDictionary.Clear();
			
		foreach ( Location location in _locations ) {
			if (location.MoveTarget == null) {
				_locationsDictionary.Add( location.transform.position, location );
			} else {
				_locationsDictionary.Add( location.MoveTarget.TransformPoint(location.MoveTarget.transform.position), location );
			}
		}
		
		_sfxSource = SoundPlayer.GetSFXSource;
	}

	private void OnDestroy() {
		OnStateChanged -= HandleAnimationState;
		SceneManager.sceneLoaded -= Initialize;
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

	public void SetDestination( Location gotoLocation ) {
		_startPosition = transform.position;
		
		_targetLocation = gotoLocation;
		if (_targetLocation != null && _targetLocation.MoveTarget != null) {
			_targetPosition = _targetLocation.MoveTarget.TransformPoint( _targetLocation.MoveTarget.position );
			Debug.LogWarning($"PlayerController: MoveTarget Exists: {_targetLocation.MoveTarget.name}");
		} else {
			_targetPosition = _targetLocation.transform.position;
		}

		if ( Vector3.Distance( transform.position, _targetPosition ) <= 0.1f ) {
			State = AnimationState.Idle;

			return;
		}

		FlipSpriteBasedOnDirection();
		FindClosestLocationWaypoint();
		State = AnimationState.Walking;
	}

	private void FindClosestLocationWaypoint() {
		float closestDistance = Mathf.Infinity;
		_nextLocation = Vector3.zero; // Reset target location

		foreach ( var location in _locations ) {
			Vector3 locationPosition = location.transform.position;
			Vector3 toLocation = locationPosition - transform.position;
			Vector3 directionToTarget = (_targetPosition - transform.position).normalized;
			float distanceToLocation = toLocation.magnitude;
			
			// Make sure the closest doesn't become the loc you're standing on &
			//		Check that the target is in the general direction of the target &
			//		Check if this location is closer than the current closest
			if ( distanceToLocation > 1f && Vector3.Dot(directionToTarget, toLocation.normalized) > 0.5f && distanceToLocation < closestDistance ) {
				closestDistance = distanceToLocation;
				if (location != null && location.MoveTarget != null) {
					_nextLocation = location.MoveTarget.TransformPoint(location.MoveTarget.position);
				} else {
					_nextLocation = locationPosition;
				}
			}
		}

		if ( _nextLocation != Vector3.zero ) {
			Debug.Log( $"Navigating to closest location at {_nextLocation}" );
		} else {
			Debug.Log( "No valid location found in the direction of the target." );
		}
	}

	private void FlipSpriteBasedOnDirection() {
		float direction = ( _targetPosition.x - transform.position.x );

		if ( direction < 0 ) {
			_modelTransform.localScale = new Vector3( -1.0f, 1.0f, 1.0f );
		} else {
			_modelTransform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		}
	}

	private void Update() {
		if ( State == AnimationState.Walking ) {
			// Move towards the next location
			transform.position = Vector3.MoveTowards(transform.position, _nextLocation, Time.deltaTime * 2);

			// Check if the character has reached the next location
			if ( Vector3.Distance( _nextLocation, transform.position ) <= 0.2f ) {
				if (_targetPosition != _nextLocation && Vector3.Distance( _targetPosition, transform.position ) > 0.2f) {
					SetDestination(  _locationsDictionary[_targetPosition]); // Move to the target position
				} else {
					transform.position = _nextLocation;
					State = AnimationState.Idle; // Set to idle if we reached the target
				}
				
				// Handle door animation if applicable
				if ( _targetLocation != null && _targetLocation.TryGetComponent( out FadeSceneLoader doorAnimation ) ) {
					doorAnimation.StartFade();
				}
			}

			PlayWalkingSound();
		}
	}

	private void PlayWalkingSound() {
		if ( Time.realtimeSinceStartup > _nextFootStepTime ) {
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