using System;
using System.Collections.Generic;
using UnityEngine.Events;


public interface ModelInterface
{
    event Action<float> HpChanged;
    event Action<float> MpChanged;
    event Action<int,string> SkillChanged;
    event Action<int> LevelChanged;
    event Action<float> ExperienceChanged;
    event Action<EquipmentType, string> EquipChanged;
    event Action<int, int, string> PackageChanged;
    event Action AttributeChanged;
    void SetSkill(int slotNumber, string skillID);
    void SetEquip(EquipmentType EquipType, string equipID);
    void SetPackage(int row, int line, string equipID);
}

