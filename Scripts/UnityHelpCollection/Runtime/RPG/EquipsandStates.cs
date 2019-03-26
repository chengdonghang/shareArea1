using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg
{
    public interface IEquip
    {
        void Equip(AttributeSys attributeSys, GameValuesSys valuesSys);
        void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys);
    }

    public interface IStateEquip : IEquip
    {
        void Equip();
        void UnEquip();
    }

    public interface IState
    {
        void Enter(AttributeSys attributeSys, GameValuesSys valuesSys);
        void Update(AttributeSys attributeSys, GameValuesSys valuesSys);
        void Exit(AttributeSys attributeSys, GameValuesSys valuesSys);
    }

    /// <summary>
    /// 属性类型
    /// </summary>
    public enum ValuesType
    {
        health,
        magic,
        physicRes,
        magicRes,
        attackValue,
        attackSpeed,
        spellPower,
        critcalRate,
        dodgeRate,
        hpRestoreSpeed,
        mpRestoreSpeed,
        strength,
        agile,
        intellgence,
        lucky,
        physique
    }

    /// <summary>
    /// 装备类型
    /// </summary>
    public enum EquipmentType
    {
        weapon,
        /// <summary>
        /// 头盔
        /// </summary>
        helm,
        /// <summary>
        /// 手套
        /// </summary>
        glove,
        clothes,
        pants,
        shoes,
        /// <summary>
        /// 腰带
        /// </summary>
        belt,
        /// <summary>
        /// 饰品
        /// </summary>
        decorater
    }

    public enum WeaponType
    {
        doubleSword,
        glove,
        /// <summary>
        /// 匕首
        /// </summary>
        dagger,
        gun,
        /// <summary>
        /// 弓
        /// </summary>
        bow,
        magicBall,
        /// <summary>
        /// 法杖
        /// </summary>
        wand
    }

    public interface IEquipment
    {
        void AddEquips();
        List<IEquip> GetEquipments();
    }

    public static class Path
    {
        public static string pathBase = "Assets/shareArea1/Resource/";
        public static string pPicture = pathBase + "Source/Photo/";
        public static string pPicEquip = pPicture + "Equip/";
        public static string pPicSkill = pPicture + "Skill/";
        public static string pData = pathBase + "Data/";
        public static string pDataEquip = pData + "Equip/";
        public static string pDataSkill = pData + "Skill/";

        public static string resPathBase = "";
        public static string respPicture = resPathBase + "Source/Photo/";
        public static string respPicEquip = respPicture + "Equip/";
        public static string respPicSkill = respPicture + "Skill/";
        public static string respData = resPathBase + "Data/";
        public static string respDataEquip = respData + "Equip/";
        public static string respDataSkill = respData + "Skill/";
    }


    public class e_Health : IEquip
    {
        private int value;
        public e_Health(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.HealthLimit += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.HealthLimit -= value;
        }
    }

    public class e_Magic : IEquip
    {
        private int value;
        public e_Magic(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MagicLimit += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MagicLimit -= value;
        }
    }

    public class e_Strength : IEquip
    {
        private int value;
        public e_Strength(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddStrength(value);
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddStrength(-value);
        }
    }

    public class e_Agile : IEquip
    {
        private int value;
        public e_Agile(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddAgile(value);
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddAgile(-value);
        }
    }

    public class e_Intelligence : IEquip
    {
        private int value;
        public e_Intelligence(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddIntelligence(value);
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddIntelligence(-value);
        }
    }

    public class e_Physicque : IEquip
    {
        private int value;
        public e_Physicque(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddPhysique(value);
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddPhysique(-value);
        }
    }

    public class e_Lucky : IEquip
    {
        private int value;
        public e_Lucky(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddLucky(value);
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            attributeSys.AddLucky(-value);
        }
    }

    public class e_AttackValue : IEquip
    {
        private int value;
        public e_AttackValue(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.AttackValue += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.AttackValue -= value;
        }
    }
    public class e_AttackSpeed : IEquip
    {
        private int value;
        public e_AttackSpeed(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.AttackSpeed += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.AttackSpeed -= value;
        }
    }
    public class e_HpRestoreSpeed : IEquip
    {
        private int value;
        public e_HpRestoreSpeed(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.HpRestoreSpeed += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.HpRestoreSpeed -= value;
        }
    }
    public class e_MpRestoreSpeed : IEquip
    {
        private int value;
        public e_MpRestoreSpeed(int value)
        {
            this.value = value;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MpRestoreSpeed += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MpRestoreSpeed -= value;
        }
    }

    public class e_PhysicRes : IEquip
    {
        private float value;
        public e_PhysicRes(int value)
        {
            if (value > 100 || value < -100) Debug.LogError("value too big or small");
            this.value = value / 100;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.PhysicalResistance += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.PhysicalResistance -= value;
        }
    }
    public class e_MagicRes : IEquip
    {
        private float value;
        public e_MagicRes(int value)
        {
            if (value > 100 || value < -100) Debug.LogError("value too big or small");
            this.value = value / 100;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MagicResistance += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.MagicResistance -= value;
        }
    }
    public class e_SpellPower : IEquip
    {
        private float value;
        public e_SpellPower(int value)
        {
            if (value > 100 || value < -100) Debug.LogError("value too big or small");
            this.value = value / 100;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.SpellPower += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.SpellPower -= value;
        }
    }
    public class e_CritcalRate : IEquip
    {
        private float value;
        public e_CritcalRate(int value)
        {
            if (value > 100 || value < -100) Debug.LogError("value too big or small");
            this.value = value / 100;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.CritRate += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.CritRate -= value;
        }
    }
    public class e_DodgeRate : IEquip
    {
        private float value;
        public e_DodgeRate(int value)
        {
            if (value > 100 || value < -100) Debug.LogError("value too big or small");
            this.value = value / 100;
        }

        public void Equip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.DodgeRate += value;
        }

        public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
        {
            valuesSys.DodgeRate -= value;
        }
    }


    public class EquipmentsFactory
    {
        public IEquip SpawnProduct(ValuesType equipType, int value)
        {
            switch (equipType)
            {
                case ValuesType.health:
                    return new e_Health(value);
                case ValuesType.magic:
                    return new e_Magic(value);
                case ValuesType.agile:
                    return new e_Agile(value);
                case ValuesType.strength:
                    return new e_Strength(value);
                case ValuesType.intellgence:
                    return new e_Intelligence(value);
                case ValuesType.physique:
                    return new e_Physicque(value);
                case ValuesType.lucky:
                    return new e_Lucky(value);
                case ValuesType.attackValue:
                    return new e_AttackValue(value);
                case ValuesType.attackSpeed:
                    return new e_AttackSpeed(value);
                case ValuesType.physicRes:
                    return new e_PhysicRes(value);
                case ValuesType.magicRes:
                    return new e_MagicRes(value);
                case ValuesType.spellPower:
                    return new e_SpellPower(value);
                case ValuesType.critcalRate:
                    return new e_CritcalRate(value);
                case ValuesType.dodgeRate:
                    return new e_DodgeRate(value);
                case ValuesType.hpRestoreSpeed:
                    return new e_HpRestoreSpeed(value);
                case ValuesType.mpRestoreSpeed:
                    return new e_MpRestoreSpeed(value);
                default:
                    Debug.LogError("error");
                    return null;
            }
        }
    }
}

