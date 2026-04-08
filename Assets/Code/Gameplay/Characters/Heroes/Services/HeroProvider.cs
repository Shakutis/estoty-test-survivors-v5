using Code.Gameplay.Characters.Heroes.Behaviours;
using Code.Gameplay.Leveling.Behaviours;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats.Behaviours;

namespace Code.Gameplay.Characters.Heroes.Services
{
	public class HeroProvider : IHeroProvider
	{
		public Hero Hero { get; private set; }
		public Health Health { get; private set; }
		public Stats Stats { get; private set; }
		public Experience Experience { get; private set; }
		
		public void SetHero(Hero hero, Stats stats, Health health, Experience experience)
		{
			Hero = hero;
			Health = health;
			Stats = stats;
			Experience = experience;
		}
	}
}