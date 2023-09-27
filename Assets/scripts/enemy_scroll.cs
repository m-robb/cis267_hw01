using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_scroll : MonoBehaviour {
	public float speed;


	/* Start is called before the first frame update. */
	void Start() {}

	/* Update is called once per frame. */
	void Update() {}

	void FixedUpdate() {
		this.transform.position += new Vector3(
			0,
			0,
			-1 * Time.fixedDeltaTime
		);

		Debug.Log(this.transform.position);
	}
}
