using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public class DecorateBase : Equipment
    {
        public Equipment baseE;

        public new void AddEquips()
        {
            EquipmentsFactory factory = new EquipmentsFactory();
            foreach (var v in spawns)
            {
                Equips.Add(factory.SpawnProduct(v.equipType, v.value));
            }

            baseE.AddEquips();
            foreach (var v in baseE.spawns)
            {
                Equips.AddRange(baseE.Equips);
            }
            return;
        }
    }
}





