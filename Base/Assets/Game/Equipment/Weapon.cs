namespace Assets.Game.Equipment
{
    using System.Collections;
    using Characters;
    using UnityEngine;
    public abstract class Weapon : MonoBehaviour
    {
        #region Base Variables
        protected float Damage = 10f;
        protected float Cooldown = 0.0f;
        protected float Duration = 0.5f;
        protected bool OnCooldown;
        public WeaponType WeaponType { get; set; }

        #endregion

        #region Abstract Methods
        public abstract void Attack();
        protected abstract void HitEffects(Collider target, GameObject go);
        #endregion

        #region Helper Methods
        protected virtual IEnumerator Heat()
        {
            this.OnCooldown = true;
            yield return new WaitForSeconds(this.Cooldown);
            this.OnCooldown = false;
        }

        protected void ApplyDamage(Character person)
        {
            person.Health -= this.Damage;
        }
        protected void AddHitEffects(GameObject projectile)
        {
            Equipment.HitEffects projectileEffect = projectile.AddComponent<HitEffects>();
            projectileEffect.HitActions += this.HitEffects;
        }
        #endregion
    }
    public enum WeaponType
    {
        Sword,
        Hammer,
        Dagger,
        Bow,
        Shotgun
    }
}
