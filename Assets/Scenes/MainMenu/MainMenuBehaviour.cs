using UnityEngine;
using UnityEngine.UIElements;
using Eflatun.SceneReference;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {
	[SerializeField] private UIDocument _mainMenu;
	[SerializeField] private SceneReference _playGameScene;
	private VisualElement _root = null;
	private VisualElement _playButton = null;
	private VisualElement _exitButton = null;


	private void Awake() {
		_root = _mainMenu.rootVisualElement;
		_playButton = _root.Q<VisualElement>("Play");
		_exitButton = _root.Q<VisualElement>("Exit");

		_playButton.RegisterCallback<ClickEvent>(Play);
		_exitButton.RegisterCallback<ClickEvent>(Quit);
	}

	private void OnDestroy() {
		_playButton.UnregisterCallback<ClickEvent>(Play);
		_exitButton.UnregisterCallback<ClickEvent>(Quit);
	}

	private void Quit(ClickEvent evt) {
		Application.Quit();
	}

	private void Play(ClickEvent e) {
		SceneManager.LoadScene(_playGameScene.Name);
	}
}