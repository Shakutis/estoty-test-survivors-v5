using Code.Gameplay.Battle;
using Code.Gameplay.Cameras.Services;
using Code.Gameplay.Characters.Enemies.Services;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Infrastructure.Instantiation;
using Code.Infrastructure.UIManagement;
using Code.UI;
using System;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class BattleInitializer : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private BattleId _battleId;
        [SerializeField] private Camera _mainCamera;
		[SerializeField] private Transform _uiRoot;
		
		private ICameraProvider _cameraProvider;
		private IHeroFactory _heroFactory;
		private IInstantiator _instantiator;
		private IInstantiateService _instantiateService;
		private IUIRootProvider _uiRootProvider;
		private IUiService _uiService;
		private IEnemySpawner _enemySpawner;
        private IHeroProvider _heroProvider;

        [Inject]
		private void Construct(
			ICameraProvider cameraProvider,
			IHeroFactory heroFactory,
			IInstantiateService instantiateService,
			IInstantiator instantiator,
			IUIRootProvider uiRootProvider,
			IUiService uiService,
			IEnemySpawner enemySpawner,
            IHeroProvider heroProvider
        )
		{
			_uiService = uiService;
			_uiRootProvider = uiRootProvider;
			_instantiateService = instantiateService;
			_instantiator = instantiator;
			_heroFactory = heroFactory;
			_cameraProvider = cameraProvider;
            _enemySpawner = enemySpawner;
            _heroProvider = heroProvider;
        }
    
		public void Initialize()
		{
			_cameraProvider.SetMainCamera(_mainCamera);
			_instantiateService.SetInstantiator(_instantiator);
			_uiRootProvider.SetUiRoot(_uiRoot);
			
			_heroFactory.CreateHero(Vector3.zero, Quaternion.identity);
            _enemySpawner.StartSpawning(_battleId);

			SubscribeToOnHeroDeath();

            _uiService.OpenWindow<HudWindow>();
		}

        public void Dispose()
        {
            if (_heroProvider?.Health != null)
                _heroProvider.Health.OnDeath -= _enemySpawner.StopSpawning;
        }

		private void SubscribeToOnHeroDeath()
		{
            if (_heroProvider.Health != null)
			{
                _heroProvider.Health.OnDeath += _enemySpawner.StopSpawning;
            }
			else
			{
                throw new MissingComponentException(
					$"[BattleInitializer] The Hero was created, but no {nameof(Health)} component was found.");
            }
        }
    }
}