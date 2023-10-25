using static __global;
using UnityEngine;


public class scroll_texture_game_speed : MonoBehaviour {
	public float speed;

	[System.NonSerialized] public Renderer _renderer;


	void Start() {
		_renderer = GetComponent<Renderer>();
	}

	void Update() {
		_renderer.material.mainTextureOffset += Vector2.right
				* Time.deltaTime * speed;
	}
}
