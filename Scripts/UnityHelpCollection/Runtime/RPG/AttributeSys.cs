﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GameValuesSys))]
public class AttributeSys:MonoBehaviour
{
    public int ID { get; set; }
    public int strength { get; private set; }
    public int agile { get; private set; }
    public int intelligence { get; private set; }
    public int physique { get; private set; }
    public int lucky { get; private set; }
    public float[] implicatedValues = new float[11];
    private GameValuesSys valuesSys;
    public Action valueChanged;

    private void Awake()
    {
        for (int i = 0; i < implicatedValues.Length; i++) { implicatedValues[i]=1.0f; }
    }

    void Start()
    {
        valuesSys = GetComponent<GameValuesSys>();
    }

    public void AddStrength(int value)
    {
        strength += value;
        valuesSys.attackValue += (int)(value * implicatedValues[0]);
        valueChanged();
    }

    public void AddAgile(int value)
    {
        agile += value;
        valuesSys.physicalResistance += (value * implicatedValues[1]);
        valuesSys.attackSpeed += (int)(value * implicatedValues[2]);
    }

    public void AddIntelligence(int value)
    {
        intelligence += value;
        valuesSys.magicResistance += (value * implicatedValues[3]);
        valuesSys.spellPower += (value * implicatedValues[4]);
        valuesSys.mpRestoreSpeed += (int)(value * implicatedValues[5]);
        valuesSys.magicLimit += (int)(value * implicatedValues[6]);
    }

    public void AddPhysique(int value)
    {
        physique += value;
        valuesSys.healthLimit += (int)(value * implicatedValues[7]);
        valuesSys.hpRestoreSpeed += (int)(value * implicatedValues[8]);
    }

    public void AddLucky(int value)
    {
        lucky += value;
        valuesSys.CritRate += (value * implicatedValues[9]);
        valuesSys.dodgeRate += (value * implicatedValues[10]);
    }



}
