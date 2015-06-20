using UnityEngine;
using System.Collections;

public class SphereConstructor : MonoBehaviour {

	public string type;
	public Color _color;
	public float scale = 1f;

	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor("_Color", _color);

		transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
