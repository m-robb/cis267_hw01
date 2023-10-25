using static __global;
using UnityEngine;

public class persist : MonoBehaviour {
	void Awake() {
		Object.DontDestroyOnLoad(gameObject);
		Destroy(this);
	}
}
