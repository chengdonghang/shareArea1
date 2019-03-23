using System;
using System.Collections.Generic;
using UnityEngine.UI;

public interface ControlInterface
{
    void TabBtnClick(Button button);
    void PackageBtnClick(Button button,int row,int line);
    void EquipBtnClick(Button button,EquipmentType type);
}

