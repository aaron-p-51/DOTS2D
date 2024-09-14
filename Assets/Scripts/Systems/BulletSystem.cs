using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DOTS2D
{
    [BurstCompile]
    public partial struct BulletSystem : ISystem
    {
        [BurstCompile]
        private void OnUpdate(ref SystemState state) 
        {
            foreach (var (bulletComponent, bulletLocalTransform) in SystemAPI.Query<RefRO<BulletComponent>, RefRW<LocalTransform>>())
            {
                bulletLocalTransform.ValueRW.Position += bulletComponent.ValueRO.speed * SystemAPI.Time.DeltaTime * bulletLocalTransform.ValueRW.Right();
            }
        }
    }
}
