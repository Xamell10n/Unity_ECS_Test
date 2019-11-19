using System;
using Unity.Entities;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct ViewData : IComponentData
    {
        public GameObject View;
    }
    
    public class ViewComponent : ComponentDataProxy<ViewData> { }
}