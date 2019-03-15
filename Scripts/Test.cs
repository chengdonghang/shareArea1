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
        manager.SetPackage(1, 1, "1");
    }

}
