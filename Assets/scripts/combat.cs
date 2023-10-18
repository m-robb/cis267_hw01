using static __global;
using UnityEngine;


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
public class combatant {
	public string _name;
	public int[] _attributes;
	public weapon _weapon;
	public armor _armor;

	public int _health;


	/*
	 * Initializes all of _combatant's values to 0.
	 */
	public static void initialize(combatant _combatant) {
		_combatant._name = "";
		_combatant._attributes = new int[3] { 0, 0, 0 };
		_combatant._health = combatant.health_max(_combatant);

		_combatant._weapon = object_combat_item_weapon[0];
		_combatant._armor = object_combat_item_armor[0];
	}


	/*
	 * Returns the max health of the combatant.
	 */
	public static int health_max(combatant _combatant) {
		return  _combatant._attributes[STRENGTH]
				* combat_attribute_strength_health;
	}

	/*
	 * Returns the  experience gain for defeating _combatant.
	 * Currently only gives the sum of stats as experience.
	 */
	public static int experience_value(combatant _combatant) {
		int experience;

		experience = 0;

		experience += _combatant._attributes[STRENGTH];
		experience += _combatant._attributes[AGILITY];
		experience += _combatant._attributes[INTELLIGENCE];

		return experience;
	}
}

public static class combat {
	/*
	 * Calculates chance for attacker to hit defender and damage dealt.
	 * Rerounds damage dealt (0 on miss).
	 */
	public static int hit(combatant attacker, combatant defender) {
		float chance; /* [0.00f, 1.00f] */
		float roll;
		int damage;
		int defense;
		bool crit;

		crit = false;

		/* Chance for the attacker to hit. */
		chance = combat_base_hit_chance
				+ attacker._attributes[AGILITY]
				+ attacker._weapon.accuracy
				- (defender._attributes[AGILITY]
				* combat_attribute_agility_dodge)
				- defender._armor.dodge;

		/* See if the attacker hits the defender. */
		roll = Random.Range(
			combat_roll_minimum,
			combat_roll_maximum + 1
		);

		/* Critical hit. Guarantees hit. Double damage. */
		if (roll == combat_roll_maximum) {
			crit = true;
		}
		/* Critical fail. Guarantees miss. Potential effect? */
		else if (roll == combat_roll_minimum) {
			return -1;
		}
		/* Miss. */
		else if (roll > chance) {
			return -1;
		}
		/* Tie. */
		else if (roll == chance) {
			return -1;
		}

		damage = attacker._attributes[
			attacker._weapon.primary_attribute
		] + attacker._weapon.damage;

		damage = (int)((float)damage * (
			crit
			? attacker._weapon.crit_multiplier
			: 1.00f
		));

		defense = defender._armor.defense;

		return damage - defense > 0 ? damage - defense : 0;
	}

	/*
	 * An engagement is combat to the death.
	 * Returns when a combatant's health is reduced to 0.
	 * Combatants take turns hitting each other, starting with attacker.
	 */
	public static string engagement(
		combatant attacker,
		combatant defender
	) {
		string log;
		combatant offense;
		combatant defense;
		int damage;
		int health_old;
		int turn; /* 2 turns in 1 round. */
		int round; /* Only used for display. */

		turn = 0;
		round = 0;
		offense = attacker;
		defense = defender;
		log = "";

		while (attacker._health > 0 && defender._health > 0) {
			/* Only increment round every other turn. */
			++turn;
			if ((turn & 1) == 1) { ++round; }

			/* Set the new attacker and defender for the turn. */
			offense = (turn & 1) == 1 ? attacker : defender;
			defense = (turn & 1) == 1 ? defender : attacker;

			health_old = defense._health;

			damage = combat.hit(offense, defense);


			/* Only add round marquee if there is a new round. */
			if ((turn & 1) == 1) {
				log += "-----ROUND " + round + "-----\n";
			}

			if (damage == -1) {
				log += offense._name + " misses ";
				log += defense._name + " . Oops!\n";
			}
			else {
				/* Make sure we don't heal combatants... */
				defense._health -= damage;

				log += offense._name + " hits ";
				log += defense._name + " for ";
				log += damage + " damage. [";
				log += health_old + " -> ";
				log += defense._health + "]\n";
			}
		}

		log += defense._name + " dies...";

		return log;
	}

	/*
	 *
	 */
	public static int attribute_sum(combatant _combatant) {
		return _combatant._attributes[STRENGTH]
				+ _combatant._attributes[AGILITY]
				+ _combatant._attributes[INTELLIGENCE];
	}
}

public struct weapon {
	public string id;
	public string name;
	public int primary_attribute;
	public float crit_multiplier;
	public int damage;
	public int accuracy;
	public int ranged_damage;
}

public struct armor {
	public string id;
	public string name;
	public int defense;
	public int dodge;
}
