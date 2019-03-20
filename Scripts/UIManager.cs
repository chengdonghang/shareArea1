using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using tabType = UIController.SwitchTab;

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
    private Button heroAvatar;
    private Dictionary<EquipmentType, Image> equips = new Dictionary<EquipmentType, Image>();
    private Dictionary<EquipmentType, Button> equipsBtn = new Dictionary<EquipmentType, Button>();
    public Dictionary<tabType, GameObject> tabPages = new Dictionary<tabType, GameObject>();


    public HeroManager model;
    public UIController control;


    void Awake()
    {
        model.HpChanged += HpChanged;
        model.MpChanged += MpChanged;
        model.ExperienceChanged += ExperienceChanged;
        model.LevelChanged += LevelChanged;
        model.SkillChanged += SkillChanged;
        model.PackageChanged += PackageChanged;
        model.EquipChanged+=EquipChanged;

        ButtomPanel = GameObject.FindWithTag("buttomPanel");
        HeroPanel = GameObject.FindWithTag("heroPanel");

        for (int i = 1; i <= 6; i++)
        {
            var val = ButtomPanel.transform.Find("skill" + i.ToString());
            skillsImage[i - 1] = val.Find("Image").GetComponent<Image>();
            skillsImage[i - 1].sprite = null;
            skillsBtn[i - 1] = val.GetComponent<Button>();
        }

        #region 初始化属性界面
        model.valuesSys.valueChanged += attrValueChanged;
        #endregion

        #region 初始化装备字典
        var father1 = HeroPanel.transform.Find("物品界面").Find("装备");
        equips[EquipmentType.helm] = father1.transform.Find("头盔").GetComponent<Image>();
        equips[EquipmentType.pants] = father1.transform.Find("裤子").GetComponent<Image>();
        equips[EquipmentType.belt] = father1.transform.Find("腰带").GetComponent<Image>();
        equips[EquipmentType.decorater] = father1.transform.Find("饰品1").GetComponent<Image>();
        equips[EquipmentType.shoes] = father1.transform.Find("鞋子").GetComponent<Image>();
        equips[EquipmentType.clothes] = father1.transform.Find("衣服").GetComponent<Image>();
        equips[EquipmentType.weapon] = father1.transform.Find("武器1").GetComponent<Image>();
        #endregion

        #region 初始化背包
        var father2 = HeroPanel.transform.Find("物品界面").Find("背包");
        for(int i = 0;i<8;i++)
            for(int j = 0; j < 8; j++)
            {
                Debug.Log("pack" + (i * 8 + (j + 1)).ToString());
                var ch = father2.Find("pack" + (i * 8 + (j + 1)).ToString());
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
        throw new System.NotImplementedException();
    }

    void EquipChanged(EquipmentType type, string id)
    {
        if (id == "-1") equips[type].sprite = null;
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
        if (id == "-1") packageImage[row, line].sprite = null;
        var sprite = Resources.Load<Sprite>(Path.respPicEquip + id);
        if (sprite == null) Debug.LogError("can not find");
        packageImage[row, line].sprite = sprite;
    }

}
