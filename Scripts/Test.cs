using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg;

public class Test : MonoBehaviour
{
    HeroValueModel manager;

    void Start()
    {
        manager = GetComponent<HeroValueModel>();
        manager.valuesSys.HurtByPhysical(20);
        manager.SetPackage(0, 0, "1");
        manager.SetEquip(EquipmentType.weapon, "1");
    }
}
