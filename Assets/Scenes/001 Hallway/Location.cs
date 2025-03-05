using System;
using UnityEngine;

public class Location : Clickable {
	private PlayerController _playerController;
	public Action OnClick;
	private Transform _moveTarget = null;


	protected override void Awake() {
		base.Awake();
		_playerController = GameObject.FindAnyObjectByType<PlayerController>();
	
		if (transform.childCount > 0) {
			foreach (Transform childTransform in transform) {
				if (childTransform.CompareTag("MoveTarget")) {
					Debug.LogWarning($"Location MoveTarget {childTransform.name}");
					_moveTarget = childTransform;
					break;
				}
			}
		}
	}

	protected override void PerformAction(Collider2D hitCollider) {
		if (hitCollider != null) {
			_playerController.SetDestination(this);
		}
	}

	protected override void PerformAction(Collider hitCollider) {
		if (hitCollider != null) {
			Debug.Log(hitCollider.gameObject.name);
			_playerController.SetDestination(this);
		}
	}

	public Transform MoveTarget {
		get { return _moveTarget; }
	}
}