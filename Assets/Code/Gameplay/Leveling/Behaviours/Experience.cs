using System;
using UnityEngine;

namespace Code.Gameplay.Leveling.Behaviours
{
    public class Experience : MonoBehaviour
    {
        [field: SerializeField] public int CurrentExperience { get; private set; }
        [field: SerializeField, Min(1)] public int ExperienceToLevelUp { get; private set; } = 10;

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
