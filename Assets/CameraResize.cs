using UnityEngine;
using System.Collections;

public class CameraResize : MonoBehaviour {

    public float aspect = 768.0f/ 1024.0f;
    // Use this for initialization
    void Start ()
    {
    float orthographicSize = Camera.main.orthographicSize;
    Camera.main.projectionMatrix = Matrix4x4.Ortho(
                   -orthographicSize * aspect, orthographicSize * aspect,
                   -orthographicSize, orthographicSize,
                   Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
