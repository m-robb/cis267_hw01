using UnityEngine;


/*
 * Custom movement system that utilies acceleration and interpolation.
 * Changes only take effect after tick is called.
 */
public struct position_helper {
	public Vector3 velocity; /* Do not touch. Measured in uu/s. */
	public Vector3 velocity_change_move;
	public Vector3 velocity_change_arbitrary;
	public Vector3 drag; /* Slows the transform down always. */
	public Vector3 friction; /* Slows the transform down on the ground. */
	public Vector3 gravity;  /* (0, -9.81, 0) is a good idea. */
	public float jump_force;
	public float velocity_change_jump;
	public float timestep; /* Set to Time.fixedDeltaTime. */
	public float tick_percentage_last;
	public bool on_ground; /* Set if the transform is on the floor. */


	/*
	 * Call in FixedUpdate.
	 * Updates the velocity with all accumulated forces.
	 * Update on_ground before after velocity_flush and before tick.
	 * Removes any unused velocity. Always call velocity_flush before.
	 * Sets on_ground to false if a jump occurred.
	 */
	public void tick(float acceleration) {
		Vector3 force_counter;

		/* Prevent accumulation of gravity while on the ground. */
		if (on_ground) {
			velocity.y = 0.00f;
		}

		/* Add accumulated velocity from inputs from previous tick. */
		velocity += velocity_change_arbitrary;

		/* Only allow jumping and basic movement when on the ground. */
		if (on_ground) {
			velocity += velocity_change_move * acceleration
					* timestep;

			if (velocity_change_jump != 0.00f) {
				/*
				 * Jump is not multiplied by timestep because
				 * it is instantaneous velocity.
				 */
				velocity.y += velocity_change_jump
						* jump_force;

				/* Force disable for gravity checks. */
				on_ground = false;
			}
		}

		/*
		 * Formerly a part of force_counter.
		 * Moved because I want it to be affected by drag.
		 */
		if (!on_ground) {
			velocity += gravity * timestep;
		}

		/* Create and apply slow down forces (counter force). */
		force_counter = Vector3.Scale(velocity, -drag) * timestep;
		if (on_ground) {
			force_counter += Vector3.Scale(velocity, -friction)
					* timestep;
		}

		velocity += force_counter;

		/* Reset accumulators for the new tick. */
		velocity_change_move = Vector3.zero;
		velocity_change_arbitrary = Vector3.zero;
		velocity_change_jump = 0.00f;

		/* Apply this just in case velocity_flush wasn't called. */
		tick_percentage_last = 0.00f;
	}

	/*
	 * Call in FixedUpdate before tick.
	 * Only call once per tick.
	 * Returns the leftover velocity that hasn't been applied yet. Use it.
	 * Sets tick_percentage_last to 0.00f.
	 * This was split from tick to allow for updating on_ground.
	 */
	public Vector3 velocity_flush() {
		tick_percentage_last = 0.00f;

		return velocity * timestep
				* (1.00f - tick_percentage_last);
	}

	/*
	 * Call in Update.
	 * time should be be Time.time.
	 * Interpolates the movement calculated in FixedUpdate for display.
	 * Add the return value to the object's position.
	 */
	public Vector3 interpolate(float time) {
		Vector3 velocity_interpolated;
		float tick_percentage;

		tick_percentage = (time % timestep) / timestep;

		/*
		 * Convert velocity from:
		 * per second -> per tick -> per percentage of tick
		 */
		velocity_interpolated = velocity * timestep
				* (tick_percentage - tick_percentage_last);

		tick_percentage_last = tick_percentage;

		return velocity_interpolated;
	}

	/*
	 * Records acceleration along the x-axis to apply in tick.
	 * move is both strength [0.00f, 1.00f] and direction [-1.00f, 1.00f].
	 * Only the most extreme call of move_x per tick is used.
	 */
	public void move_x(float move) {
		velocity_change_move.x = move_max(
			velocity_change_move.x,
			Mathf.Clamp(move, -1.00f, 1.00f)
		);
	}

	/*
	 * Records acceleration along the y-axis to apply in tick.
	 * move is both strength [0.00f, 1.00f] and direction [-1.00f, 1.00f].
	 * Only the most extreme call of move_y per tick is used.
	 */
	public void move_y(float move) {
		velocity_change_move.y = move_max(
			velocity_change_move.y,
			Mathf.Clamp(move, -1.00f, 1.00f)
		);
	}

	/*
	 * Records acceleration along the z-axis to apply in tick.
	 * move is both strength [0.00f, 1.00f] and direction [-1.00f, 1.00f].
	 * Only the most extreme call of move_z per tick is used.
	 */
	public void move_z(float move) {
		velocity_change_move.z = move_max(
			velocity_change_move.z,
			Mathf.Clamp(move, -1.00f, 1.00f)
		);
	}

	/*
	 * Records acceleration along the y-axis (jumping) to apply in tick.
	 * Only the most extreme call of move_jump per tick is used.
	 * Checks if the player is on the ground when tick is called not when
	 * 		move_jump is called.
	 */
	public void move_jump(float strength) {
		velocity_change_jump = move_max(
			velocity_change_jump,
			Mathf.Clamp(strength, 0.00f, 1.00f)
		);
	}

	/*
	 * Call anywhere. Use sparingly.
	 * Applies an arbitrary force.
	 * Use accelerate for basic movement.
	 */
	public void move_arbitrary(Vector3 force) {
		velocity_change_arbitrary += force;
	}

	/*
	 * Returns the maximum of the absolute value of the two values.
	 */
	private float move_max(float previous, float current) {
		if (Mathf.Abs(current) > Mathf.Abs(previous)) {
			return current;
		}

		return previous;
	}

	/*
	 * Sets all values to zero.
	 * Required to use in Start unless you want to do it manually.
	 */
	public void initialize() {
		velocity = Vector3.zero;
		velocity_change_move = Vector3.zero;
		velocity_change_arbitrary = Vector3.zero;
		drag = Vector3.zero;
		friction = Vector3.zero;
		gravity = Vector3.zero;
		jump_force = 0.00f;
		velocity_change_jump = 0.00f;
		timestep = 0.00f;
		tick_percentage_last = 0.00f;
		on_ground = false;
	}
}
