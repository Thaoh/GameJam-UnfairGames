using UnityEngine;

public class Location : Clickable {
	private PlayerController _playerController;
	
	protected override void Awake() {
		base.Awake();
		_playerController = GameObject.FindAnyObjectByType<PlayerController>(  );
	}

	protected override void PerformAction( Collider2D hitCollider ) {
		if (hitCollider != null) {
			_playerController.SetDestination(this);
		}
	}
	protected override void PerformAction( Collider hitCollider ) {
		if (hitCollider != null) {
			_playerController.SetDestination(this);
		}
	}
}
