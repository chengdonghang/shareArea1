﻿using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Value1Changed : UnityEvent<float> { }

public class GameValuesSys : MonoBehaviour {
    public int ID { get; set; }
    public int HealthLimit { get { return healthLimit; } set { healthLimit = value; valueChanged(); } }
    public int healthLimit = 100;
    public int MagicLimit { get { return magicLimit; } set { magicLimit = value; valueChanged(); } }
    public int magicLimit = 100;
    /// <summary>
    /// 当前生命魔法值
    /// </summary>
    public int HP = 100;
    public int MP = 100;

    public float PhysicalResistance { get { return physicalResistance; } set { physicalResistance = value; valueChanged(); } }
    public float physicalResistance = 1.0f;
    public float MagicResistance { get { return magicResistance; } set { magicResistance = value; valueChanged(); } }
    public float magicResistance = 1.0f;
    public int AttackValue { get { return attackValue; } set { attackValue = value; valueChanged(); } }
    public int attackValue = 10;
    public int AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; valueChanged(); } }
    public int attackSpeed = 10;
    /// <summary>
    /// 法术强度
    /// </summary>
    public float spellPower = 1.0f;
    public float SpellPower { get { return spellPower; } set { spellPower = value; valueChanged(); } }
    public float CritRate { get { return critRate; } set { critRate = value; valueChanged(); } }
    public float critRate = 1.0f;
    public float DodgeRate { get { return dodgeRate; } set { dodgeRate = value; valueChanged(); } }
    public float dodgeRate = 0.0f;
    public int HpRestoreSpeed { get { return hpRestoreSpeed; } set { hpRestoreSpeed = value; valueChanged(); } }
    public int hpRestoreSpeed = 1;
    public int MpRestoreSpeed { get { return MpRestoreSpeed; } set { MpRestoreSpeed = value; valueChanged(); } }
    public int mpRestoreSpeed = 1;
    public int CritcalMult = 1;

    private WaitForSeconds restoreDelta = new WaitForSeconds(1);

    public Value1Changed HpChanged = new Value1Changed();
    public Value1Changed MpChanged = new Value1Changed();
    [HideInInspector]public UnityEvent Critcaled = new UnityEvent();
    [HideInInspector]public UnityEvent Dodged = new UnityEvent();
    public event Action valueChanged;

    private void Start()
    {
        Debug.Log("run");
        StartCoroutine(Restore());
    }

    private IEnumerator Restore()
    {
        while (true)
        {
            yield return restoreDelta;
            ChangeHP(hpRestoreSpeed);
            ChangeMP(mpRestoreSpeed);
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
        if (dodgeRate >= UnityEngine.Random.value) { Dodged.Invoke(); return false; }
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
        if(critRate >= UnityEngine.Random.value)
        {
            Critcaled.Invoke();
            return CritcalMult * attackValue;
        }
        return attackValue;
    }

    public int giveMagicDamage(int baseValue) { return Mathf.FloorToInt(baseValue * spellPower); }
}
