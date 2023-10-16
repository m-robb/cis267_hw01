using UnityEngine;

/*
 * Global values.
 * This is the method through which the game master interacts with the world.
 * Be careful about writing values here. General use should just be reading.
 */
public static class global {
	/* GAME */
	public static float game_speed;
	public static float game_scroll_speed;
	public static float game_width;


	/* MOVEMENT */
	public static Vector3 movement_drag;
	public static Vector3 movement_friction;
	public static Vector3 movement_gravity;


	/* PLAYER */
	public static float player_floor_height;
		/* MOVEMENT */
	public static float player_movement_acceleration;
	public static float player_movement_jump_strength;


	/* RENDER */
	public static float render_max_z;


	/* OBJECTS */
		/* WALLS */
	/* public static GameObject object_wall_left;
	public static GameObject object_wall_right; */
		/* COLUMNS */


	/* COMBAT */
		/* ATTRIBUTES */
			/* STRENGTH */
	public static int combat_attributes_strength_health;

			/* AGILITY */
	public static int combat_attributes_agility_dodge;

			/* INTELLIGENCE */

	/* CONSTANTS */
	public const int STRENGTH = 0;
	public const int AGILITY = 1;
	public const int INTELLIGENCE = 2;
}


/*
 * master is the game master. It writes all important values.
 * Communication occurs through the static class global.
 */
public class master : MonoBehaviour {
	void Awake() {
		/* Initialize global. */
		global.game_speed = 1.00f;
		global.game_scroll_speed = 1.00f;
		global.game_width = 9.00f;

		global.movement_drag = 0.50f * Vector3.one;
		global.movement_friction = 3.33f
				* new Vector3(1.00f, 0.00f, 1.00f);
		global.movement_gravity = -9.81f * Vector3.up;

		global.player_floor_height = 0.00f;
		global.player_movement_acceleration = 16.00f;
		global.player_movement_jump_strength = 3.00f;

		global.render_max_z = -2.00f;
	}
}
