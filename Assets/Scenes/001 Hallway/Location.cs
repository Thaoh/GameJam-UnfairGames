using UnityEngine;

public class Location : Clickable {
	[SerializeField] private Location _leftLocation = null;
	[SerializeField] private Location _rightLocation = null;

	public Location LeftLocation { get { return _leftLocation; } }
	public Location RightLocation { get { return _rightLocation; } }
	
	private PlayerController _playerController;
	
	protected override void Awake() {
		base.Awake();
		_playerController = GameObject.FindAnyObjectByType<PlayerController>(  );
	}

	protected override void PerformAction( Collider2D hitCollider ) {
		if (hitCollider != null) {
			Debug.Log( $"Perform action: {hitCollider.name}" );
			_playerController.GoTo(hitCollider.transform.position);
		}
	}
}
