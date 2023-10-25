using static __global;
using UnityEngine;

public class trap_distance_fade : MonoBehaviour {
	public float resistance;

	[System.NonSerialized] public SpriteRenderer sprite_renderer;


	void Start() {
		sprite_renderer = transform.Find("body")
				.GetComponent<SpriteRenderer>();
	}

	void Update() {
		Color color;

		color = sprite_renderer.color;

		color.a = _player._combatant._attributes[INTELLIGENCE]
				/ (10.00f * resistance);
	}
}
