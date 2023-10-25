using UnityEngine;


[ExecuteInEditMode]
public class camera_shader : MonoBehaviour {
	public Material material;

	[System.NonSerialized] public Camera _camera;


	void Start() {
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.DepthNormals;

		material = new Material(Shader.Find("shader/camera/depth"));
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		Graphics.Blit(source, destination, material);
	}
}
