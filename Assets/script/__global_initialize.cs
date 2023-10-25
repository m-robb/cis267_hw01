using static __global;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * master is the game master. It writes all important values.
 * Communication occurs through the static class 
 */
public class __global_initialize : MonoBehaviour {
	[System.NonSerialized] public float score_time_previous;


	void Awake() {
		score_time_previous = Time.time;

		/* Initialize  */
		game_speed = 1.00f;
		game_scroll_speed = 4.00f;
		game_width = 11.00f;
		game_score_time = 0;

		movement_drag = 0.50f * Vector3.one;
		movement_friction = 3.33f
				* new Vector3(1.00f, 0.00f, 1.00f);
		movement_gravity = -9.81f * Vector3.up;

		player_floor_height = 0.00f;
		player_movement_acceleration = 16.00f;
		player_movement_jump_strength = 4.00f; /* 3.00f is best. */

		render_max_z = -2.00f;

		object_combat_item_weapon = combat_item_weapon_read();
		object_combat_item_armor = combat_item_armor_read();
		/* object_combat_enemy = combat_enemy_read(); */

		combat_experience_per_level = 10;
		combat_roll_minimum = 1;
		combat_roll_maximum = 100;
		combat_base_hit_chance = (combat_roll_minimum
				+ combat_roll_maximum) / 2;
		combat_attribute_strength_health = 5;
		combat_attribute_agility_dodge = 1;

		Destroy(gameObject);
	}
}
