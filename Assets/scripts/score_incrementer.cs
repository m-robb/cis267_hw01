using static __global;
using UnityEngine;


/*
 * Increments the player score by one every tenth of a second.
 */
public class score_incrementer : MonoBehaviour {
	[System.NonSerialized] public float score_time_previous;


	void Start() {
		score_time_previous = Time.time;
	}

	void FixedUpdate() {
		if (_player._combatant._health > 0) {
			if (Time.time >= score_time_previous + 0.10f) {
				++game_score_time;
				score_time_previous = Time.time;
			}
		}
	}
}
