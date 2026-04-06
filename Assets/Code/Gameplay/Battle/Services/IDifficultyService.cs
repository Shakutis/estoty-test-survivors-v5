using Code.Gameplay.Battle.Configs;
using Code.Gameplay.UnitStats.Behaviours;
using System.Collections.Generic;

namespace Code.Gameplay.Battle.Services
{
    public interface IDifficultyService
    {
        void StartTrackingDifficulty(BattleId battleId);

        void StopTrackingDifficulty();

        void ApplyModifiers(Stats stats, List<DifficultyGrowthModifiers> growthModifiers);
    }
}
