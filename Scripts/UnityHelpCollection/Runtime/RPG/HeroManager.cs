using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    [RequireComponent(typeof(GameValuesSys), typeof(AttributeSys))]
    public class HeroManager : MonoBehaviour, ModelInterface
    {
        public event Action<float> HpChanged;
        public event Action<float> MpChanged;
        public event Action<int, string> SkillChanged;
        public event Action<int> LevelChanged;
        public event Action<float> ExperienceChanged;
        public event Action<EquipmentType, string> EquipChanged;
        public event Action<int, int, string> PackageChanged;
        public event Action AttributeChanged;

        public GameValuesSys valuesSys;
        public AttributeSys attrSys;

        private Equipment[,] packages = new Equipment[8, 8];
        private Dictionary<EquipmentType, Equipment> equips = new Dictionary<EquipmentType, Equipment>();

        public HeroLevelData heroLevelData;
        private int nowLevel = 1;
        private int nowExperience = 0;
        public Dictionary<ValuesType, int> addAttrPointDic = new Dictionary<ValuesType, int>();
        private int leftAttrPoint = 0; //人物属性点
        public int LeftAttrPoint { get { return leftAttrPoint; } }

        void Awake()
        {
            valuesSys = GetComponent<GameValuesSys>();
            attrSys = GetComponent<AttributeSys>();

            addAttrPointDic[ValuesType.strength] = 0;
            addAttrPointDic[ValuesType.agile] = 0;
            addAttrPointDic[ValuesType.intellgence] = 0;
            addAttrPointDic[ValuesType.lucky] = 0;
            addAttrPointDic[ValuesType.physique] = 0;
            //初始化字典
            foreach (EquipmentType v in Enum.GetValues(typeof(EquipmentType)))
            {
                equips[v] = null;
            }
        }

        void Start()
        {
            valuesSys.HpChanged.AddListener(delegate (float value) { HpChanged(value); });
            valuesSys.MpChanged.AddListener(delegate (float value) { MpChanged(value); });

            nowLevel = 1;
            nowExperience = 0;
            ExperienceChanged(0);
            LevelChanged(1);
        }

        public void AddExperience(int add)
        {
            var rea = heroLevelData.levelReachExperience[nowLevel];
            if (add + nowExperience >= rea)
            {
                nowExperience = add + nowExperience - rea;
                nowLevel += 1;
                leftAttrPoint += heroLevelData.upGetAttrPoint;
                AttributeChanged();
                LevelChanged(nowLevel);
                ExperienceChanged(nowExperience);
            }
            else
            {
                nowExperience += add;
                ExperienceChanged(nowExperience);
            }
        }


        /// <summary>
        /// 设置装备栏
        /// </summary>
        /// <returns>换掉物品的ID</returns>
        /// <param name="EquipType">Equip type.</param>
        /// <param name="equipID">Equip identifier.</param>
        public string SetEquip(EquipmentType EquipType, string equipID)
        {
            var ret1 = equips[EquipType];
            if (ret1)
                Debug.Log(ret1.ToString());
            if (equipID == "-1" && equips[EquipType] != null)
            {
                var ret = equips[EquipType];
                ret.UnEquip(attrSys, valuesSys);
                equips[EquipType] = null;
                EquipChanged(EquipType, equipID);
                if (!ret) { return "-1"; } else { return ret.ID; }
            }
            else if (equipID != "-1" && equips[EquipType] != null)
            {
                var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);
                if (s != null)
                {
                    var ret = equips[EquipType];
                    ret.UnEquip(attrSys, valuesSys);
                    equips[EquipType] = s;
                    equips[EquipType].Equip(attrSys, valuesSys);
                    EquipChanged(EquipType, equipID);
                    return ret.ID;
                }
                return "-1";
            }
            else if (equipID != "-1" && equips[EquipType] == null)
            {
                var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);
                if (s != null)
                {
                    equips[EquipType] = s;
                    equips[EquipType].Equip(attrSys, valuesSys);
                    EquipChanged(EquipType, equipID);
                }
                return "-1";
            }
            else return "-1";
        }

        public void NextFreeSlot(ref int row, ref int line)
        {
            for (int i = 0; i < packages.GetLength(0); i++)
            {
                for (int j = 0; j < packages.GetLength(1); j++)
                {
                    if (packages[i, j] == null) { row = i; line = j; return; }
                }
            }
        }

        /// <summary>
        /// 设置背包物品
        /// </summary>
        /// <returns>换掉物品的ID</returns>
        /// <param name="row">Row.</param>
        /// <param name="line">Line.</param>
        /// <param name="equipID">Equip identifier.</param>
        public string SetPackage(int row, int line, string equipID)
        {
            if (equipID == "-1")
            {
                var ret = packages[row, line];
                packages[row, line] = null;
                PackageChanged(row, line, equipID);
                if (!ret) { return "-1"; } else { return ret.ID; }
            }

            var s = Resources.Load<Equipment>(Path.respDataEquip + equipID);

            if (s != null)
            {
                if (s.IsFirstInstantiate())
                    s.AddEquips();
                var ret = packages[row, line];
                packages[row, line] = s;
                PackageChanged(row, line, equipID);
                if (!ret) { return "-1"; } else { return ret.ID; }
            }
            return "-1";
        }

        public Equipment GetEquips(EquipmentType type)
        {
            return equips[type];
        }

        public Equipment GetPackage(int row, int line)
        {
            return packages[row, line];
        }

        public void SetSkill(int slotNumber, string skillID)
        {
            if (slotNumber < 0 || slotNumber > 5) { Debug.LogError("index error"); return; }
            if (skillID == "-1") { }
        }

        public bool AddAttribtePoint(ValuesType valuesType)
        {
            if (leftAttrPoint <= 0) return false;
            addAttrPointDic[valuesType] += 1;
            switch (valuesType)
            {
                case ValuesType.strength:
                    leftAttrPoint -= 1;
                    attrSys.AddStrength(1);
                    return true;
                case ValuesType.agile:
                    leftAttrPoint -= 1;
                    attrSys.AddAgile(1);
                    return true;
                case ValuesType.intellgence:
                    leftAttrPoint -= 1;
                    attrSys.AddIntelligence(1);
                    return true;
                case ValuesType.physique:
                    leftAttrPoint -= 1;
                    attrSys.AddPhysique(1);
                    return true;
                case ValuesType.lucky:
                    leftAttrPoint -= 1;
                    attrSys.AddLucky(1);
                    return true;
                default:
                    Debug.LogError("error");
                    return false;
            }
        }

        /// <summary>
        /// 这个函数具有一个Bug，就是可以使用减去物品的属性，以后可以尝试维护
        /// </summary>
        /// <returns><c>true</c>, if attribte point was minused, <c>false</c> otherwise.</returns>
        /// <param name="valuesType">Values type.</param>
        public bool MinusAttribtePoint(ValuesType valuesType)
        {
            if (addAttrPointDic[valuesType] == 0)
            {
                return false;
            }
            addAttrPointDic[valuesType] -= 1;
            switch (valuesType)
            {
                case ValuesType.strength:
                    leftAttrPoint += 1;
                    attrSys.AddStrength(-1);
                    return true;
                case ValuesType.agile:
                    leftAttrPoint += 1;
                    attrSys.AddAgile(-1);
                    return true;
                case ValuesType.intellgence:
                    leftAttrPoint += 1;
                    attrSys.AddIntelligence(-1);
                    return true;
                case ValuesType.physique:
                    leftAttrPoint += 1;
                    attrSys.AddPhysique(-1);
                    return true;
                case ValuesType.lucky:
                    leftAttrPoint += 1;
                    attrSys.AddLucky(-1);
                    return true;
                default:
                    Debug.LogError("error");
                    return false;
            }
        }
    }

}

