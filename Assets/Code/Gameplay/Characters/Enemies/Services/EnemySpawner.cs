using Code.Common.Extensions;
using Code.Gameplay.Battle;
using Code.Gameplay.Cameras.Services;
using Code.Gameplay.Characters.Heroes.Behaviours;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Infrastructure.ConfigsManagement;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Characters.Enemies.Services
{
	public class EnemySpawner : IEnemySpawner, ITickable, IDisposable
    {
		private readonly ICameraProvider _cameraProvider;
		private readonly IHeroProvider _heroProvider;
		private readonly IEnemyFactory _enemyFactory;
        private readonly IConfigsService _configsService;

        private EnemyId[] _spawnPool;
        private bool _isActive;
        private float _timer;

        private float _spawnInterval;
		private float _firstSpawnDelay;
        private float _spawnDistanceGap;
        private bool _isFirstSpawn;

        private static readonly Vector2[] _horizontalDirections = { Vector2.left, Vector2.right };
        private static readonly Vector2[] _verticalDirections = { Vector2.up, Vector2.down };

        public EnemySpawner(ICameraProvider cameraProvider, IHeroProvider heroProvider, IEnemyFactory enemyFactory, IConfigsService configsService)
        {
            _heroProvider = heroProvider;
            _cameraProvider = cameraProvider;
            _enemyFactory = enemyFactory;
            _configsService = configsService;
        }

		public void Dispose()
		{
            StopSpawning();
		}

		public void StartSpawning(BattleId battleId)
		{
            var config = _configsService.GetBattleConfig(battleId);

            _spawnPool = config.AllowedEnemies.ToArray();
			_spawnInterval = config.SpawnInterval;
			_firstSpawnDelay = config.FirstSpawnDelay;
			_spawnDistanceGap = config.SpawnDistanceGap;

            _timer = 0.0f;
            _isFirstSpawn = true;
            _isActive = true;
        }

		public void StopSpawning()
		{
            _isActive = false;
            _spawnPool = null;
        }

        public void Tick()
        {
			if (!_isActive || _spawnPool == null || _spawnPool.Length == 0)
				return;

            _timer += Time.deltaTime;

            UpdateSpawning();
        }

		private void UpdateSpawning()
		{
			float currentThreshold = _isFirstSpawn ? _firstSpawnDelay : _spawnInterval;

			while (_timer >= currentThreshold)
			{
                Hero hero = _heroProvider.Hero;

				if (hero == null) break;

                SpawnEnemy(hero);
                ResetOrSubtractTimer();

                currentThreshold = _spawnInterval;
            }
		}

		private void SpawnEnemy(Hero hero)
		{
            Vector2 randomSpawnPosition = RandomSpawnPosition(hero.transform.position);
            EnemyId randomId = _spawnPool.PickRandom();

            _enemyFactory.CreateEnemy(randomId, at: randomSpawnPosition, Quaternion.identity);
        }

		private void ResetOrSubtractTimer()
		{
            if (_isFirstSpawn)
            {
                _timer -= _firstSpawnDelay;
                _isFirstSpawn = false;
            }
            else
            {
                _timer -= _spawnInterval;
            }
        }

		private Vector2 RandomSpawnPosition(Vector2 heroWorldPosition)
		{
			bool startWithHorizontal = Random.Range(0, 2) == 0;

			return startWithHorizontal 
				? HorizontalSpawnPosition(heroWorldPosition) 
				: VerticalSpawnPosition(heroWorldPosition);
		}

		private Vector2 HorizontalSpawnPosition(Vector2 heroWorldPosition)
		{
			Vector2 primaryDirection = _horizontalDirections.PickRandom();
      
			float horizontalOffsetDistance = _cameraProvider.WorldScreenWidth / 2 + _spawnDistanceGap;
			float verticalRandomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

			return heroWorldPosition + primaryDirection * horizontalOffsetDistance + Vector2.up * verticalRandomOffset;
		}

		private Vector2 VerticalSpawnPosition(Vector2 heroWorldPosition)
		{
			Vector2 primaryDirection = _verticalDirections.PickRandom();
      
			float verticalOffsetDistance = _cameraProvider.WorldScreenHeight / 2 + _spawnDistanceGap;
			float horizontalRandomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

			return heroWorldPosition + primaryDirection * verticalOffsetDistance + Vector2.right * horizontalRandomOffset;
		}
    }
}