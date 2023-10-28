using static __global;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class game_over_update : MonoBehaviour {
	string path = Application.streamingAssetsPath + "/text/highscore.txt";
	public GameObject score;
	public GameObject score_high;

	[System.NonSerialized] public Text score_text;
	[System.NonSerialized] public Text score_high_text;
	[System.NonSerialized] public int score_high_legacy;


	void Start() {
		int score_new;

		score_text = score.GetComponent<Text>();
		score_high_text = score_high.GetComponent<Text>();
		score_new = score_calculate();

		score_save(score_new);
		score_high_legacy = score_load();

		score_text.text = UI_TEXT_SCORE_DEFAULT + "     "
				+ string.Format("{0:00000}", score_new
				);

		score_high_text.text = "HIGH " + UI_TEXT_SCORE_DEFAULT
				+ string.Format("{0:00000}", score_high_legacy
				);
	}

	public void score_save(int score) {
		if (score > score_load()) {
			BinaryFormatter formatter_binary
					= new BinaryFormatter();
			FileStream stream = new FileStream(
				path,
				FileMode.Create
			);

			formatter_binary.Serialize(stream, score);

			stream.Close();
		}
	}

	public int score_load() {
		if (File.Exists(path)) {
			BinaryFormatter formatter_binary
					= new BinaryFormatter();
			FileStream stream
					= new FileStream(path, FileMode.Open);
			int score = (int)formatter_binary.Deserialize(stream);
			stream.Close();

			return score;
		}
		else {
			Debug.LogError("Highscore file not found at " + path);
			return -1;
		}
	}
}
