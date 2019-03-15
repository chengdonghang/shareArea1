﻿using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OfficeOpenXml;

public class ExcelLoader
{
    public static string mPath = "/shareArea1/Resource/Data/Excel/";
    [MenuItem("Excel/Equipment")]
    static void LoadEquip()
    {
        using(FileStream f = new FileStream(Application.dataPath+ mPath+"equip.xlsx",FileMode.Open,FileAccess.Read))
        {
            using(ExcelPackage pack = new ExcelPackage(f))
            {
                var sheets = pack.Workbook.Worksheets;
                ExcelWorksheet sheet = sheets[1];
                int i;
                i = 2;
                while (sheet.Cells[i, 1].Text.Length!=0)
                {
                    var itemID = sheet.Cells[i, 1].Text;
                    var equip = AssetDatabase.LoadAssetAtPath<Equipment>(m_Path.pDataEquip + itemID + ".asset");
                    if (equip != null)
                    {
                        //如果该项已经在spawns里面出现，就不添加只修改，否则需要添加该项
                        equip.ID = itemID;
                        equip.name = sheet.Cells[i, 2].Text;
                        equip.Name = sheet.Cells[i, 2].Text;
                        Dictionary<EquipType, int> dic = new Dictionary<EquipType, int>();
                        int k = 0;
                        foreach(var v in equip.spawns) { dic.Add(v.equipType, k++); }
                        for(int j = 3; j < 3 + Enum.GetNames(typeof(EquipType)).Length; j++)
                        {
                            int value = int.Parse(sheet.Cells[i, j].Text);
                            if (value != 0)
                            {
                                var key = (EquipType)(j - 3);
                                if (dic.ContainsKey(key))
                                {
                                    equip.spawns[dic[key]].value = value;
                                }
                                else
                                {
                                    equip.spawns.Add(new Equipment.SpawnEquip(key, value));
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        equip = CreateScriptableObj();
                        equip.ID = itemID;
                        equip.name = sheet.Cells[i, 2].Text;
                        for (int j = 3; j < 3 + Enum.GetNames(typeof(EquipType)).Length; j++)
                        {
                            int value = int.Parse(sheet.Cells[i, j].Text);
                            if(value!=0)
                                equip.spawns.Add(new Equipment.SpawnEquip((EquipType)(j - 3), value));
                        }
                        SaveObjData(equip, equip.ID, m_Path.pDataEquip);
                    }
                    i++;
                }
            }
        }

    }



    [MenuItem("Excel/CreateSheet")]
    static void CreateSheet()
    {
        FileInfo f = new FileInfo(Application.dataPath + mPath+"equip.xlsx");
        ExcelPackage package = new ExcelPackage(f);
        var sheet = package.Workbook.Worksheets.Add("装备属性表");
        sheet.Cells[1, 1].Value = "ID";
        sheet.Cells[1, 2].Value = "Name";
        int i = 3;
        foreach(var s in Enum.GetNames(typeof(EquipType)))
            sheet.Cells[1, i++].Value = s;
        package.Save();  
    }

    static Equipment CreateScriptableObj()
    {
        var instance = ScriptableObject.CreateInstance<Equipment>();
        return instance;
    }

    /// <summary>
    /// 保存物品信息
    /// </summary>
    /// <param name="obj">要保存的物品.</param>
    /// <param name="path">在Resource文件夹下相对路径名.</param>
    static void SaveObjData(ScriptableObject obj,string name,string path)
    {
        AssetDatabase.CreateAsset(obj, path + name + ".asset");
        AssetDatabase.Refresh();
    }

}