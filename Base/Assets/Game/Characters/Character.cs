using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Game.Equipment;
using UnityEngine;

namespace Assets.Game.Characters
{
    public abstract class Character : MonoBehaviour
    {
        //public abstract void Move(); Implemented 
        public event Action DamageTaken;
        private bool flashing;
        protected float InvulnerabilityTime = 0.5f;

        public float Health
        {
            get { return this.health; }
            set
            {
                if (value <= 0)
                {
                    this.Kill();
                }
                else
                {
                    if (!Invulnerable)
                    {
                        this.health = value;
                        this.DamageTaken();
                    }
                }
                
            }
        }

        public Color DamagedColor = new Color(255, 0, 0);
        public Color NormalColor = new Color(255,255,255);
        public int MaxHealth;
        public float Energy;
        public float Stamina;
        public bool Invulnerable;

        public Weapon Weapon;
        private float health;

        public void Awake()
        {
            transform.tag = "Character";
            DamageTaken += () => StartCoroutine(GoInvulnerable());
        }
        public void Attack()
        {
            Weapon.Attack();
        }
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public virtual void TakeDamage(float damage)
        {
            this.Health -= damage;
            StartCoroutine(FlashRed());

        }

        protected IEnumerator DamageIndicator(float damage)
        {
            GameObject textObject = new GameObject(this.name + "Damage Text");
            GUIText text = textObject.AddComponent<GUIText>();
            text.text = damage.ToString();
            text.font = GameGlobals.DamageFont;
            text.material.color = damage > 0 ? new Color(255, 0, 0) : new Color(0, 255, 0);
            yield return new WaitForSeconds(0.3f);
            Destroy(text);
        }

        protected IEnumerator FlashRed()
        {
                gameObject.renderer.material.color = DamagedColor;
                yield return new WaitForSeconds(InvulnerabilityTime*2);
                gameObject.renderer.material.color = NormalColor;
        }
        public IEnumerator GoInvulnerable()
        {
            this.Invulnerable = true;
            yield return new WaitForSeconds(InvulnerabilityTime);
            this.Invulnerable = false;
        }
    }
}
