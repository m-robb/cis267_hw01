using static __global;
using UnityEngine;


public class dragon_movement : MonoBehaviour {
	public float speed;

	[System.NonSerialized] public position_helper positioner;
	[System.NonSerialized] public float height_offset;
	[System.NonSerialized] public float width_offset;

	void Start() {
		/* height_offset = bounds.y / 2;
		width_offset = bounds.x / 2; */

		positioner.initialize();
		positioner.timestep = Time.fixedDeltaTime;
		positioner.drag = movement_drag;
		positioner.friction = movement_friction;
		/* positioner.gravity = movement_gravity; */
		positioner.jump_force = 0.00f;
	}

	void Update() {
		positioner.move_z(-1.00f);

		transform.position += positioner.interpolate(Time.time)
				* game_speed;
	}

	void FixedUpdate() {
		transform.position += positioner.velocity_flush()
				* game_speed;

		positioner.on_ground = grounded(player_floor_height);

		/* Enforce collisions with the floor and side walls. */
		/* if (positioner.on_ground) {
			floor_enforce();
		} */
		/* if (Mathf.Abs(transform.position.x) + width_offset
				> game_width/2) {
			width_enforce(Mathf.Sign(transform.position.x));
		} */

		positioner.tick(speed);
	}

	/*
	 * Returns true if the transform is on the ground.
	 * Accounts for offset.
	 */
	private bool grounded(float floor_height) {
		return transform.position.y - height_offset <= floor_height;
	}

	/*
	 * Adheres the player to the floor.
	 */
	private void floor_enforce() {
		transform.position = new Vector3(
			transform.position.x,
			player_floor_height + height_offset,
			transform.position.z
		);
	}

	/*
	 * Prevents the player from walking out of bounds.
	 */
	private void width_enforce(float sign) {
		positioner.velocity.x = 0.00f;
		transform.position = new Vector3(
			sign * (game_width/2 - width_offset),
			transform.position.y,
			transform.position.z
		);
	}
}
