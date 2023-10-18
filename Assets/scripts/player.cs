using static __global;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour {
	[System.NonSerialized] public combatant _combatant;
	[System.NonSerialized] public Bounds bounds;


	void Start() {
		bounds = GetComponent<SpriteRenderer>().bounds;
		_combatant = new combatant();
		object_player = gameObject;
		_player = this;
		player_offset_y = bounds.size.y / 2.00f;

		combatant.initialize(_combatant);
		_combatant._attributes = new int[3] { 10, 10, 10 };
		_combatant._health = combatant.health_max(_combatant);
		_combatant._name = "Player";
	}

	void FixedUpdate() {
		if (_combatant._health <= 0) {
			SceneManager.LoadScene("game_over");
		}
	}
}
