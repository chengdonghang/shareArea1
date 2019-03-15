using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    HeroManager manager;

    void Start()
    {
        manager = GetComponent<HeroManager>();
        manager.valuesSys.HurtByPhysical(20);
        manager.SetPackage(0, 0, "1");
        manager.SetEquip(EquipmentType.weapon, "1");
    }

}
