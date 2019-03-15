using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Equipment : ScriptableObject,IEquipment
{
    public string ID { get { return id; } set{ id = value; } }
    public string Name { get { return e_Name; } set { e_Name = value; } }
    [SerializeField]private string id;
    [SerializeField]private string e_Name;
    public EquipmentType equipmentType;

    public List<IEquip> Equips = new List<IEquip>();

    public List<IEquip> GetEquipments() { return Equips; }

    public List<SpawnEquip> spawns = new List<SpawnEquip>();
    [Serializable]
    public class SpawnEquip
    {
        public EquipType equipType;
        public int value;
        public SpawnEquip(EquipType type,int value)
        {
            this.equipType = type;
            this.value = value;
        }
    }

    public void AddEquips()
    {
        EquipmentsFactory factory = new EquipmentsFactory();
        foreach (var v in spawns)
        {
            Equips.Add(factory.SpawnProduct(v.equipType, v.value));
        }
        return;
    }
}
