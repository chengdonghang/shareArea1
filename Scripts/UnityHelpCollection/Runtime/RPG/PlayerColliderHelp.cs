using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public abstract class PlayerColliderHelp:MonoBehaviour
    {
        public string WeaponName = "Weapon";
        public string SpellName = "PlayerSpell";
        public string PlayerName = "Player";

        protected abstract void WeaponEnter(Collider collider);
        protected abstract void PlayerEnter(Collider collider);
        protected abstract void SpellEnter(Collider collider);

        protected void _OnTriggerEnter(Collider collider)
        {
            if (collider.tag == WeaponName)
                WeaponEnter(collider);
            else if (collider.tag == SpellName)
                SpellEnter(collider);
            else if (collider.tag == PlayerName)
                PlayerEnter(collider);
        }

    }
}
