using static __global;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour {
	/*
	 * Resets all values and enters the game scene.
	 */
	public void button_play() {
		GameObject global_initialize;

		global_initialize = new GameObject();
		global_initialize.AddComponent<__global_initialize>();
		SceneManager.LoadScene("game");
	}

	/*
	 * Closes the application.
	 */
	public void button_exit() {
		Application.Quit();
	}
}
