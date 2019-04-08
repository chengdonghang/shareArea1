using UnityEngine;
using System.Collections;
using Rpg;

[RequireComponent(typeof(GameValuesSys),typeof(AttributeSys))]
public class NewMonoBehaviour : MonoBehaviour
{
    private GameValuesSys valuesSys;
    private AttributeSys attributeSys;

    private void Awake()
    {
        valuesSys = GetComponent<GameValuesSys>();
        attributeSys = GetComponent<AttributeSys>();
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy") 
        {
            var his = collision.collider.GetComponent<GameValuesSys>();
            var damage = his.givePhyDamage();
            valuesSys.HurtByPhysical(damage);
        }
        else if(collision.collider.tag == "EnemySpell") 
        {
            var his = collision.collider.GetComponent<SpellValue>();
            var damage = his.owner.giveMagicDamage(his.magicDamage);
            valuesSys.HurtByMagic(damage);
        }
    }
}
