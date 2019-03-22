using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip
{
    void Equip(AttributeSys attributeSys, GameValuesSys valuesSys);
    void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys);
}

public interface IStateEquip:IEquip
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
        valuesSys.attackSpeed += value;
    }

    public void UnEquip(AttributeSys attributeSys, GameValuesSys valuesSys)
    {
        valuesSys.attackSpeed -= value;
    }
}

public class EquipmentsFactory
{
    public IEquip SpawnProduct(ValuesType equipType,int value)
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
                return new e_A
            default:
                Debug.LogError("error");
                return null;
        }
    }
}