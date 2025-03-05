
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.Serialization;

public class EstelleBroom : ClickableUIMessage {
	[FormerlySerializedAs("_startBroomEvent")] [SerializeField] private AudioEvent _configuredSoundEvent;
	[SerializeField] private SceneReference _nextLevel;
	[SerializeField] private float _levelChangeDelay = 5f;

	private FadeController _fadeController;
	protected override void Awake() {
		base.Awake();
		_fadeController = Object.FindFirstObjectByType<FadeController>();
	}

	private float _levelChangedByTime = Mathf.Infinity;

	protected override void Update() {
		base.Update();
		
		if (Condition() && Time.realtimeSinceStartup >= _levelChangedByTime) {
			GameManager.LoadLevel(_nextLevel);
		}
	}

	protected virtual bool Condition() {
		return GameManager.EnergySourcePickedUp;
	}

	protected override void PerformAction(Collider2D hitCollider) {
		if (Condition()) {
			PlayConfiguredSoundEvent();
			_levelChangedByTime = Time.realtimeSinceStartup + _levelChangeDelay;
			_fadeController.StartFade(_levelChangedByTime, _levelChangeDelay);
		} else {
			base.PerformAction(hitCollider);	
		}
	}
	protected override void PerformAction(Collider hitCollider) {
		if (Condition()) {
			PlayConfiguredSoundEvent();
			_levelChangedByTime = Time.realtimeSinceStartup + _levelChangeDelay;
			_fadeController.StartFade(_levelChangedByTime, _levelChangeDelay);
		} else {
			base.PerformAction(hitCollider);	
		}
	}

	protected virtual void PlayConfiguredSoundEvent() {
		_configuredSoundEvent.Play(SoundPlayer.GetSFXSource);
	}
}
