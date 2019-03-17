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

    public UIController uIController;
    public ModelInterface model;

    private void SwitchTo(SwitchTab tab)
    {
        if (nowTab != tab)
        {

        }
    }

    public void TabBtnClick(Button button)
    {
        switch (button.name)
        {
            case "属性按钮":

        }
    }

    public void PackageBtnClick(Button button)
    {
        throw new System.NotImplementedException();
    }

    public void EquipBtnClick(Button button)
    {
        throw new System.NotImplementedException();
    }
}
