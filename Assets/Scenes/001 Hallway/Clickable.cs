using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Clickable : MonoBehaviour {
	private Camera _mainCamera;

	private Collider2D _collider2D;
	private Collider _collider;

	protected virtual void Awake() {
		SceneManager.sceneLoaded += Initialize;
	}

	private void OnDestroy() {
		SceneManager.sceneLoaded -= Initialize;
	}

	private void Initialize(Scene arg0, LoadSceneMode arg1) {
		_collider2D = GetComponent<Collider2D>();
		_collider = GetComponent<Collider>();
		_mainCamera = Camera.main;
	}

	protected virtual void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (_mainCamera == null) {
				Debug.LogError($"mainCamera is missing!");
			}

			if (_collider2D != null) {
				Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);			
				var hit = Physics2D.Raycast(mousePosition, _mainCamera.transform.position - Input.mousePosition, Mathf.Infinity);

				if (hit.collider != null && hit.collider == _collider2D) {
					PerformAction(hit.collider);
				}	
			} else if (_collider != null) {
				Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
				Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit, 200f) && hit.collider == _collider) {
					PerformAction(hit.collider);
				}	
			}
		}
	}

	protected virtual void PerformAction(Collider2D hitCollider) {
		
	}
	
	protected virtual void PerformAction(Collider hitCollider) {
		
	}
}