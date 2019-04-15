using UnityEngine;
using Rpg;

[RequireComponent(typeof(HeroValueModel))]
public class HeroBodyController : MonoBehaviour
{
    public float MoveSpeed = 15.0f;
    public float RotateSpeed = 80.0f;
    private bool _hasClicked = false;
    private Skills _nowSkill;

    public Animator animator;
    public Transform Camera;
    public HeroValueModel heroManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        heroManager = GetComponent<HeroValueModel>();
    }

    void PlayAnim(string name)
    {
        animator.SetBool(name, true);
    }

    void SetAttackAnim(int value)
    {
        animator.SetInteger("attackBtnClick", value);
    }

    void StopAnim(string name) 
    {
        animator.SetBool(name, false);
    }

    void StopAttackAnim(string name)
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(name)))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
        {
            animator.SetInteger("attackBtnClick", 0);
        }
    }

    bool AnimIsOver(string name)
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(name)))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
        {
            return true;
        }
        return false;
    }

    void MoveInSelfSpace(Vector3 vector)
    {
        transform.Translate(vector, Space.Self);
    }

    void MoveInWorldSpace(Vector3 vector)
    {
        transform.Translate(vector, Space.World);
    }

    void RotateAroundYAxis(float angle)
    {
        transform.Rotate(transform.up, angle);
    }

    #region 动画事件函数
    void SetClick()
    {
        _hasClicked = true;
    }

    void ActionOver()
    {
        animator.SetInteger("attackBtnClick", 0);
    }
    
    void SkillEvent1()
    {
        if (_nowSkill == null) return;
        _nowSkill.ReleaseAnimEvent1(gameObject);
    }

    void SkillEvent2()
    {
        if (_nowSkill == null) return;
        _nowSkill.ReleaseAnimEvent2(gameObject);
    }

    void SkillEvent3()
    {
        if (_nowSkill == null) return;
        _nowSkill.ReleaseAnimEvent3(gameObject);
    }

    void SkillOver()
    {
        _nowSkill = null;
        animator.SetInteger("skillAttack", 0);
    }
    #endregion

    #region 控制区域

    bool skillController()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!heroManager.SkillInCold(0))
                ReleaseSkill(heroManager.skills[0]);
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (!heroManager.SkillInCold(1))
                ReleaseSkill(heroManager.skills[1]);
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (!heroManager.SkillInCold(2))
                ReleaseSkill(heroManager.skills[2]);
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (!heroManager.SkillInCold(3))
                ReleaseSkill(heroManager.skills[3]);
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            heroManager.UseBloodOrMagicVial(true);
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            heroManager.UseBloodOrMagicVial(true);
            return false;
        }
        else return true;
    }

    void ReleaseSkill(Skills skills)
    {
        if (skills == null) return;
        if (skills.playAnim < 0 && skills.playAnim >= 3)
        {
            Debug.LogError("error");
            return;
        }
        animator.SetInteger("skillAttack", skills.playAnim);
        skills.ReleaseNow(gameObject);
        _nowSkill = skills;
    }

    bool attackController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("idle")&& animator.GetInteger("attackBtnClick") == 0)
            {
                SetAttackAnim(1);
            } 
            else if(info.IsName("simpleAttack"+animator.GetInteger("attackBtnClick").ToString()) 
                && animator.GetInteger("attackBtnClick") > 0&&_hasClicked)
            {
                _hasClicked = false;
                int value = animator.GetInteger("attackBtnClick");
                SetAttackAnim(value+1);
            }
            return false;
        }
        return true;
    }

    void moveController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (Mathf.Approximately(v, 0.0f))
        {
            StopAnim("walk");
            StopAnim("walkBack");
        }
        else
        {
            if (v > 0.0f)
                PlayAnim("walk");
            else if (v < 0.0f)
                PlayAnim("walkBack");
        }
        var vector = Camera.forward;
        vector.y = 0;
        RotateAroundYAxis(h * MoveSpeed);
        //Debug.DrawRay(transform.position, vector * 100);
        if(info.IsName("walkForward")||info.IsName("walkBack"))
            MoveInWorldSpace(vector.normalized * v * MoveSpeed * Time.deltaTime);
    }
    #endregion

    void Update()
    {
        Debug.Log(_hasClicked);
        if(skillController())
            if (attackController())
                moveController();
    }

}
