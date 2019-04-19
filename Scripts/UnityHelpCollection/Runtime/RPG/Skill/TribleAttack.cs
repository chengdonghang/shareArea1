using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rpg
{
    public class TribleAttack : Skills
    {
        public override void ReleaseNow(GameObject hero)
        {
            var attr = GetAttributeSys(hero);
            var value = GetValuesSys(hero);
            lastValue1 = value.attackValue;
            value.attackValue += attr.strength * 2;
        }

        public override void SkillOver(GameObject hero)
        {
            var value = GetValuesSys(hero);
            value.attackValue += lastValue1;
        }
    }
}

