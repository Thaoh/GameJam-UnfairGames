
using Eflatun.SceneReference;
using UnityEngine;

public class EstelleBroom : ClickableUIMessage {
	[SerializeField] private AudioEvent _startBroomEvent;
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
		
		if (GameManager.EnergySourcePickedUp && Time.realtimeSinceStartup >= _levelChangedByTime) {
			GameManager.LoadLevel(_nextLevel);
		}
	}

	protected override void PerformAction(Collider2D hitCollider) {
		if (GameManager.EnergySourcePickedUp) {
			_startBroomEvent.Play(SoundPlayer.GetSFXSource);
			_levelChangedByTime = Time.realtimeSinceStartup + _levelChangeDelay;
			_fadeController.StartFade(_levelChangedByTime, _levelChangeDelay);
		} else {
			base.PerformAction(hitCollider);	
		}
	}
}
