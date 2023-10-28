using System.IO;
using UnityEngine;
using UnityEngine.UI;


/*
 * Global values.
 * This is the method through which the game master interacts with the world.
 * Be careful about writing values here. General use should just be reading.
 */
public static class __global {
	/* GAME */
	public static float game_speed;
	public static float game_scroll_speed;
	public static float game_width;
	public static int game_score_time;


	/* MOVEMENT */
	public static Vector3 movement_drag;
	public static Vector3 movement_friction;
	public static Vector3 movement_gravity;


	/* PLAYER */
	public static player _player;
	public static float player_floor_height;
	public static float player_z;
	public static float player_offset_y;
		/* MOVEMENT */
	public static float player_movement_acceleration;
	public static float player_movement_jump_strength;


	/* RENDER */
	public static float render_max_z;


	/* OBJECTS */
		/* PLAYER */
	public static GameObject object_player;

		/* COMBAT */
	public static combatant[] object_combat_enemy;
			/* ITEM */
	public static weapon[] object_combat_item_weapon;
	public static armor[] object_combat_item_armor;

		/* UI */
			/* TEXT */
	public static Text object_ui_text_score;
	public static Text object_ui_text_health;
	public static Text object_ui_text_attribute_strength;
	public static Text object_ui_text_attribute_agility;
	public static Text object_ui_text_attribute_intelligence;


	/* COMBAT */
	public static int combat_experience_per_level;
	public static int combat_base_hit_chance;
	public static int combat_roll_minimum;
	public static int combat_roll_maximum;

		/* ATTRIBUTES */
			/* STRENGTH */
	public static int combat_attribute_strength_health;

			/* AGILITY */
	public static int combat_attribute_agility_dodge;

			/* INTELLIGENCE */


	/* CONSTANTS */
		/* COMBAT */
			/* ATTRIBUTES */
	public const int STRENGTH = 0;
	public const int AGILITY = 1;
	public const int INTELLIGENCE = 2;

		/* UI */
			/* TEXT */
	public const string UI_TEXT_SCORE = "TEXT_SCORE";
	public const string UI_TEXT_HEALTH = "TEXT_HEALTH";
	public const string UI_TEXT_ATTRIBUTE_STRENGTH
			= "TEXT_ATTRIBUTE_STRENGTH";
	public const string UI_TEXT_ATTRIBUTE_AGILITY
			= "TEXT_ATTRIBUTE_AGILITY";
	public const string UI_TEXT_ATTRIBUTE_INTELLIGENCE
			= "TEXT_ATTRIBUTE_INTELLIGENCE";
	public const string UI_TEXT_SCORE_DEFAULT = "SCORE: ";
	public const string UI_TEXT_HEALTH_DEFAULT = " HP: ";
	public const string UI_TEXT_ATTRIBUTE_STRENGTH_DEFAULT = "STR: ";
	public const string UI_TEXT_ATTRIBUTE_AGILITY_DEFAULT = "AGI: ";
	public const string UI_TEXT_ATTRIBUTE_INTELLIGENCE_DEFAULT = "INT: ";
	

	/* -------------------- GLOBAL FUNCTIONS -------------------- */
	/*
	 * Reads all weapons from file into an array.
	 * Returns the array filled with weapons.
	 */
	public static weapon[] combat_item_weapon_read() {
		string path = Application.streamingAssetsPath
				+ "/text/item/weapon.txt";
		const int lines_per_weapon = 8; /* Includes blank line. */
		weapon[] weapons;
		StreamReader stream_reader;
		int lines;
		int i;

		if (!File.Exists(path)) {
			Debug.LogError("weapon file not found at " + path);
			return new weapon[0];
		}

		stream_reader = new StreamReader(path);
		lines = 0;
		
		/* Find number of lines. */
		while (stream_reader.ReadLine() != null) { ++lines; }

		weapons = new weapon[lines / lines_per_weapon];

		stream_reader.BaseStream.Position = 0;
		stream_reader.DiscardBufferedData();
		
		i = 0;
		while (i < weapons.Length) {
			weapons[i].id = stream_reader.ReadLine();
			weapons[i].name = stream_reader.ReadLine();
			weapons[i].primary_attribute =
					int.Parse(stream_reader.ReadLine());
			weapons[i].crit_multiplier =
					float.Parse(stream_reader.ReadLine());
			weapons[i].damage =
					int.Parse(stream_reader.ReadLine());
			weapons[i].accuracy =
					int.Parse(stream_reader.ReadLine());
			weapons[i].ranged_damage =
					int.Parse(stream_reader.ReadLine());
			stream_reader.ReadLine();

			++i;
		}

		stream_reader.Close();

		return weapons;
	}

