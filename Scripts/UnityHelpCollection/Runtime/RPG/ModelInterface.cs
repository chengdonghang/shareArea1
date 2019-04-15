using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Rpg
{
    public interface ModelInterface
    {
        event Action<float> HpChanged;
        event Action<float> MpChanged;
        event Action<int, string> SkillChanged;
        event Action<int> LevelChanged;
        event Action<float> ExperienceChanged;
        event Action<int> bloodVialChanged;
        event Action<int> magicVialChanged;
        event Action<EquipmentType, string> EquipChanged;
        event Action<int, int, string> PackageChanged;
        event Action AttributeChanged;
        event Action<int, float> skillTimeCold;
        bool AddAttribtePoint(ValuesType valuesType);
        void SetSkill(int slotNumber, string skillID);
        string SetEquip(EquipmentType EquipType, string equipID);
        string SetPackage(int row, int line, string equipID);
    }
}



