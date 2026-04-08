using Code.Gameplay.Characters.Heroes.Behaviours;
using Code.Gameplay.Leveling.Behaviours;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats.Behaviours;

namespace Code.Gameplay.Characters.Heroes.Services
{
	public interface IHeroProvider
	{
		Hero Hero { get; }
		Health Health { get; }
		Stats Stats { get; }
        Experience Experience { get; }
        void SetHero(Hero hero, Stats stats, Health health, Experience experience);
	}
}