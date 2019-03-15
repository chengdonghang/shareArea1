using UnityEngine;
using UnityEditor;
using System;

public class ScriptObjectCreator
{

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
        AssetDatabase.CreateAsset(v, m_Path.pDataEquip+"test.asset");
        AssetDatabase.Refresh();
        return v;
    }
}
