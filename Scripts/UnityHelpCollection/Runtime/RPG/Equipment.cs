using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Rpg
{
    public class Equipment : ScriptableObject, IEquipment
    {
        public string ID { get { return id; } set { id = value; } }
        public string Name { get { return e_Name; } set { e_Name = value; } }
        [SerializeField] private string id;
        [SerializeField] private string e_Name;
        public EquipmentType equipmentType;

        public List<IEquip> Equips = new List<IEquip>();

        public List<IEquip> GetEquipments() { return Equips; }

        public List<SpawnEquip> spawns = new List<SpawnEquip>();
        [Serializable]
        public class SpawnEquip
        {
            public ValuesType equipType;
            public int value;
            public SpawnEquip(ValuesType type, int value)
            {
                this.equipType = type;
                this.value = value;
            }
        }

        public bool IsFirstInstantiate()
        {
            if (Equips.Count == 0) return true;
            else return false;
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

        public void Equip(AttributeSys sys1, GameValuesSys sys2)
        {
            foreach (var v in Equips)
            {
                v.Equip(sys1, sys2);
            }
        }

        public void UnEquip(AttributeSys sys1, GameValuesSys sys2)
        {
            foreach (var v in Equips)
            {
                v.UnEquip(sys1, sys2);
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (var v in Equips) { s += v.ToString() + "\t"; }
            return s;
        }
    }
}


