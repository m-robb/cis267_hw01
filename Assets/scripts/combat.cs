/*
 * Anything that can fight should have stats.
 * Strength:
 *	-maximum health
 *	-damage with melee weapons
 * Agility:
 *	-dodge chance
 *	-chance to hit for both melee and ranged weapons
 *	-damage with ranged weapons
 * Intelligence:
 *	-chance to avoid traps
 *	-magic resistance?
 *	-slows game speed so that the player can think for longer (add toggle)
 */
public struct stats {
	public int strength;
	public int agility;
	public int intelligence;
	public weapon weapon;
	public armor armor;

	public int hit(stats enemy) {
		return (strength + weapon.damage) - (enemy.agility + enemy.armor.protection);
	}
}

public struct weapon {
	public int damage;
	public int accuracy;
}

public struct armor {
	public int protection;
	/* public int weight; */
}