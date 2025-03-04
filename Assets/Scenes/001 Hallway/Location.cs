using UnityEngine;

public class Location : Clickable {
	private PlayerController _playerController;
	
	protected override void Awake() {
		base.Awake();
		_playerController = GameObject.FindAnyObjectByType<PlayerController>(  );
	}

	protected override void PerformAction( Collider2D hitCollider ) {
		if (hitCollider != null) {
			_playerController.GoTo(this);
		}
	}
}
