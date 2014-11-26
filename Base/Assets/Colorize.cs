using UnityEngine;
using System.Collections;

public class Colorize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    renderer.material.color = new UnityEngine.Color(20, 20, 20, 255);
	}
	
	// Update is called once per frame
	void Update () {
        renderer.material.color = new UnityEngine.Color(20, 20, 20, 255);
	}
}
