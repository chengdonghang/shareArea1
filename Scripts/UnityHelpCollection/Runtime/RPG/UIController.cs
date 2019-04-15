using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rpg;

public class UIController : MonoBehaviour,ControlInterface
{
    private bool HeroPanelIsOn = false;
    private bool TalkModeIsOn = false;
    SwitchTab nowTab = SwitchTab.attribute;

    public UIManager uiManager;
    public HeroValueModel model;

    private void Awake()
    {
        if (!model)
        {
            Debug.LogWarning("找寻tag为Player的物体获取model");
            model = GameObject.FindWithTag("PLayer").GetComponent<HeroValueModel>();
        }
    }

    private void SwitchTo(SwitchTab tab)
    {
        if (nowTab != tab)
        {
            uiManager.tabPages[nowTab].SetActive(false);
            nowTab = tab;
            uiManager.tabPages[nowTab].SetActive(true);
        }
    }

    public void TabBtnClick(Button button)
    {
        Debug.Log(button.name);
        switch (button.name)
        {
            case "属性按钮":
                {
                    SwitchTo(SwitchTab.attribute);
                    break;
                }
            case "物品按钮":
                {
                    SwitchTo(SwitchTab.package);
                    break;
                }
            case "技能按钮":
                {
                    SwitchTo(SwitchTab.skill);
                    break;
                }
            case "任务按钮":
                {
                    SwitchTo(SwitchTab.mission);
                    break;
                }
            case "关闭按钮":
                {
                    Time.timeScale = 1.0f;
                    uiManager.HeroPanel.SetActive(false);
                    break;
                }
            case "人物头像":
                {
                    Time.timeScale = 0.0f;
                    uiManager.HeroPanel.SetActive(true);
                    uiManager.tabPages[nowTab].SetActive(true);
                    break;
                }
            default:
                {
                    Debug.LogError("error");
                    break;
                }
        }
    }

    public void PackageBtnClick(Button button, int row, int line)
    {
        Debug.Log("row" + row + "line" + line);
        var e = model.GetPackage(row, line);
        if (e)
        {
            var nowEquip = model.GetEquips(e.equipmentType);
            if (nowEquip) 
            { 
                model.SetEquip(e.equipmentType, e.ID); 
                model.SetPackage(row, line, nowEquip.ID); 
            }
            else
            {
                model.SetEquip(e.equipmentType, e.ID);
                model.SetPackage(row, line, "-1");
            }
        }
    }

    public void EquipBtnClick(Button button, EquipmentType type)
    {
        var e = model.GetEquips(type);
        if (e)
        {
            int row = -1, line = -1;
            model.NextFreeSlot(ref row, ref line);
            model.SetEquip(type, "-1");
            model.SetPackage(row, line, e.ID);
        }
    }

    public void AttributeBtnClick(ValuesType type,bool isPlus)
    {
        if (isPlus)
        {
            if (model.AddAttribtePoint(type))
            { uiManager.attrPointText.text = model.LeftAttrPoint.ToString(); }
            return;
        }
        else
        {
            if (model.MinusAttribtePoint(type))
            { uiManager.attrPointText.text = model.LeftAttrPoint.ToString(); }
            return;
        }
    }
}
