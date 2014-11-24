using UnityEngine;
using System.Collections;

public class ColliderEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter()
    {
        Debug.Log("Ding-Dong Coming through!");
    }
}
