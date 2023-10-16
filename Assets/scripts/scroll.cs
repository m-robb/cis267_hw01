using UnityEngine;

public class scroll : MonoBehaviour {
	/* private position_helper positioner;


	void Start() {
		positioner.initialize();
		positioner.timestep = Time.deltaTime;
		positioner.on_ground = true;

		positioner.move_arbitrary(-Vector3.forward);
	}

	void Update() {
		transform.position += positioner.interpolate(Time.time);

		if (transform.position.z <= global.render_max_z) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		transform.position += positioner.velocity_flush();
		positioner.tick(0.00f);
	} */

	void Update() {
		transform.position += -Vector3.forward * Time.deltaTime
				* global.game_scroll_speed * global.game_speed;

		if (transform.position.z <= global.render_max_z) {
			Destroy(gameObject);
		}
	}
}
