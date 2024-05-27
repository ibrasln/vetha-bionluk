using System.Collections.Generic;
using UnityEngine;

namespace Mission.Earth
{
    [CreateAssetMenu(fileName = "PointsData_", menuName = "Data/Points Data")]
    public class PointsData : ScriptableObject
    {
        public List<Vector2> Points;
    }
}