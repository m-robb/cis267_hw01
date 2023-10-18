using static __global;
using UnityEngine;

public class camera_follow : MonoBehaviour {
	private Transform target_transform;

	void Update() {
		if (!target_transform) {
			target_transform = _player.transform;
		}
		else {
			transform.position = new Vector3(
				target_transform.position.x,
				transform.position.y,
				transform.position.z
			);
		}
	}
}
