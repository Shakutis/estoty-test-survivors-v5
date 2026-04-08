using Code.Gameplay.Characters.Enemies.Services;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Infrastructure.UIManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
	public class HudWindow : WindowBase
	{
		[SerializeField] private Slider _healthBar;
        [SerializeField] private Slider _experienceBar;
		[SerializeField] private Text _killedEnemiesText;
		
		private IHeroProvider _heroProvider;
		private IEnemyDeathTracker _enemyDeathTracker;

		public override bool IsUserCanClose => false;

		[Inject]
		private void Construct(IHeroProvider heroProvider, IEnemyDeathTracker enemyDeathTracker)
		{
			_enemyDeathTracker = enemyDeathTracker;
			_heroProvider = heroProvider;
		}

        protected override void OnOpen()
        {
            _heroProvider.Health.OnHealthChanged += HandleHealthChanged;
            _heroProvider.Experience.OnExperienceChanged += HandleExperienceChanged;
			_enemyDeathTracker.OnEnemyDied += HandleEnemyDied;

            UpdateHealthBar();
            UpdateExperienceBar();
            UpdateKilledEnemiesText();
        }

        protected override void OnClose()
        {
            _heroProvider.Health.OnHealthChanged -= HandleHealthChanged;
            _heroProvider.Experience.OnExperienceChanged -= HandleExperienceChanged;
            _enemyDeathTracker.OnEnemyDied -= HandleEnemyDied;
        }

		private void HandleHealthChanged(float change) => UpdateHealthBar();
        private void HandleExperienceChanged(int currentXp) => UpdateExperienceBar();
        private void HandleEnemyDied() => UpdateKilledEnemiesText();

        private void UpdateHealthBar()
        {
            _healthBar.value = _heroProvider.Health.CurrentHealth / _heroProvider.Health.MaxHealth;
        }

        private void UpdateExperienceBar()
        {
            _experienceBar.value = (float)_heroProvider.Experience.CurrentExperience / _heroProvider.Experience.ExperienceToLevelUp;
        }

        private void UpdateKilledEnemiesText()
		{
			_killedEnemiesText.text = _enemyDeathTracker.TotalKilledEnemies.ToString();
		}
	}
}