using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Value1Changed : UnityEvent<float> { }

public class GameValuesSys : MonoBehaviour {
    public int ID { get; set; }
    public int healthLimit = 100;
    public int magicLimit = 100;
    /// <summary>
    /// 当前生命魔法值
    /// </summary>
    public int HP = 100;
    public int MP = 100;

    public Value1Changed HpChanged = new Value1Changed();
    public Value1Changed MpChanged = new Value1Changed();

    public float physicalResistance = 1.0f;
    public float magicResistance = 1.0f;
    public int attackValue = 10;
    public int attackSpeed = 10;
    /// <summary>
    /// 法术强度
    /// </summary>
    public float spellPower = 1.0f;
    public float CritRate = 1.0f;
    public float dodgeRate = 0.0f;
    public int hpRestoreSpeed = 1;
    public int mpRestoreSpeed = 1;
    public int CritcalMult = 1;

    private WaitForSeconds restoreDelta = new WaitForSeconds(1);

    void Awake()
    {
        
    }

    private IEnumerator Restore()
    {
        RestoreValue(ref HP, hpRestoreSpeed, healthLimit);
        RestoreValue(ref MP, mpRestoreSpeed, magicLimit);
        yield return restoreDelta;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">当前值</param>
    /// <param name="restore">回复值</param>
    /// <param name="limit">上限值</param>
    private void RestoreValue(ref int value,int restore,int limit)
    {
        if (value < limit)
        {
            if (value + restore <= limit) value += restore;
            else value = limit;
        }
    }

    void HpHasChanged()
    {
        if (HP > healthLimit) HP = healthLimit;
        else if (HP < 0) HP = 0;
        HpChanged.Invoke((float)HP / healthLimit);
    }

    void MpHasChanged()
    {
        if (MP > magicLimit) MP = magicLimit;
        else if (MP < 0) MP = 0;
        MpChanged.Invoke((float)MP / magicLimit);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns>返回false代表闪避成功</returns>
    public bool HurtByPhysical(int value)
    {
        if (dodgeRate >= Random.value) return false;
        HP -= Mathf.FloorToInt(value * physicalResistance);
        HpHasChanged();
        return true;
    }

    public void HurtByMagic(int value)
    {
        HP -= Mathf.FloorToInt(value * magicResistance);
        HpHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">为增加的生命值</param>
    public void ChangeHP(int value)
    {
        HP += value;
        HpHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">为增加的魔法值</param>
    public void ChangeMP(int value)
    {
        MP += value;
        MpHasChanged();
    }

    public int givePhyDamage()
    {
        if(CritRate >= Random.value)
        {
            return CritcalMult * attackValue;
        }
        return attackValue;
    }

    public int giveMagicDamage(int baseValue) { return Mathf.FloorToInt(baseValue * spellPower); }
}
