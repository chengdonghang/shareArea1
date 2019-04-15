using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rpg;

public class HeroItemExamer : MonoBehaviour
{
    public HeroValueModel model;

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("开始");
        if (collider.tag == "Item"&&Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("开始拾取");
            var com = collider.GetComponent<ItemContainer>();
            if (com.isBloodVial) model.AddBloodOrMagicVial(true);
            else if (com.isMagicVial) model.AddBloodOrMagicVial(false);
            else
            {
                int row = 0, line =0;
                model.NextFreeSlot(ref row, ref line);
                model.SetPackage(row, line,com.equipment.ID);
            }
            Destroy(collider.gameObject);
        }
    }
}
