using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	public static bool EnergySourcePickedUp = false;
	
	public static void LoadLevel( SceneReference scene ) {
		SceneManager.LoadScene(scene.Name);
	}
}