	/*
	 * Reads all armor from file into an array.
	 * Returns the array filled with weapons.
	 */
	public static armor[] combat_item_armor_read() {
		string path = Application.streamingAssetsPath
				+ "/text/item/armor.txt";
		const int lines_per_armor = 8; /* Includes blank line. */
		armor[] armors;
		StreamReader stream_reader;
		int lines;
		int i;

		if (!File.Exists(path)) {
			Debug.LogError("armor file not found at " + path);
			return new armor[0];
		}

		stream_reader = new StreamReader(path);
		lines = 0;
		
		/* Find number of lines. */
		while (stream_reader.ReadLine() != null) { ++lines; }

		armors = new armor[lines / lines_per_armor];

		stream_reader.BaseStream.Position = 0;
		stream_reader.DiscardBufferedData();
		
		i = 0;
		while (i < armors.Length) {
			armors[i].id = stream_reader.ReadLine();
			armors[i].name = stream_reader.ReadLine();
			armors[i].defense =
					int.Parse(stream_reader.ReadLine());
			armors[i].dodge = int.Parse(stream_reader.ReadLine());
			stream_reader.ReadLine();

			++i;
		}

		stream_reader.Close();

		return armors;
	}

	/*
	 * Reads all enemies from file into an array.
	 * Returns the array filled with weapons.
	 */
	public static combatant[] combat_enemy_read() {
		string path = Application.streamingAssetsPath
				+ "/text/enemy.txt";
		const int lines_per_enemy = 7; /* Includes blank line. */
		combatant[] enemy;
		StreamReader stream_reader;
		int lines;
		int i;

		if (!File.Exists(path)) {
			Debug.LogError("armor file not found at " + path);
			return new combatant[0];
		}

		stream_reader = new StreamReader(path);
		lines = 0;
		
		/* Find number of lines. */
		while (stream_reader.ReadLine() != null) { ++lines; }

		enemy = new combatant[lines / lines_per_enemy];

		stream_reader.BaseStream.Position = 0;
		stream_reader.DiscardBufferedData();
		
		i = 0;
		while (i < enemy.Length) {
			combatant.initialize(enemy[i]);
			enemy[i]._id = stream_reader.ReadLine();
			enemy[i]._name = stream_reader.ReadLine();
			enemy[i]._attributes[STRENGTH]
					= int.Parse(stream_reader.ReadLine());
			enemy[i]._attributes[AGILITY]
					= int.Parse(stream_reader.ReadLine());
			enemy[i]._attributes[INTELLIGENCE]
					= int.Parse(stream_reader.ReadLine());
			enemy[i]._experience
					= int.Parse(stream_reader.ReadLine());

			/* Add the ability to default equip enemies. */

			stream_reader.ReadLine();

			enemy[i]._health = combatant.health_max(enemy[i]);

			++i;
		}

		stream_reader.Close();

		return enemy;
	}

	/*
	 * Returns true if x is within grace of y.
	 */
	public static bool equals_grace(float x, float y, float grace) {
		return Mathf.Abs(x - y) <= grace;
	}

