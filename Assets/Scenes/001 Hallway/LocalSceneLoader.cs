using UnityEngine;
using Eflatun.SceneReference;

public class LocalSceneLoader : Clickable {
	[SerializeField] private SceneReference _sceneToLoad;
	
	protected override void PerformAction(Collider2D hitCollider) {
		GameManager.LoadLevel(_sceneToLoad);
	}
}