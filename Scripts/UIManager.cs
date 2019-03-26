using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rpg;
using tabType = Rpg.SwitchTab;

public class UIManager : MonoBehaviour
{
    public GameObject ButtomPanel;
    public GameObject HeroPanel;

    public Image HP;
    public Image MP;
    public Image HeroImage;
    public Slider experience;
    public Text LevelNumber;
    private Image[] skillsImage = new Image[6];
    private Button[] skillsBtn = new Button[6];
    private Image[,] packageImage = new Image[8, 8];
    private Button[,] packageBtn = new Button[8, 8];
    private Dictionary<Button, Point> packageDic = new Dictionary<Button, Point>();
    private class Point { public readonly int _x; public readonly int _y; public Point(int x, int y) { _x = x; _y = y; } }
    private Button heroAvatar;
    private Dictionary<EquipmentType, Image> equips = new Dictionary<EquipmentType, Image>();
    private Dictionary<EquipmentType, Button> equipsBtn = new Dictionary<EquipmentType, Button>();
    public Dictionary<tabType, GameObject> tabPages = new Dictionary<tabType, GameObject>();
    public Dictionary<ValuesType, Text> valuesText = new Dictionary<ValuesType, Text>();
    public Dictionary<Button, ValuesType> valuesBtnPlus = new Dictionary<Button, ValuesType>();
    public Dictionary<Button, ValuesType> valuesBtnMinus = new Dictionary<Button, ValuesType>();
    public Text attrPointText;

    public HeroManager model;
    public UIController control;


