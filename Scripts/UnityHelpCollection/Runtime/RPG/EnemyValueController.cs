using UnityEngine;
using System.Collections;

namespace Rpg
{
    [RequireComponent(typeof(GameValuesSys), typeof(PlayMakerFSM))]
    public class EnemyValueController : PlayerColliderHelp
    {
        public int experience = 10;
        private GameValuesSys valuesSys;
        private AttributeSys attributeSys;
        private PlayMakerFSM fsm;
        protected GameObject hero;

        private HeroValueModel playModel;
        private GameValuesSys playValuesSys;
        private bool _OnlySendOnce = false;
        private Animation animator;

        private void Awake()
        {
            valuesSys = GetComponent<GameValuesSys>();
            attributeSys = GetComponent<AttributeSys>();
            playModel = GameObject.FindWithTag("Player").GetComponent<HeroValueModel>();
            playValuesSys = GameObject.FindWithTag("Player").GetComponent<GameValuesSys>();
            fsm = GetComponent<PlayMakerFSM>();
            animator = GetComponent<Animation>();
        }

        private void Update()
        {
            IsDead();
        }

        void IsDead()
        {
            if (valuesSys.HP <= 0 && !_OnlySendOnce)
            {
                fsm.SetState("Dead");
                _OnlySendOnce = true;
                playModel.SendMessage("AddExperience", experience);
                animator.Play("death");
                Destroy(this.gameObject, 1.5f);
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log(collider.name);
            _OnTriggerEnter(collider);
        }

        protected override void WeaponEnter(Collider collider)
        {
            var his = playValuesSys;
            var damage = his.givePhyDamage();
            valuesSys.HurtByPhysical(damage);
            fsm.SendEvent("HURT");
        }

        protected override void PlayerEnter(Collider collider)
        {
            
        }

        protected override void SpellEnter(Collider collider)
        {
            var his = collider.GetComponent<SpellValue>();
            var damage = his.owner.giveMagicDamage(his.magicDamage);
            valuesSys.HurtByMagic(damage);
            fsm.SendEvent("HURT");
        }
    }
}
