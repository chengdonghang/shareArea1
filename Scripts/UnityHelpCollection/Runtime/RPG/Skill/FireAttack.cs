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
            var obj = hero.transform.Find("SpawnLow");
            Instantiate(spawnParticles[0], obj.position, obj.rotation);
        }

        public override void ReleaseAnimEvent2(GameObject hero)
        {

        }

        public override void ReleaseAnimEvent3(GameObject hero)
        {

        }

        public override void ReleaseNow(GameObject hero)
        {

        }
    }
}

