using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;

namespace DOTS2D
{

    //[BurstCompile]
    //public partial struct BulletMovementJob : IJobEntity
    //{
    //    public float deltaTime;

    //    public void Execute(ref LocalTransform transform, ref BulletComponent bulletComponent)
    //    {
    //        bulletComponent.lifespan -= deltaTime;
    //        transform.Position += bulletComponent.speed * deltaTime * transform.Right();
    //    }
    //}


    //public partial struct BulletMovementSystem : ISystem
    //{
    //    [BurstCompile]
    //    public void OnUpdate(ref SystemState state)
    //    {
    //        var bulletMovementJob = new BulletMovementJob
    //        {
    //            deltaTime = SystemAPI.Time.DeltaTime
    //        };

    //        bulletMovementJob.ScheduleParallel();
    //    }
    //}
}
