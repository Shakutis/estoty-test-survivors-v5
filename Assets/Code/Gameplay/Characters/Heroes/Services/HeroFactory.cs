using Code.Gameplay.Characters.Heroes.Behaviours;
using Code.Gameplay.Characters.Heroes.Configs;
using Code.Gameplay.Guns.Behaviours;
using Code.Gameplay.Identification.Behaviours;
using Code.Gameplay.Leveling.Behaviours;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using Code.Infrastructure.ConfigsManagement;
using Code.Infrastructure.Identification;
using Code.Infrastructure.Instantiation;
using UnityEngine;

namespace Code.Gameplay.Characters.Heroes.Services
{
	public class HeroFactory : IHeroFactory
	{
		private readonly IConfigsService _configsService;
		private readonly IInstantiateService _instantiateService;
		private readonly IIdentifierService _identifiers;
		private readonly IHeroProvider _heroProvider;

		public HeroFactory(
			IConfigsService configsService, 
			IHeroProvider heroProvider, 
			IInstantiateService instantiateService, 
			IIdentifierService identifiers)
		{
			_configsService = configsService;
			_heroProvider = heroProvider;
			_instantiateService = instantiateService;
			_identifiers = identifiers;
		}
		
		public Hero CreateHero(Vector3 at, Quaternion rotation)
		{
			HeroConfig heroConfig = _configsService.HeroConfig;
			Hero hero = _instantiateService.InstantiatePrefabForComponent(heroConfig.Prefab, at, rotation);
			Stats stats = hero.GetComponent<Stats>();
			Health health = hero.GetComponent<Health>();
			Experience experience = hero.GetComponent<Experience>();

			hero.GetComponent<Id>()
				.Setup(_identifiers.Next());

            stats
                .SetBaseStat(StatType.MaxHealth, heroConfig.Health)
				.SetBaseStat(StatType.MovementSpeed, heroConfig.MovementSpeed)
				.SetBaseStat(StatType.ProjectileSpeed, heroConfig.ProjectileSpeed)
				.SetBaseStat(StatType.VisionRange, heroConfig.VisionRange)
				.SetBaseStat(StatType.ShootCooldown, heroConfig.ShootCooldown)
				.SetBaseStat(StatType.Damage, heroConfig.Damage);

            health.Setup();
			
			hero.GetComponent<GunOwner>().OwnedGun
				.GetComponent<Stats>()
				.SetBaseStat(StatType.RotationSpeed, heroConfig.GunRotationSpeed);
			
			_heroProvider.SetHero(hero, stats, health, experience);
			
			return hero;
		}
	}
}