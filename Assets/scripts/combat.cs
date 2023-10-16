using UnityEngine;
using static global;


/*
 * Anything that can fight should have attributes.
 * Strength:
 *	-maximum health
 *	-damage with melee weapons
 *	-carry weight?
 * Agility:
 *	-dodge chance
 *	-chance to hit for both melee and ranged weapons
 *	-damage with ranged weapons
 * Intelligence:
 *	-chance to avoid traps or see them further away
 *	-magic resistance?
 *	-slows game speed so that the player can think for longer (add toggle)
 */
public struct combat {
	public int[] _attributes;
	public weapon _weapon;
	public armor _armor;

	public int health_current;


	/*
	 *
	 */
	public static int hit(combat attacker, combat defender) {
		float chance;	/* [0.00f, 1.00f] */
		int damage;
		int defense;

		/* Chance for the attacker to hit. */
		chance = Mathf.Clamp(
			(attacker._weapon.accuracy
					+ attacker._attributes[AGILITY])
					/ 100.00f,
			0.00f,
			1.00f
		);
		/* Chance for the defender to dodge. */
		chance *= Mathf.Clamp(
			(defender._armor.defense
					+ defender._attributes[AGILITY])
					/ 100.00f,
			0.00f,
			1.00f
		);

		/* See if the attacker hits the defender. CHECK THIS MATH!!! */
		if (Random.Range(0.00f, 1.00f) < chance) {
			return 0;
		}

		damage = attacker._attributes[
			attacker._weapon.primary_attribute
		] + attacker._weapon.damage;

		defense = defender._armor.defense;

		return damage - defense > 0 ? damage - defense : 0; 
	}

	/*
	 * Returns the max health of the combatant.
	 */
	public static int max_health(combat _combat) {
		return  _combat._attributes[STRENGTH]
				* global.combat_attributes_strength_health;
	}
}

public struct weapon {
	public string name;
	public int primary_attribute;
	public int damage;
	public int accuracy;
	public int ranged_damage;
}

public struct armor {
	public string name;
	public int defense;
	public int dodge;
}
