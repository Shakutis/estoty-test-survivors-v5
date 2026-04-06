using Code.Gameplay.Battle.Services;
using Code.Gameplay.Characters.Enemies.Behaviours;
using Code.Gameplay.Characters.Enemies.Configs;
using Code.Gameplay.Identification.Behaviours;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats.Behaviours;
using Code.Infrastructure.ConfigsManagement;
using Code.Infrastructure.Identification;
using Code.Infrastructure.Instantiation;
using UnityEngine;

namespace Code.Gameplay.Characters.Enemies.Services
{
	public class EnemyFactory : IEnemyFactory
	{
		private readonly IConfigsService _configsService;
		private readonly IInstantiateService _instantiateService;
		private readonly IIdentifierService _identifiers;
		private readonly IDifficultyService _difficultyService;

		public EnemyFactory(
			IConfigsService configsService, 
			IInstantiateService instantiateService,
			IIdentifierService identifiers,
			IDifficultyService difficultyService)
		{
			_configsService = configsService;
			_instantiateService = instantiateService;
			_identifiers = identifiers;
            _difficultyService = difficultyService;
		}
		
		public Enemy CreateEnemy(EnemyId id, Vector3 at, Quaternion rotation)
		{
			EnemyConfig enemyConfig = _configsService.GetEnemyConfig(id);
			Enemy enemy = _instantiateService.InstantiatePrefabForComponent(enemyConfig.Prefab, at, rotation);
            Stats stats = enemy.GetComponent<Stats>();
            
			enemy.GetComponent<Id>()
				.Setup(_identifiers.Next());

			enemyConfig.ApplyStats(stats);

			_difficultyService.ApplyModifiers(stats, enemyConfig.DifficultyGrowthModifiers);

            enemy.GetComponent<Health>().Setup();
			
			return enemy;
		}
	}
}