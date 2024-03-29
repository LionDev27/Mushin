using System;
using UnityEngine;

namespace Mushin.Scripts.Player
{
    public abstract class Player:MonoBehaviour
    {
        public PlayerStats CurrentStats { get; set; }

        public Action<int> OnDashesUpdated;
        public Action<int,int> OnXpUpdated;
        public Action<int> OnLevelUp;
        public Action<UpgradeData> OnUpgradeApplied;
        public Action<int> OnLevelReset; 

        //Inputs
        public abstract void OnMoveInput(Vector2 value);
        public abstract void OnAttackInput(Vector2 dir);
        public abstract void OnDashInput();
        public abstract void OnSkill1Input();
        public abstract void OnSkill2Input();

        //Events
        public abstract void OnStatsUpdated();
        public abstract void OnIsDashing(bool dashing);
        public abstract void OnHealOrbCollected(float amount);
        public abstract void OnXPOrbCollected(int xpToAdd);
        public abstract void OnTakeDamage(float damage);
        public abstract void OnMaxHealth();
        public abstract void OnShouldAddLife();
        public abstract void OnDashUpgrade();
        public abstract void OnStartLevelingUp();
        public abstract void ResetPlayer();
    }
}