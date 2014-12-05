using Assets.Game.Characters;
using Assets.Game.Equipment;
using Assets.Game.Equipment.Weapons;
using UnityEngine;
using System.Collections;

public class DebugInterface : MonoBehaviour
{

    private GameObject player;
    private bool init;
    private Character playerChar;

    void Init()
    {
        player = GameObject.Find("Player");
        playerChar = player.GetComponent<Character>();
    }
    void OnGUI()
    {
        if (!init)
        {
            Init();
            init = true;
        }
        if (GUI.Button(new Rect(Screen.width - 120, 10, 100, 50), "Sword" ))
        {
            Destroy(playerChar.Weapon);
            playerChar.Weapon = player.AddComponent<Sword>();
        }
        if (GUI.Button(new Rect(Screen.width - 120, 70, 100, 50), "Hammer"))
        {
            Destroy(playerChar.Weapon);
            playerChar.Weapon = player.AddComponent<Hammer>();
        }
        if (GUI.Button(new Rect(Screen.width - 120, 130, 100, 50), "Dagger"))
        {
            Destroy(playerChar.Weapon);
            playerChar.Weapon = player.AddComponent<Dagger>();
        }
        if (GUI.Button(new Rect(Screen.width - 120, 190, 100, 50), "Shotgun"))
        {
            Destroy(playerChar.Weapon);
            playerChar.Weapon = player.AddComponent<Shotgun>();
        } 
        if (GUI.Button(new Rect(Screen.width - 120, 250, 100, 50), "Bow"))
        {
            Destroy(playerChar.Weapon);
            playerChar.Weapon = player.AddComponent<Bow>();
        }
    }
}
