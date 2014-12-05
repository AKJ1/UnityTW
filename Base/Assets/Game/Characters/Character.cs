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
        public GameObject deathAnimation;
        public GameObject bleedAnimation;
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
                        this.DamageTaken();   
                        this.health = value;
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
        protected abstract void Attack();

        public void Kill()
        {
            this.Invulnerable = true;
            Instantiate(deathAnimation, (transform.position - transform.forward), Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
        }

        public virtual void TakeDamage(float damage)
        {
            if (!Invulnerable)
            {
                this.Health -= damage;
                GameObject blood = (GameObject)Instantiate(bleedAnimation, transform.position, Quaternion.Euler(0, 0, 0));
                blood.transform.parent = transform;
                StartCoroutine(FlashRed());
            }
        }

        public virtual void TakeMinorDamage(float damage)
        {
            if (!Invulnerable)
            {
                this.health -= damage;
                if (this.health <= 0)
                {
                    Kill();
                }
                GameObject blood = (GameObject)Instantiate(bleedAnimation, transform.position, Quaternion.Euler(0, 0, 0));
                blood.transform.parent = transform;
                StartCoroutine(FlashRed());

            }
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
                yield return new WaitForSeconds(0.2f);
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
