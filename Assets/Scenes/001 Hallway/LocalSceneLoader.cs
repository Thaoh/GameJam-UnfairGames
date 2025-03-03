using System;
using UnityEngine;
using Eflatun.SceneReference;

public class LocalSceneLoader : MonoBehaviour {
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private SceneReference _sceneToLoad;
	private Camera _mainCamera;

	private Collider2D _collider2D;

	private void Awake() {
		_collider2D = GetComponent<Collider2D>();
		_mainCamera = Camera.main;
	}

	private void Update() {
		if (_sceneToLoad.Name == null) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			if (_mainCamera  == null) {
				Debug.LogError($"mainCamera is missing!");
			}
			
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if (hit.collider != null && hit.collider) {
				LoadLevel();
			}
		}
	}

	public void LoadLevel() {
		GameManager.LoadLevel(_sceneToLoad);
	}
}