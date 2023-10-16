using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_movement : MonoBehaviour {
	private position_helper positioner;
	private SpriteRenderer sprite_renderer;
	private float height_offset;
	private float width_offset;

	void Start() {
		sprite_renderer = GetComponent<SpriteRenderer>();
		height_offset = sprite_renderer.bounds.size.y / 2;
		width_offset = sprite_renderer.bounds.size.x / 2;

		positioner.initialize();
		positioner.timestep = Time.fixedDeltaTime;
		positioner.drag = global.movement_drag;
		positioner.friction = global.movement_friction;
		positioner.gravity = global.movement_gravity;
		positioner.jump_force = global.player_movement_jump_strength;
	}

	void Update() {
		positioner.move_x(Input.GetAxisRaw("horizontal"));
		positioner.move_jump(Input.GetAxisRaw("jump"));
		transform.position += positioner.interpolate(Time.time)
				* global.game_speed;
	}

	void FixedUpdate() {
		transform.position += positioner.velocity_flush()
				* global.game_speed;

		positioner.on_ground = grounded(global.player_floor_height);

		/* Enforce collisions with the floor and side walls. */
		if (positioner.on_ground) {
			floor_enforce();
		}
		if (Mathf.Abs(transform.position.x) + width_offset
				> global.game_width/2) {
			width_enforce(Mathf.Sign(transform.position.x));
		}

		positioner.tick(global.player_movement_acceleration);
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
			global.player_floor_height + height_offset,
			transform.position.z
		);
	}

	/*
	 * Prevents the player from walking out of bounds.
	 */
	private void width_enforce(float sign) {
		positioner.velocity.x = 0.00f;
		transform.position = new Vector3(
			sign * (global.game_width/2 - width_offset),
			transform.position.y,
			transform.position.z
		);
	}
}
