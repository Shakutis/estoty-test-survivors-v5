using Code.Gameplay.Characters.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Battle.Configs
{
    [CreateAssetMenu(fileName = "BattleConfig", menuName = Constants.GameName + "/Configs/Battle")]
    public class BattleConfig : ScriptableObject
    {
        public BattleId Id;

        public List<EnemyId> AllowedEnemies;

        [Min(0.1f)] public float SpawnInterval = 3.0f;
        [Min(0.0f)] public float FirstSpawnDelay = 0.1f;
        [Min(0.0f)] public float SpawnDistanceGap = 0.5f;
    }
}
