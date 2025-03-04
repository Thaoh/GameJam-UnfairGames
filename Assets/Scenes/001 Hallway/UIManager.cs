using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour {
	public static UIManager Instance;
	[SerializeField] private UIDocument _mainUIDocument;
	[SerializeField] private float _letterDelay = 0.2f;
	
	private VisualElement _root;
	private VisualElement _container;
	private Label _content;

	private string _nextMessage;
	private bool _rendering;
	private float _nextLetterTime;
	private string _prefix = "";

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
		_root = _mainUIDocument.rootVisualElement;
		_container = _root.Q<VisualElement>("Dialogue");
		_content = _root.Q<Label>("DialogueLabel");
		
		_nextLetterTime = Time.realtimeSinceStartup + _letterDelay;
		SetDialogue();
	}

	private void Update() {
		if (_rendering == false || Time.realtimeSinceStartup <= _nextLetterTime || _nextMessage == "" || _nextMessage == null) {
			return;
		}

		if (_content.text.Length == 0) {
			_content.text = _prefix;
		}
		
		_content.text += _nextMessage.Substring(0, 1);
		_nextMessage = _nextMessage.Substring(1);

		_nextLetterTime = Time.realtimeSinceStartup + _letterDelay;
		
		if (_nextMessage.Length == 0) {
			_rendering = false;
		}
	}

	public static void SetDialogue(string dialogue = null, string prefix = null) {
		if (dialogue == null) {
			Instance._container.visible = false;
			Instance._content.text = "";
			return;
		}

		Instance._prefix = prefix;
		Instance._container.visible = true;
		Instance._nextMessage = dialogue;
		Instance._content.text = "";
		Instance._rendering = true;
	}
}
