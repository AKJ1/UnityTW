using UnityEngine;
using System.Collections;

public class AddVisualizer : MonoBehaviour
{

    public GameObject Animation;
	// Use this for initialization
	void Start ()
	{
	    Instantiate(Animation, transform.position, Quaternion.Euler(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
