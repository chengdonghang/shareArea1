using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour,ControlInterface
{
    private bool HeroPanelIsOn = false;
    public enum SwitchTab
    {
        attribute,
        package,
        mission,
        skill
    }
    SwitchTab nowTab = SwitchTab.attribute;

    public UIManager uiManager;
    public ModelInterface model;

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
                    uiManager.HeroPanel.SetActive(false);
                    break;
                }
            case "人物头像":
                {
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
        throw new System.NotImplementedException();
    }

    public void EquipBtnClick(Button button, EquipmentType type)
    {
        throw new System.NotImplementedException();
    }
}
