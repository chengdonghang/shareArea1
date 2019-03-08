using UnityEngine;
using UnityEditor;
using System;


public class ScriptObjectCreator
{
    /// <summary>
    /// resource文件夹路径
    /// </summary>
    public static string pathBase = "Assets/shareArea1/Resource/";
    [MenuItem("Create/Equipment")]
    static void DoIt()
    {
        Debug.LogWarning("happen");
        foreach (var v in Enum.GetNames(typeof(EquipType)))
            Debug.Log(v);
            
    }

    public static Equipment CreateEquipAsset()
    {
        var v =ScriptableObject.CreateInstance<DecorateBase>();
        AssetDatabase.CreateAsset(v, pathBase+"EquipAsset/test.asset");
        AssetDatabase.Refresh();
        return v;
    }
}
