using Code.Gameplay.Battle.Configs;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Characters.Enemies.Configs
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = Constants.GameName + "/Configs/Enemy")]
	public class EnemyConfig : ScriptableObject
	{
		public EnemyId Id;

		public Behaviours.Enemy Prefab;

		public float Health = 100f;
		public float MovementSpeed = 5f;
		public float Damage = 10f;

		public List<DifficultyGrowthModifiers> DifficultyGrowthModifiers;

		public void ApplyStats(Stats stats)
		{
			stats
				.SetBaseStat(StatType.MaxHealth, Health)
				.SetBaseStat(StatType.MovementSpeed, MovementSpeed)
				.SetBaseStat(StatType.Damage, Damage);
        }
    }
}