	/*
	 * Allows UI elements to identify themselves for future updating.
	 * id is a constant identifier found in __global.
	 * object_ui should be the UI element's GameObject.
	 */
	public static bool ui_identify(string id, GameObject object_ui) {
		if (id == UI_TEXT_SCORE) {
			object_ui_text_score = object_ui.GetComponent<Text>();
		}
		else if (id == UI_TEXT_HEALTH) {
			object_ui_text_health = object_ui.GetComponent<Text>();
		}
		else if (id == UI_TEXT_ATTRIBUTE_STRENGTH) {
			object_ui_text_attribute_strength
					= object_ui.GetComponent<Text>();
		}
		else if (id == UI_TEXT_ATTRIBUTE_AGILITY) {
			object_ui_text_attribute_agility
					= object_ui.GetComponent<Text>();
		}
		else if (id == UI_TEXT_ATTRIBUTE_INTELLIGENCE) {
			object_ui_text_attribute_intelligence
					= object_ui.GetComponent<Text>();
		}
		else {
			Debug.LogError("Failed to ui_identify id of " + id);
			return false;
		}

		return true;
	}

	/*
	 * Calculates the score.
	 * score is to the sum of score from time and score from attributes.
	 */
	public static int score_calculate() {
		return game_score_time
				+ combat.attribute_sum(_player._combatant);
	}

	/*
	 * Updates all known game UI elements.
	 */
	public static void ui_update() {
		if (object_ui_text_score) {
			object_ui_text_score.text = UI_TEXT_SCORE_DEFAULT
					+ string.Format(
						"{0:00000}",
						game_score_time
							+ score_calculate()
					);
		}

		if (object_ui_text_health) {
			object_ui_text_health.text = UI_TEXT_HEALTH_DEFAULT
					+ string.Format(
						"{0:000}",
						_player._combatant._health
					);
		}

		if (object_ui_text_attribute_strength) {
			object_ui_text_attribute_strength.text =
					UI_TEXT_ATTRIBUTE_STRENGTH_DEFAULT
					+ string.Format(
						"{0:000}",
						_player._combatant
						._attributes[STRENGTH]
					);
		}

		if (object_ui_text_attribute_agility) {
			object_ui_text_attribute_agility.text =
					UI_TEXT_ATTRIBUTE_AGILITY_DEFAULT
					+ string.Format(
						"{0:000}",
						_player._combatant
						._attributes[AGILITY]
					);
		}

		if (object_ui_text_attribute_intelligence) {
			object_ui_text_attribute_intelligence.text =
					UI_TEXT_ATTRIBUTE_INTELLIGENCE_DEFAULT
					+ string.Format(
						"{0:000}",
						_player._combatant
						._attributes[INTELLIGENCE]
					);
		}
	}

	/*
	 * Returns the total size of all Renderers in game_object.
	 */
	public static Vector3 size_total(GameObject game_object) {
		return game_object.transform.Find("hitbox").lossyScale;
	}

	/*
	 * Returns the level a combatant would be with experience.
	 * Levels require exponentially more experience.
	 */
	public static int combat_level(int experience) {
		int level;

		if (experience == 0) { return 1; }

		level = 0;
		while (experience >= 0) {
			++level;
			experience -= level * combat_experience_per_level;
		}

		return level;
	}

	/*
	 * Returns the word associated with an attribute's number.
	 */
	public static string attribute_text(int attribute) {
		switch (attribute) {
			case (STRENGTH): { return "STRENGTH"; }
			case (AGILITY): { return "AGILITY"; }
			case (INTELLIGENCE): { return "INTELLIGENCE"; }
			default: {
				Debug.LogError("attribute" + attribute
						+ " does not exist.");
				return "ERROR";
			}
		}
	}

	/*
	 * Returns the abbreviation associated with an attribute's number.
	 * The abbreviation will be three characters in length;
	 */
	public static string attribute_text_short(int attribute) {
		return attribute_text(attribute).Substring(0, 3);
	}
}
