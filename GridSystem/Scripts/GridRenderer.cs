using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KarrottEngine.EntitySystem;
namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class GridRenderer : MonoBehaviour
    {
        public Entity RenderEntity() 
        {
            return new Entity(new GameObject());
        }
    }   
}
