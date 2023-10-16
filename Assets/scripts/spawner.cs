using UnityEngine;

/*
 * Assuming that there are five dividers (walls), meaning three paths.
 */
public class spawner : MonoBehaviour {
	public GameObject enemy_prefab;
	public GameObject[] columns;
	private utimer spawn_timer;


	/* Start is called before the first frame update. */
	void Start() {
		spawn_timer.start_time = Time.time;
		spawn_timer.duration = 3.33f;
	}

	void FixedUpdate() {
		if (spawn_timer.done(Time.time)) {
			GameObject enemy;
			Transform parent;
			Vector3 position;
			Quaternion rotation;

			enemy = Instantiate(enemy_prefab);

			parent = columns[
				Random.Range(0, columns.Length)
			].transform;

			position = new Vector3(
				parent.position.x,
				parent.position.y,
				10.00f
			);

			rotation = Quaternion.identity;

			enemy.transform.parent = parent;
			enemy.transform.position = position;
			enemy.transform.rotation = rotation;
			
			spawn_timer.start_time = Time.time;
		}
	}
}
