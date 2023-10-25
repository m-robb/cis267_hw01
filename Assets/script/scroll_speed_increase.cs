using static __global;
using UnityEngine;

public class scroll_speed_increase : MonoBehaviour {
	public float increment;
	public float time;

	[System.NonSerialized] public utimer timer;


	void Start() {
		timer.start_time = Time.time;
		timer.duration = time;
	}

	void Update() {
		if (timer.done(Time.time)) {
			game_scroll_speed += increment;

			timer.start_time = Time.time;
		}
	}
}
