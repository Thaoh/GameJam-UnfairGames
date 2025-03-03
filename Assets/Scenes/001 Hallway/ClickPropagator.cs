using System;
using UnityEngine;
using UnityEngine.Events;
using Eflatun.SceneReference;

public class ClickPropagator : MonoBehaviour
{
	[SerializeField] private UnityEvent _clickAction;
	
	[SerializeField] private SceneReference _sceneToLoad;

	private void OnCollisionEnter(Collision other) {
		
	}
}
