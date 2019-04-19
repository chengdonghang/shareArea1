using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rpg
{
    public class FireAttack : Skills
    {
        public GameObject[] spawnParticles = new GameObject[1];

        public override void ReleaseAnimEvent1(GameObject hero)
        {
            SpawnSkillObjectForward(hero, spawnParticles[0]);
        }

        public override void ReleaseNow(GameObject hero)
        {

        }
    }
}

