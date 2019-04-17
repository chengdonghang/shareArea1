using UnityEngine;
using System.Collections;
using Rpg;

[RequireComponent(typeof(GameValuesSys),typeof(AttributeSys))]
public class HeroCollisionController : MonoBehaviour
{
    private GameValuesSys valuesSys;
    private AttributeSys attributeSys;
    private HeroValueModel valueModel;

    private void Awake()
    {
        valuesSys = GetComponent<GameValuesSys>();
        attributeSys = GetComponent<AttributeSys>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Enemy") 
        {
            var his = collision.GetComponentInParent<GameValuesSys>();
            var damage = his.givePhyDamage();
            valuesSys.HurtByPhysical(damage);
        }
        else if(collision.tag == "EnemySpell") 
        {
            var his = collision.GetComponent<SpellValue>();
            var damage = his.owner.giveMagicDamage(his.magicDamage);
            valuesSys.HurtByMagic(damage);
        }
    }
}
