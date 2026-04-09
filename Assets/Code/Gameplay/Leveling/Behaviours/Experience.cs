using System;
using UnityEngine;

namespace Code.Gameplay.Leveling.Behaviours
{
    public class Experience : MonoBehaviour
    {
        [field: SerializeField] public int CurrentExperience { get; private set; }

        [SerializeField, Min(1)] private int _experienceToLevelUp = 10;
        public int ExperienceToLevelUp => _experienceToLevelUp;

        public event Action<int> OnExperienceChanged;
        public event Action OnLevelUp;

        public void AddExperience(int amount)
        {
            CurrentExperience += amount;
            OnExperienceChanged?.Invoke(CurrentExperience);

            if (CurrentExperience >= ExperienceToLevelUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            CurrentExperience -= ExperienceToLevelUp;
            OnLevelUp?.Invoke();
        }
    }
}
