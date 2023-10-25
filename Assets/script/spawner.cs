using static __global;
using UnityEngine;

/*
 * Assuming that there are five dividers (walls), meaning three paths.
 */
public class spawner : MonoBehaviour {
	public GameObject[] enemies;
	public GameObject[] columns;
	private utimer spawn_timer;


	/* Start is called before the first frame update. */
	void Start() {
		spawn_timer.start_time = Time.time;
		spawn_timer.duration = 3.33f;
	}

	void FixedUpdate() {
		if (spawn_timer.done(Time.time)) {
			GameObject _enemy;
			Transform parent;
			Vector3 position;

			_enemy = Instantiate(
				enemies[Random.Range(0, enemies.Length)]
			);

			parent = columns[
				Random.Range(0, columns.Length)
			].transform;


			position = new Vector3(
				parent.position.x,
				0.00f,
				10.00f
			);

			_enemy.transform.parent = parent;
			_enemy.transform.position = position;
			
			spawn_timer.start_time = Time.time;
		}
	}
}
