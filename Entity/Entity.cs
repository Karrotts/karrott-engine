using System;
using System.Collections;
using UnityEngine;
namespace KarrottEngine.EntitySystem
{
    public enum EntityType {
        TILE,
        PLAYER,
        ENEMY,
        ENVIRONMENT,
        UNKNOWN
    }

    [Serializable]
    public class Entity
    {
        public GameObject EntityObject { get; }
        public EntityType Type { get; }
        public int SpecialType { get; set; }
        public Entity(GameObject entityObject, EntityType Type)
        {
            this.EntityObject = entityObject;
            this.Type = Type;
        }
    }
}