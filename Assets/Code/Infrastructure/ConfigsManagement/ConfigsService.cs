using System;
using System.Collections.Generic;
using Code.Gameplay.Battle;
using Code.Gameplay.Battle.Configs;
using Code.Gameplay.Characters.Enemies;
using Code.Gameplay.Characters.Enemies.Configs;
using Code.Gameplay.Characters.Heroes.Configs;
using Code.Gameplay.PickUps;
using Code.Gameplay.PickUps.Configs;
using Code.Infrastructure.AssetManagement;

namespace Code.Infrastructure.ConfigsManagement
{
	public class ConfigsService : IConfigsService
	{
		private readonly IAssetsService _assets;

		private Dictionary<EnemyId, EnemyConfig> _enemiesById;
		private Dictionary<PickUpId, PickUpConfig> _pickupsById;
        private Dictionary<BattleId, BattleConfig> _battleById;
        
		public HeroConfig HeroConfig { get; private set; }

		public ConfigsService(IAssetsService assets)
		{
			_assets = assets;
		}
		
		public void Load()
		{
            HeroConfig = _assets.LoadAssetFromResources<HeroConfig>("Configs/HeroConfig");

            _enemiesById = LoadToDictionary<EnemyId, EnemyConfig>("Configs/Enemies", x => x.Id);
            _pickupsById = LoadToDictionary<PickUpId, PickUpConfig>("Configs/PickUps", x => x.Id);
            _battleById = LoadToDictionary<BattleId, BattleConfig>("Configs/Battles", x => x.Id);
        }

        public EnemyConfig GetEnemyConfig(EnemyId id) => GetConfig(_enemiesById, id);

        public PickUpConfig GetPickUpConfig(PickUpId id) => GetConfig(_pickupsById, id);

        public BattleConfig GetBattleConfig(BattleId id) => GetConfig(_battleById, id);

        private Dictionary<TKey, TValue> LoadToDictionary<TKey, TValue>(string path, Func<TValue, TKey> keySelector) where TValue : UnityEngine.Object
        {
            TValue[] assets = _assets.LoadAssetsFromResources<TValue>(path);

            var dict = new Dictionary<TKey, TValue>(assets.Length);

            foreach (TValue asset in assets)
            {
                dict.Add(keySelector(asset), asset);
            }

            return dict;
        }

        private TValue GetConfig<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey id)
        {
            if (dictionary.TryGetValue(id, out TValue config))
                return config;

            throw new KeyNotFoundException($"{typeof(TValue).Name} with id {id} not found");
        }
	}
}