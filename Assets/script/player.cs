using static __global;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour {
	[System.NonSerialized] public combatant _combatant;
	[System.NonSerialized] public Transform hitbox;
	[System.NonSerialized] public int level_former;


	void Start() {
		hitbox = transform.Find("hitbox");
		_combatant = new combatant();
		object_player = gameObject;
		_player = this;
		player_offset_y = hitbox.lossyScale.y / 2.00f;

		combatant.initialize(_combatant);
		_combatant._id = "player";
		_combatant._name = "Player";
		_combatant._attributes = new int[3] { 10, 10, 10 };
		_combatant._health = combatant.health_max(_combatant);

		level_former = combat_level(_combatant._experience);
	}

	void FixedUpdate() {
		string log;
		int level;
		int attribute_gained;

		level = combat_level(_combatant._experience);

		if (_combatant._health <= 0) {
			SceneManager.LoadScene("game_over");
		}

		while (level > level_former) {
			++level_former;

			attribute_gained = combatant.level_up(_combatant);

			log = "";
			log += _combatant._name + " has leveled up!\n";
			log += "Gained: +1 " + attribute_text(attribute_gained)
					+ ".\n";
			log += "Fully healed to " + _combatant._health + "HP.";

			Debug.Log(log);
		}
	}
}
