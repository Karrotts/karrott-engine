using UnityEngine;
namespace KarrottEngine.EntitySystem
{
    public class Entity
    {
        public GameObject EntityObject { get; }
        public Entity(GameObject entityObject)
        {
            this.EntityObject = entityObject;
        }
    }
}