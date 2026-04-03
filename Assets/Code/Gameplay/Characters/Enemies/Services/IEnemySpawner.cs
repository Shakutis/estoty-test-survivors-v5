using Code.Gameplay.Battle;

namespace Code.Gameplay.Characters.Enemies.Services
{
    public interface IEnemySpawner
    {
        void StartSpawning(BattleId battleId);
        void StopSpawning();
    }
}