    void Start()
    {
        model.HpChanged += HpChanged;
        model.MpChanged += MpChanged;
        model.ExperienceChanged += ExperienceChanged;
        model.LevelChanged += LevelChanged;
        model.SkillChanged += SkillChanged;
        model.PackageChanged += PackageChanged;
        model.EquipChanged+=EquipChanged;

        ButtomPanel = transform.Find("底部面板").gameObject;
        HeroPanel = transform.Find("人物面板").gameObject;

        #region 初始化技能面板
        for (int i = 1; i <= 6; i++)
        {
            var val = ButtomPanel.transform.Find("skill" + i.ToString());
            skillsImage[i - 1] = val.Find("Image").GetComponent<Image>();
            skillsImage[i - 1].sprite = null;
            skillsBtn[i - 1] = val.GetComponent<Button>();
        }
        #endregion

        #region 初始化属性界面
        var father0 = HeroPanel.transform.Find("属性界面");
        model.valuesSys.valueChanged += attrValueChanged;
        model.attrSys.valueChanged += attrValueChanged;
        valuesText[ValuesType.attackValue] = father0.transform.Find("物理伤害数值").GetComponent<Text>();
        valuesText[ValuesType.attackSpeed] = father0.transform.Find("攻击速度数值").GetComponent<Text>();
        valuesText[ValuesType.health] = father0.transform.Find("生命上限数值").GetComponent<Text>();
        valuesText[ValuesType.magic] = father0.transform.Find("魔法上限数值").GetComponent<Text>();
        valuesText[ValuesType.spellPower] = father0.transform.Find("法术强度数值").GetComponent<Text>();
        valuesText[ValuesType.magicRes] = father0.transform.Find("法术抗性数值").GetComponent<Text>();
        valuesText[ValuesType.mpRestoreSpeed] = father0.transform.Find("魔法回复数值").GetComponent<Text>();
        valuesText[ValuesType.physicRes] = father0.transform.Find("物理抗性数值").GetComponent<Text>();
        valuesText[ValuesType.hpRestoreSpeed] = father0.transform.Find("生命回复数值").GetComponent<Text>();
        valuesText[ValuesType.dodgeRate] = father0.transform.Find("闪避率数值").GetComponent<Text>();
        valuesText[ValuesType.critcalRate] = father0.transform.Find("暴击率数值").GetComponent<Text>();
        valuesText[ValuesType.strength] = father0.transform.Find("力量数值").GetComponent<Text>();
        valuesText[ValuesType.agile] = father0.transform.Find("敏捷数值").GetComponent<Text>();
        valuesText[ValuesType.intellgence] = father0.transform.Find("智力数值").GetComponent<Text>();
        valuesText[ValuesType.lucky] = father0.transform.Find("幸运数值").GetComponent<Text>();
        valuesText[ValuesType.physique] = father0.transform.Find("体质数值").GetComponent<Text>();
        attrPointText = father0.transform.Find("属性点数值").GetComponent<Text>();
        attrPointText.text = model.LeftAttrPoint.ToString();

        #region 注册加点按钮
        valuesBtnPlus[father0.transform.Find("力量加点").GetComponent<Button>()] = ValuesType.strength;
        valuesBtnPlus[father0.transform.Find("敏捷加点").GetComponent<Button>()] = ValuesType.agile;
        valuesBtnPlus[father0.transform.Find("智力加点").GetComponent<Button>()] = ValuesType.intellgence;
        valuesBtnPlus[father0.transform.Find("体质加点").GetComponent<Button>()] = ValuesType.physique;
        valuesBtnPlus[father0.transform.Find("幸运加点").GetComponent<Button>()] = ValuesType.lucky;
        foreach(var v in valuesBtnPlus.Keys)
        {
            v.onClick.AddListener(delegate () { control.AttributeBtnClick(valuesBtnPlus[v],true); });
        }
        #endregion

        #region 注册减点按钮
        valuesBtnMinus[father0.transform.Find("力量减点").GetComponent<Button>()] = ValuesType.strength;
        valuesBtnMinus[father0.transform.Find("敏捷减点").GetComponent<Button>()] = ValuesType.agile;
        valuesBtnMinus[father0.transform.Find("智力减点").GetComponent<Button>()] = ValuesType.intellgence;
        valuesBtnMinus[father0.transform.Find("体质减点").GetComponent<Button>()] = ValuesType.physique;
        valuesBtnMinus[father0.transform.Find("幸运减点").GetComponent<Button>()] = ValuesType.lucky;
        foreach (var v in valuesBtnMinus.Keys)
        {
            v.onClick.AddListener(delegate () { control.AttributeBtnClick(valuesBtnMinus[v],false); });
        }
        #endregion

        #endregion

        #region 初始化装备字典
        var father1 = HeroPanel.transform.Find("物品界面").Find("装备");
        equips[EquipmentType.helm] = father1.transform.Find("头盔").Find("Image").GetComponent<Image>();
        equips[EquipmentType.pants] = father1.transform.Find("裤子").Find("Image").GetComponent<Image>();
        equips[EquipmentType.belt] = father1.transform.Find("腰带").Find("Image").GetComponent<Image>();
        equips[EquipmentType.decorater] = father1.transform.Find("饰品1").Find("Image").GetComponent<Image>();
        equips[EquipmentType.shoes] = father1.transform.Find("鞋子").Find("Image").GetComponent<Image>();
        equips[EquipmentType.clothes] = father1.transform.Find("衣服").Find("Image").GetComponent<Image>();
        equips[EquipmentType.weapon] = father1.transform.Find("武器1").Find("Image").GetComponent<Image>();
        #region 初始化装备按钮
        equipsBtn[EquipmentType.helm] = father1.transform.Find("头盔").GetComponent<Button>();
        equipsBtn[EquipmentType.pants] = father1.transform.Find("裤子").GetComponent<Button>();
        equipsBtn[EquipmentType.belt] = father1.transform.Find("腰带").GetComponent<Button>();
        equipsBtn[EquipmentType.decorater] = father1.transform.Find("饰品1").GetComponent<Button>();
        equipsBtn[EquipmentType.shoes] = father1.transform.Find("鞋子").GetComponent<Button>();
        equipsBtn[EquipmentType.clothes] = father1.transform.Find("衣服").GetComponent<Button>();
        equipsBtn[EquipmentType.weapon] = father1.transform.Find("武器1").GetComponent<Button>();
        foreach(var pair in equipsBtn)
        {
            pair.Value.onClick.AddListener(delegate
            {
                control.EquipBtnClick(pair.Value, pair.Key);
            });
        }
        #endregion
        #endregion

        #region 初始化背包
        var father2 = HeroPanel.transform.Find("物品界面").Find("背包");
        for(int i = 0;i<8;i++)
            for(int j = 0; j < 8; j++)
            {
                var ch = father2.Find("pack" + (i * 8 + (j + 1)).ToString());
                var btn = ch.GetComponent<Button>();
                packageDic[btn] = new Point(i, j);
                btn.onClick.AddListener(delegate () { control.PackageBtnClick(btn,packageDic[btn]._x,packageDic[btn]._y); });
                var pic = ch.Find("Image").GetComponent<Image>();
                packageImage[i, j] = pic;
            }
        #endregion

        #region 初始化页面物体
        tabPages[tabType.attribute] = HeroPanel.transform.Find("属性界面").gameObject;
        tabPages[tabType.mission] = HeroPanel.transform.Find("任务界面").gameObject;
        tabPages[tabType.package] = HeroPanel.transform.Find("物品界面").gameObject;
        tabPages[tabType.skill] = HeroPanel.transform.Find("技能界面").gameObject;
        #endregion

        #region 注册页面切换按钮
        Button[] btns = new Button[5];
        btns[0] = HeroPanel.transform.Find("属性按钮").GetComponent<Button>();
        btns[1] = HeroPanel.transform.Find("物品按钮").GetComponent<Button>();
        btns[2] = HeroPanel.transform.Find("技能按钮").GetComponent<Button>();
        btns[3] = HeroPanel.transform.Find("任务按钮").GetComponent<Button>();
        btns[4] = HeroPanel.transform.Find("关闭按钮").GetComponent<Button>();
        foreach(var btn in btns)
        {
            btn.onClick.AddListener(delegate()
            {
                control.TabBtnClick(btn);
            });
        }
        #endregion

        #region 注册人物头像，初始化页面
        heroAvatar = ButtomPanel.transform.Find("人物头像").GetComponent<Button>();
        experience = ButtomPanel.transform.Find("经验条").GetComponent<Slider>();
        model.ExperienceChanged += ExperienceChanged;
        heroAvatar.onClick.AddListener(delegate()
        {
            Debug.Log("人物头像点击");
            control.TabBtnClick(heroAvatar);
        });
        HeroPanel.SetActive(false);
        foreach(var v in tabPages.Values) { v.SetActive(false); }
        #endregion
    }

