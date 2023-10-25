using static __global;
using UnityEngine;

public class enemy : MonoBehaviour {
	public string _id;
	public string _name;
	public int strength;
	public int agility;
	public int intelligence;
	public bool trap;

	[System.NonSerialized] public combatant _combatant;
	[System.NonSerialized] public Transform hitbox;
	[System.NonSerialized] public float z_previous;


	void Start() {
		hitbox = transform.Find("hitbox");

		_combatant = new combatant();
		combatant.initialize(_combatant);
		_combatant._id = _id;
		_combatant._name = _name;
		_combatant._attributes[STRENGTH] = strength;
		_combatant._attributes[AGILITY] = agility;
		_combatant._attributes[INTELLIGENCE] = intelligence;
		_combatant._trap = trap;
		_combatant._health = combatant.health_max(_combatant);

		z_previous = transform.position.z;
	}

	void FixedUpdate() {
		float z;

		z = transform.position.z;

		/* Destroy if enemy is out of the render range. */
		if (z <= render_max_z) {
			Destroy(gameObject);
		}
		/* Commence engagement. */
		else if (
			(z_previous >= _player.transform.position.z
					&& z <= _player.transform.position.z)
					&& collision_xy(_player.hitbox)
		) {
			Debug.Log(combat.engagement(
				_player._combatant,
				_combatant)
			);

			if (_combatant._trap) {
				Destroy(gameObject);
			}
			/* Give the player experience if they won. */
			else if (_combatant._health <= 0) {
				_player._combatant._experience
						+= combatant
						.experience_value(_combatant);
			}
		}

		/* Destroy the enemy if they have zero health. */
		if (_combatant._health <= 0 && !_combatant._trap) {
			Destroy(gameObject);
		}

		z_previous = z;
	}

	/*
	 * Detects collisions with _transform (and their accompanying _bounds).
	 * Detects collisions in 2D (x and y), the z-axis is ignored.
	 * Returns true if there is a collision.
	 */
	public bool collision_xy(Transform _hitbox) {
		Rect rect, _rect;

		rect = new Rect(
			hitbox.position.x - hitbox.lossyScale.x / 2.00f,
			hitbox.position.y - hitbox.lossyScale.y / 2.00f,
			hitbox.lossyScale.x,
			hitbox.lossyScale.y
		);

		_rect = new Rect(
			_hitbox.position.x - _hitbox.lossyScale.x / 2.00f,
			_hitbox.position.y - _hitbox.lossyScale.y / 2.00f,
			_hitbox.lossyScale.x,
			_hitbox.lossyScale.y
		);

		return rect.Overlaps(_rect);
	}
}
