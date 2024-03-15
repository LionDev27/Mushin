using UnityEngine;

namespace Mushin.Scripts.Events
{
    public class PlayerKilledEventData : EventData
    {
        public readonly Vector3 Position; //Info que se necesite pasar en el evento

        public PlayerKilledEventData(Vector3 position) : base(EventIds.PLAYERKILLED)
        {
            Position = position;
        }
    }
}