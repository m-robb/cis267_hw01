using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_scroll : MonoBehaviour {
	public float speed;

	void Update() {
		this.transform.position += new Vector3(
			0,
			0,
			-1 * Time.deltaTime
		);

		if (this.transform.position.z <= 0) { Destroy(this.gameObject); }
	}
}
