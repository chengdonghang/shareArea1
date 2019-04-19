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
        public virtual void ReleaseAnimEvent1(GameObject hero) { }
        public virtual void ReleaseAnimEvent2(GameObject hero) { }
        public virtual void ReleaseAnimEvent3(GameObject hero) { }
        public virtual void SkillOver(GameObject hero) { }


        #region
        protected int lastValue1;
        protected int lastValue2;
        protected float lastValueF1;
        protected float lastValueF2;
        #endregion

        /// <summary>
        /// 获得英雄的属性系统（力量敏捷相关）
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        protected AttributeSys GetAttributeSys(GameObject hero)
        {
            return hero.GetComponent<AttributeSys>();
        }

        protected GameValuesSys GetValuesSys(GameObject hero)
        {
            return hero.GetComponent<GameValuesSys>();
        }

        public enum HEIGHT
        {
            low,
            mid,
            high
        }

        protected void SpawnSkillObjectForward(GameObject hero,GameObject spawn,HEIGHT height = HEIGHT.low)
        {
            if(height == HEIGHT.low)
            {
                var obj = hero.transform.Find("SpawnLow");
                Instantiate(spawn, obj.position, obj.rotation);
            }
        }

    }
}

