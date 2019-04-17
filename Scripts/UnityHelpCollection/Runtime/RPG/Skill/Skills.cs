using UnityEngine;
using Tools;

namespace Rpg
{
    public abstract class Skills : ScriptableObject
    {
        public string id = "-1";
        public int playAnim = 1;
        public float coldTime = 2.0f;

        public abstract void ReleaseNow(GameObject hero);
        public abstract void ReleaseAnimEvent1(GameObject hero);
        public abstract void ReleaseAnimEvent2(GameObject hero);
        public abstract void ReleaseAnimEvent3(GameObject hero);
        public virtual void SkillOver(GameObject hero) { }
    }
}

