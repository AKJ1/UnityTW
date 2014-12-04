using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.LookAt(Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
