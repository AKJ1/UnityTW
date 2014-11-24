using Assets.Game.Characters;
using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire"))
	    {
	        transform.GetComponent<Character>().Weapon.Attack();
	    }
	}
}