    private void attrValueChanged()
    {
        valuesText[ValuesType.agile].text = model.attrSys.agile.ToString();
        valuesText[ValuesType.attackValue].text = model.valuesSys.AttackValue.ToString();
        valuesText[ValuesType.attackSpeed].text = model.valuesSys.AttackSpeed.ToString();
        valuesText[ValuesType.critcalRate].text = model.valuesSys.CritRate.ToString() + "%";
        valuesText[ValuesType.dodgeRate].text = model.valuesSys.DodgeRate.ToString() + "%";
        valuesText[ValuesType.health].text = model.valuesSys.HealthLimit.ToString();
        valuesText[ValuesType.hpRestoreSpeed].text = model.valuesSys.HpRestoreSpeed.ToString();
        valuesText[ValuesType.intellgence].text = model.attrSys.intelligence.ToString();
        valuesText[ValuesType.lucky].text = model.attrSys.lucky.ToString();
        valuesText[ValuesType.magic].text = model.valuesSys.MagicLimit.ToString();
        valuesText[ValuesType.magicRes].text = model.valuesSys.MagicResistance.ToString() + "%";
        valuesText[ValuesType.mpRestoreSpeed].text = model.valuesSys.MpRestoreSpeed.ToString();
        valuesText[ValuesType.physicRes].text = model.valuesSys.PhysicalResistance.ToString() + "%";
        valuesText[ValuesType.physique].text = model.attrSys.physique.ToString();
        valuesText[ValuesType.spellPower].text = model.valuesSys.SpellPower.ToString() + "%";
        valuesText[ValuesType.strength].text = model.attrSys.strength.ToString();
    }

    void EquipChanged(EquipmentType type, string id)
    {
        if (id == "-1") { equips[type].sprite = null; return; }
        var sprite = Resources.Load<Sprite>(Path.respPicEquip + id);
        if (sprite == null) Debug.LogError("can not find");
        equips[type].sprite = sprite;
    }


    private void SkillChanged(int arg1, string arg2)
    {
        skillsImage[arg1].sprite = Resources.Load<Sprite>("Image/" + arg2.ToString() + ".jpg");
    }

    private void LevelChanged(int obj)
    {
        LevelNumber.text = obj.ToString();
    }

    private void ExperienceChanged(float obj)
    {
        experience.value = obj;
    }

    private void MpChanged(float obj)
    {
        MP.fillAmount = obj;
    }

    private void HpChanged(float obj)
    {
        HP.fillAmount = obj;
    }

    private void PackageChanged(int row,int line,string id)
    {
        if (id == "-1")
        {
            packageImage[row, line].sprite = null;
            return;
        }
        var sprite = Resources.Load<Sprite>(Path.respPicEquip + id);
        if (sprite == null) Debug.LogError("can not find");
        packageImage[row, line].sprite = sprite;
    }

}
