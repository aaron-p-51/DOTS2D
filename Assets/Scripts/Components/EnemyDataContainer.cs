using System.Collections.Generic;
using Unity.Entities;

namespace DOTS2D
{
    // Same info and EnemySO class. We need it in a struct
    public struct EnemyData
    {
        public int level;
        public Entity prefab;
        public float health;
        public float damage;
        public float moveSpeed;
    }

    // Note: Must be a class and not a struct because we are using a list
    public class EnemyDataContainer : IComponentData
    {
        public List<EnemyData> enemies;
    }
}
