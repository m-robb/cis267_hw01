using static __global;
using UnityEngine;


/*
 * Increments the player score by one every tenth of a second.
 */
public class score_incrementer : MonoBehaviour {
	[System.NonSerialized] public utimer timer;


	void Start() {
		timer.start_time = Time.time;
		timer.duration = 0.25f;
	}

	void FixedUpdate() {
		if (_player._combatant._health > 0) {
			if (timer.done(Time.time)) {
				timer.start_time = Time.time;

				++game_score_time;
			}
		}
	}
}
