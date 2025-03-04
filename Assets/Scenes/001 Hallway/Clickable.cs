using UnityEngine;

public abstract class Clickable : MonoBehaviour {
	private Camera _mainCamera;

	private Collider2D _collider2D;

	protected virtual void Awake() {
		_collider2D = GetComponent<Collider2D>();
		_mainCamera = Camera.main;
	}

	protected virtual void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (_mainCamera == null) {
				Debug.LogError($"mainCamera is missing!");
			}

			Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);			
			var hit = Physics2D.Raycast(mousePosition, _mainCamera.transform.position - Input.mousePosition, Mathf.Infinity);

			if (hit.collider != null && hit.collider == _collider2D) {
				PerformAction(hit.collider);
			}
		}
	}

	protected virtual void PerformAction(Collider2D hitCollider) {
		
	}
}