using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HP;
    public Image MP;
    public Image HeroImage;
    public Slider experience;
    public Text LevelNumber;
    private Image[] skillsImage = new Image[6];
    private Image[,] packageImage = new Image[6, 8];

    public HeroManager model;

    void Awake()
    {
        model.HpChanged += HpChanged;
        model.MpChanged += MpChanged;
        model.ExperienceChanged += ExperienceChanged;
        model.LevelChanged += LevelChanged;


        model.SkillChanged += SkillChanged;
        model.PackageChanged += PackageChanged;

        for (int i = 1; i <= 6; i++)
        {
            skillsImage[i - 1] = transform.Find("skill" + i.ToString()).Find("Image").GetComponent<Image>();
            skillsImage[i - 1].sprite = null;
        }

        var father = transform.Find("背包");
        for(int i = 0;i<8;i++)
            for(int j = 0; j < 8; j++)
            {
                var ch = father.Find("pack" + (i + 1) * 8 + (j + 1));
                var pic = ch.Find("Image").GetComponent<Image>();
                packageImage[i, j] = pic;
            }
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
        var sprite = Resources.Load<Sprite>(Path.respPicEquip + id);
        if (sprite == null) Debug.LogError("can not find");
        packageImage[row, line].sprite = sprite;
    }

}
