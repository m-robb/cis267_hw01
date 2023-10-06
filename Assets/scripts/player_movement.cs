using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_movement : MonoBehaviour {
	public float acceleration;
	public float friction;

	private float latest_move_x;

	private fixed_position_helper positioner;

	void Start() {
		latest_move_x = 0.00f;

		positioner.initialize();
		positioner.fixed_timestep = Time.fixedDeltaTime;
		positioner.acceleration = acceleration;
		positioner.friction = new Vector3(1, 0, 1) * friction;
		positioner.gravity  = new Vector3(0.00f, 0.00f, 0.00f);
	}

	void Update() {
		latest_move_x = move_x();
		transform.position += positioner.interpolated_move(Time.time);
	}

	void FixedUpdate() {
		positioner.accelerate(new Vector3(latest_move_x, 0, 0));
		transform.position += positioner.tick();
	}

	private float move_x() { return Input.GetAxisRaw("horizontal"); }
}
