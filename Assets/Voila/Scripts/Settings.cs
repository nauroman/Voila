using UnityEngine;

namespace Voila.Scripts
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Voila/Settings")]
    public class Settings : ScriptableObject
    {
        public int maxObjects = 20;
        public float interval = 1;
        public Vector3 positionBounds = new Vector3(10, 0, 10);
    }
}
