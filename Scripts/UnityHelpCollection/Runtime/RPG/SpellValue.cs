using UnityEngine;
using System.Collections;

namespace Rpg
{
    public class SpellValue : MonoBehaviour
    {
        public GameValuesSys owner;
        public int magicDamage = 0;

        private void Update()
        {
            if (!owner) Debug.LogError("法术没有Owner");
        }
    }
}

