using UnityEngine;

namespace Game.Scripts.Features.Platform
{
    public class PlatformPool: ObjectPool
    {
        public PlatformPool(GameObject parent, int capacity) : base(parent, capacity)
        {
        }
    }
}