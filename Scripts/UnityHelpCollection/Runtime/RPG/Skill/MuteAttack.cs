using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rpg
{
    public class MuteAttack : Skills
    {
        GameValuesSys sys;
        float lastValue;

        public override void SkillOver(GameObject hero)
        {
            sys.CritRate = lastValue;
        }

        public override void ReleaseNow(GameObject hero)
        {
            sys = GetValuesSys(hero);
            lastValue = sys.CritRate;
            sys.CritRate = 1.0f;
        }
    }
}

