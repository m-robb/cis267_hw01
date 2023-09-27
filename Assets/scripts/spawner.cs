using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using utimer;

public class spawner : MonoBehaviour {
	public GameObject enemy;
	public GameObject[] columns;
	private utimer spawn_timer;


	/* Start is called before the first frame update. */
	void Start() {
		spawn_timer.start_time = Time.time;
		spawn_timer.duration = 3.33;
	}

	void FixedUpdate() {
		if (spawn_timer.done()) {
			Instantiate(
				enemy,
				columns[
					Random.Range(
						0,
						columns.Length
					)
				].transform,
				Vector3.zero
			);

			spawn_timer.start_time = Time.time;
		}
	}
}
