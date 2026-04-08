using Code.Gameplay.Leveling.Behaviours;
using UnityEngine;

namespace Code.Gameplay.PickUps.Behaviours
{
    [RequireComponent(typeof(PickUp))]
    public class ExperienceOnPickUp : MonoBehaviour
    {
        [SerializeField] private int _experienceAmount;

        private PickUp _pickUp;

        private void Awake()
        {
            _pickUp = GetComponent<PickUp>();
        }

        private void OnEnable()
        {
            _pickUp.OnPickUp += HandlePickup;
        }

        private void OnDisable()
        {
            _pickUp.OnPickUp -= HandlePickup;
        }

        private void HandlePickup(GameObject pickUpper)
        {
            if (pickUpper.TryGetComponent(out Experience experience))
            {
                experience.AddExperience(_experienceAmount);
            }
        }
    }
}
