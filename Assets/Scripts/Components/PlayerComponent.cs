using Unity.Entities;

namespace DOTS2D
{
    public struct PlayerComponent : IComponentData
    {
        public float velocity;
        public float moveSpeed;
        public float shootCooldown;
        public Entity bulletPrefab;
    }
}
