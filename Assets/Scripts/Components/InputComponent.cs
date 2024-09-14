using Unity.Entities;
using Unity.Mathematics;

namespace DOTS2D
{
    public struct InputComponent : IComponentData
    {
        public float2 movement;
        public float2 mousePos;
        public bool pressingLMB;
    }
}
