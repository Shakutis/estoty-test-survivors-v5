using System;
using UnityEngine;

namespace Code.Gameplay.Leveling.Behaviours
{
   public class Experience : MonoBehaviour
    {
        [SerializeField] private int _currentExperience;
        [Min(1)][SerializeField] private int _experienceToLevelUp;

        public event Action<int> OnExperienceChanged;
        public event Action OnLevelUp;

        public void AddExperience(int amount)
        {
            _currentExperience += amount;
            OnExperienceChanged?.Invoke(_currentExperience);

            if (_currentExperience >= _experienceToLevelUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _currentExperience -= _experienceToLevelUp;
            OnLevelUp?.Invoke();

            Debug.Log("Level Up!");
        }
    }
}
