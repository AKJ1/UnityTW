using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Characters
{
    abstract class Character : MonoBehaviour, IMovable, IKillable
    {
        public abstract void Move();
        public abstract void TakeDamage();
        public Weapon Weapon;
        public void Kill()
        {
            Destroy(this.gameObject);
        }

    }

    class Weapon
    {
        public string Name;
        public WeaponType Type;
    }

    enum WeaponType
    {
        Hammer,
        Shotgun,
        Sword,

    }
}
