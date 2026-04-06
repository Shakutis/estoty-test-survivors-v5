using Code.Gameplay.Battle.Configs;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Battle.Services
{
    public class DifficultyService : IDifficultyService, ITickable, IDisposable
    {
        private float _elapsedSeconds;
        private bool _isActive;

        public void StartTrackingDifficulty(BattleId battleId)
        {
            _elapsedSeconds = 0.0f;
            _isActive = true;
        }

        public void StopTrackingDifficulty()
        {
            _isActive = false;
        }

        public void Tick()
        {
            if (!_isActive) return;

            _elapsedSeconds += Time.deltaTime;
        }

        public void Dispose()
        {
            StopTrackingDifficulty();
        }

        public void ApplyModifiers(Stats stats, List<DifficultyGrowthModifiers> growthModifiers)
        {
            int elapsedMinutes = (int)(_elapsedSeconds / 60f);

            if (elapsedMinutes == 0) return;

            foreach (DifficultyGrowthModifiers modifier in growthModifiers)
            {
                stats.AddStatModifier(new StatModifier(modifier.StatType, modifier.GrowthPerMinute * elapsedMinutes));
            }
        }
    }
}
