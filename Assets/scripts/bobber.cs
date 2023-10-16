using UnityEngine;

/*
 * Bobs the attached Transform with amplitude increasing with speed.
 */
public class bobber : MonoBehaviour {
	public float total_distance;
	public float speed_base;

	private float bottom_offset;
	private float bob; /* [0.00f, 1.00f] */
	private bool positive;

	private const float bob_max = 1.00f;


	void Start() {
		bob = 0.00f;
		positive = true;
		bottom_offset = transform.localPosition.y - total_distance / 2;
	}

	void Update() {
		bob += speed_base * global.game_scroll_speed * Time.deltaTime
				 * (positive ? 1.00f : -1.00f);
		if (bob > 1.00f | bob < 0.00f) {
			positive = !positive;
			bob += (bob % 1.00f) * 2.00f
					* (positive ? 1.00f : -1.00f);
		}

		transform.localPosition = new Vector3(
			transform.localPosition.x,
			bottom_offset + total_distance * bob,
			transform.localPosition.z
		);
	}
}
