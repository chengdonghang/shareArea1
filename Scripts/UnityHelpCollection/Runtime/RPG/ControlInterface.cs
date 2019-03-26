using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Rpg
{
    public interface ControlInterface
    {
        void TabBtnClick(Button button);
        void PackageBtnClick(Button button, int row, int line);
        void EquipBtnClick(Button button, EquipmentType type);
        void AttributeBtnClick(ValuesType type, bool isPlus);
    }

    public enum SwitchTab
    {
        attribute,
        package,
        mission,
        skill,
        shop,
        drawPrize,
        settle
    }
}



