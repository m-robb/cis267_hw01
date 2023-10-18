using static __global;
using UnityEngine;

public class enemy : MonoBehaviour {
	[System.NonSerialized] public combatant _combatant;
	[System.NonSerialized] public Bounds bounds;


	void Start() {
		_combatant = new combatant();
		bounds = GetComponent<SpriteRenderer>().bounds;

		combatant.initialize(_combatant);
		_combatant._attributes = new int[3] { 5, 5, 5 };
		_combatant._health = combatant.health_max(_combatant);
		_combatant._name = "Generic Enemy";
	}

	void FixedUpdate() {
		float z = transform.position.z;

		if (z <= render_max_z) {
			Destroy(gameObject);
		}
		else if (z <= object_player.transform.position.z
				&& collision_x(
					_player.transform,
					_player.bounds
				)) {
			Debug.Log(combat.engagement(
				_player._combatant,
				_combatant)
			);
		}

		if (_combatant._health <= 0) {
			Destroy(gameObject);
		}
	}

	private bool collision_x(Transform _transform, Bounds _bounds) {
		return transform.position.x + bounds.size.x / 2.00f >=
				_transform.position.x - _bounds.size.x / 2.00f
			|| transform.position.x - bounds.size.x / 2.00f <=
				_transform.position.x + _bounds.size.x / 2.00f;
	}
}
