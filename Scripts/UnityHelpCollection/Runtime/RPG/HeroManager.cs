using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour,ModelInterface
{
    public event Action<float> HpChanged;
    public event Action<float> MpChanged;
    public event Action<int, string> SkillChanged;
    public event Action<int> LevelChanged;
    public event Action<float> ExperienceChanged;
    public event Action<EquipmentType, string> EquipChanged;
    public event Action<int, int, string> PackageChanged;
    public GameValuesSys valuesSys;

    private Equipment[,] packages = new Equipment[8, 8];
    private Dictionary<EquipmentType, Equipment> equips = new Dictionary<EquipmentType, Equipment>();

    void Awake()
    {
        valuesSys.HpChanged.AddListener(delegate (float value) { HpChanged(value); });
        valuesSys.MpChanged.AddListener(delegate (float value) { MpChanged(value); });

        //初始化字典
        foreach(EquipmentType v in Enum.GetValues(typeof(EquipmentType)))
        {
            equips[v] = null;
        }
    }

    public void SetEquip(EquipmentType EquipType, string equipID)
    {
        if (equipID == "-1")
        {
            equips[EquipType] = null;
            EquipChanged(EquipType, equipID);
        }

        var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);
        if (s != null)
        {
            equips[EquipType] = s;
            EquipChanged(EquipType, equipID);
        }
    }

    public void SetPackage(int row, int line, string equipID)
    {
        if (equipID == "-1")
        {
            packages[row, line] = null;
            PackageChanged(row, line, equipID);
        }

        var s = Resources.Load<Equipment>(Path.respDataEquip+equipID);
        Debug.Log(Path.respDataEquip + equipID );
        Debug.Log(s);
        if (s != null)
        {
            packages[row, line] = s;
            PackageChanged(row, line, equipID);
        }
    }

    public void SetSkill(int slotNumber, string skillID)
    {
        throw new NotImplementedException();
    }

    
}
