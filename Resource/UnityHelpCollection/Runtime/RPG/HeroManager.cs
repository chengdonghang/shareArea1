using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour,ModelInterface
{
    public event Action<float> HpChanged;
    public event Action<float> MpChanged;
    public event Action<int, string> SkillChanged;
    public event Action<int> LevelChanged;
    public event Action<float> ExperienceChanged;
    public event Action<int, string> EquipChanged;
    public event Action<int, int, string> PackageChanged;
    public GameValuesSys valuesSys;


    void Awake()
    {
        valuesSys.HpChanged.AddListener(delegate (float value) { HpChanged(value); });
        valuesSys.MpChanged.AddListener(delegate (float value) { MpChanged(value); });
    }

    public void SetEquip(int EquipType, string equipID)
    {
        throw new NotImplementedException();
    }

    public void SetPackage(int row, int line, string equipID)
    {
        throw new NotImplementedException();
    }

    public void SetSkill(int slotNumber, string skillID)
    {
        throw new NotImplementedException();
    }

    
}
