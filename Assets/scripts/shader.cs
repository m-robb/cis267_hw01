using UnityEngine;

public class shader : MonoBehaviour {
	public GameObject shader_root;

	private Sprite sprite;


	void Start() {
		sprite = Resources.Load<Sprite>("sprites/primitives/square");

		if (!shader_root) { shader_root = gameObject; }
		
		shade(0.00f, 5.00f, 50, 0.05f);
	}

	/*
	 *
	 */
	public void shade(float start, float end, int count, float alpha) {
		const float SIZE = 500;
		Transform shader_root_transform;
		GameObject object_tmp;
		SpriteRenderer renderer;
		float spacing;
		int i;

		shader_root_transform = shader_root.transform;

		/* Destroy all children of shader_root. */
		i = shader_root_transform.childCount;
		while (i > 0) {
			Destroy(shader_root_transform.GetChild(i).gameObject);
		}

		spacing = (start - end) / (count - 1);

		for (i = 0; i < count + 1; ++i) {
			object_tmp = new GameObject("shade" + i);
			renderer = object_tmp.AddComponent<SpriteRenderer>();

			object_tmp.transform.parent = shader_root_transform;
			object_tmp.transform.rotation = Quaternion.identity;
			object_tmp.transform.position = new Vector3(
				0.00f,
				0.00f,
				shader_root_transform.position.z - i * spacing
			);
			object_tmp.transform.localScale = Vector3.one * SIZE;

			renderer.sprite = sprite;
			renderer.color = new Color(0.00f, 0.00f, 0.00f, alpha);
		}
	}
}
