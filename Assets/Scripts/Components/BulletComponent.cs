using Unity.Entities;
using Unity.Mathematics;

namespace DOTS2D
{
    public struct BulletComponent : IComponentData
    {
        public float3 velocity;
        public float speed;
        public float size;
        public float lifespan;
    }
}
