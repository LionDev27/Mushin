using UnityEngine;

namespace Mushin.Scripts.Events
{
    public class EnemyKilledEventData : EventData
    {
        public readonly Vector3 Position; //Info que se necesite pasar en el evento

        public EnemyKilledEventData(Vector3 position) : base(EventIds.ENEMYKILLED)
        {
            Position = position;
        }
    }
}