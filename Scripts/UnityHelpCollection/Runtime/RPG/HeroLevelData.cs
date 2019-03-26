using UnityEngine;
using System.Collections;

namespace Rpg
{
    public class HeroLevelData : ScriptableObject
    {
        public int maxLevel = 10;
        public int upGetAttrPoint = 10;
        public int[] levelReachExperience = new int[10];
    }
}


