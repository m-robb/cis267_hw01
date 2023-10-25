using static __global;
using UnityEngine;

public class scroll : MonoBehaviour {
	void Update() {
		transform.position += -Vector3.forward * Time.deltaTime
				* game_scroll_speed * game_speed;
	}
}
