using UnityEngine;

public class camera_follow : MonoBehaviour {
	public GameObject target;
	
	private Transform target_transform;

	void Start() {
		target_transform = target.transform;
	}

	void Update() {
		transform.position = new Vector3(
			target_transform.position.x,
			transform.position.y,
			transform.position.z
		);
	}
}
