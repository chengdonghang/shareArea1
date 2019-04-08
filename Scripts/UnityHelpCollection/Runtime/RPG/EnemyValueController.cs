using UnityEngine;
using System.Collections;

namespace Rpg
{
    [RequireComponent(typeof(GameValuesSys), typeof(PlayMakerFSM))]
    public class EnemyValueController : MonoBehaviour
    {
        private GameValuesSys valuesSys;
        private AttributeSys attributeSys;
        private PlayMakerFSM fsm;

        private GameValuesSys playValuesSys;

        private void Awake()
        {
            valuesSys = GetComponent<GameValuesSys>();
            attributeSys = GetComponent<AttributeSys>();
            playValuesSys = GameObject.FindWithTag("Player").GetComponent<GameValuesSys>();
            fsm = GetComponent<PlayMakerFSM>();
        }

        private void Update()
        {
            if (valuesSys.HP <= 0)
                fsm.SendEvent("DEAD");
        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log(collider.tag);
            if (collider.tag == "Weapon")
            {
                var his = playValuesSys;
                var damage = his.givePhyDamage();
                valuesSys.HurtByPhysical(damage);
                fsm.SendEvent("HURT");
            }
            else if (collider.tag == "PlayerSpell")
            {
                var his = collider.GetComponent<SpellValue>();
                var damage = his.owner.giveMagicDamage(his.magicDamage);
                valuesSys.HurtByMagic(damage);
                fsm.SendEvent("HURT");
            }
        }
    }
}
