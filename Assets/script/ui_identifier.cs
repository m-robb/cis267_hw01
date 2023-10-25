using static __global;
using UnityEngine;

public class ui_identifier : MonoBehaviour {
	public string id;

	void Awake() {
		ui_identify(id, gameObject);
		Destroy(this);
	}
}
