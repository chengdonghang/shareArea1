using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using OfficeOpenXml;

public class ExcelLoader
{
    [MenuItem("Excel/Equipment")]
    static void LoadEquip()
    {
        using(FileStream f = new FileStream(Application.dataPath+ "/shareArea1/Resource/Excel/equip.xlsx",FileMode.Open,FileAccess.Read))
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
                    var equip = AssetDatabase.LoadAssetAtPath<Equipment>(ScriptObjectCreator.pathBase + "EquipAsset/" + itemID + ".asset");
                    if (equip != null)
                    {
                        equip.ID = itemID;
                        equip.name = sheet.Cells[i, 2].Text;
                        for(int j = 3; j < 3 + Enum.GetNames(typeof(EquipType)).Length; j++)
                        {
                            int value = int.Parse(sheet.Cells[i, j].Text);
                            if(value!=0)
                                equip.spawns.Add(new Equipment.SpawnEquip((EquipType)(j - 3), value));
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
                    }
                    SaveObjData(equip, equip.ID, "/EquipAsset/");
                    i++;
                }
            }
        }

    }



    [MenuItem("Excel/CreateSheet")]
    static void CreateSheet()
    {
        FileInfo f = new FileInfo(Application.dataPath + "/shareArea1/Resource/Excel/equip.xlsx");
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
        AssetDatabase.CreateAsset(obj, ScriptObjectCreator.pathBase + path + name + ".asset");
        AssetDatabase.Refresh();
    }

}
