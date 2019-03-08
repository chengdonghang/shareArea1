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
        using(FileStream f = new FileStream(Application.dataPath+ "shareArea1/Resource/Excel/equip.xlsx",FileMode.Open,FileAccess.Read))
        {
            using(ExcelPackage pack = new ExcelPackage(f))
            {
                var sheets = pack.Workbook.Worksheets;
                ExcelWorksheet sheet = sheets[0];
                int i;
                i = 1;
                while (sheet.Cells[i, 0].Text.Length!=0)
                {
                    var itemID = sheet.Cells[i, 0].Text;
                    var equip = AssetDatabase.LoadAssetAtPath<Equipment>(ScriptObjectCreator.pathBase + "EquipAsset/" + itemID + ".asset");
                    if (equip != null)
                    {
                        equip.ID = itemID;
                        equip.name = sheet.Cells[i, 1].Text;
                        for(int j = 2; j <= 9; j++)
                        {
                            int value = int.Parse(sheet.Cells[i, j].Text);
                            equip.spawns.Add(new Equipment.SpawnEquip((EquipType)(j - 2), value));
                        }
                    }
                    else
                    {
                        equip = CreateScriptableObj();
                        equip.ID = itemID;
                        equip.name = sheet.Cells[i, 1].Text;
                        for (int j = 2; j <= 9; j++)
                        {
                            int value = int.Parse(sheet.Cells[i, j].Text);
                            equip.spawns.Add(new Equipment.SpawnEquip((EquipType)(j - 2), value));
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
        var sheet = package.Workbook.Worksheets.Add("Sheet1");
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
