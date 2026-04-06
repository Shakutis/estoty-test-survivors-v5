using Code.Gameplay.UnitStats;
using System;
using UnityEngine;

namespace Code.Gameplay.Battle.Configs
{
    [Serializable]
    public class DifficultyGrowthModifiers
    {
        public StatType StatType;

        [Min(0.0f)] public float GrowthPerMinute;
    }
}
