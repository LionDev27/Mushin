using UnityEngine;

namespace Mushin.Scripts.Player
{
    public class DashShadow : MonoBehaviour,IPoolable
    {
        private string _poolTag;
        public void Recycle()
        {
            ObjectPooler.Instance.ReturnToPool(_poolTag,gameObject);
        }

        public void SetTag(string poolTag)
        {
            _poolTag = poolTag;
        }
    }
}
