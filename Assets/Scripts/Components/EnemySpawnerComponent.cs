using Unity.Entities;
using Unity.Mathematics;

namespace DOTS2D
{
    public struct EnemySpawnerComponent : IComponentData
    {
        public float spawnCooldown;
        public float2 cameraSize;
    }
}
