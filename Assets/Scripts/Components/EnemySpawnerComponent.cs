using Unity.Entities;

namespace DOTS2D
{
    public struct EnemySpawnerComponent : IComponentData
    {
        public float spawnCooldown;
    }
}
