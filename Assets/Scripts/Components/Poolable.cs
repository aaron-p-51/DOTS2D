using Unity.Entities;

namespace DOTS2D
{
    public struct Poolable : IComponentData
    {
        public bool IsActive;
    }
}
