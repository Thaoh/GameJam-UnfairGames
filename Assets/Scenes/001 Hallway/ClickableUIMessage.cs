using UnityEngine;
using UnityEngine.Serialization;

public class ClickableUIMessage : Clickable {
	[SerializeField] private string _prefix;
	[SerializeField] string[] _messages;
	[SerializeField] bool _repeating;
	[SerializeField] private bool _shuffle;
	private int _currentMessageIndex;

	private int _messageIndex {
		get { return _currentMessageIndex; }
		set {
			if (_repeating) {
				_currentMessageIndex = value >= _messages.Length ? 0 : value;
			} else {
				_currentMessageIndex = Mathf.Clamp(value, 0, _messages.Length - 1);
			}
		}
	}

	protected override void PerformAction(Collider2D hitCollider) {
		if (_shuffle) {
			_messageIndex = Random.Range(0, _messages.Length);
		}

		if (_messageIndex < _messages.Length) {
			UIManager.SetDialogue(_messages[_messageIndex++], _prefix);
		}
	}
}