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
    public Rigidbody m_Rigidbody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        heroManager = GetComponent<HeroValueModel>();
        m_Rigidbody = GetComponent<Rigidbody>();
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
        Debug.Log(_nowSkill + "\t" + gameObject);
        _nowSkill.SkillOver(gameObject);
        _nowSkill = null;
        animator.SetInteger("skillAttack", 0);
    }
    #endregion

    #region 控制区域

    bool skillController()
    {
        //如果当前技能不为空
        if (_nowSkill != null) return true;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!heroManager.SkillInCold(0))
            {
                heroManager.SetSkillInCold(0);
                ReleaseSkill(heroManager.skills[0]);
            }              
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (!heroManager.SkillInCold(1))
            {
                heroManager.SetSkillInCold(1);
                ReleaseSkill(heroManager.skills[1]);
            }
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (!heroManager.SkillInCold(2))
            {
                Debug.Log("hapeen");
                heroManager.SetSkillInCold(2);
                ReleaseSkill(heroManager.skills[2]);
            }
            else
                Debug.Log("Skill In Cold");
            return false;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (!heroManager.SkillInCold(3))
            {
                heroManager.SetSkillInCold(3);
                ReleaseSkill(heroManager.skills[3]);
            }
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
        Debug.Log("hapeen");
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

    void MoveAnimControl(float v)
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (Mathf.Approximately(v, 0.0f))
        {
            StopAnim("walk");
            StopAnim("walkBack");
        }
        else
        {
            if (v > 0.0f)
            {
                PlayAnim("walk");
            }

            else if (v < 0.0f)
            {
                PlayAnim("walkBack");
            }
        }
    }

    void ApplyTurnRotation(Quaternion look)
    {
        var angle = look.eulerAngles;
        angle.x = angle.z = 0;
        look = Quaternion.Euler(angle);
        transform.rotation = look;
    }

    void MoveControl(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        //move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("walkForward") || info.IsName("walkBack"))
        {
            ApplyTurnRotation(Camera.rotation);
            m_Rigidbody.MovePosition(transform.position + move * MoveSpeed * Time.deltaTime);
        }   
    }

    void MoveController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // calculate camera relative direction to move:
        var m_CamForward = Vector3.Scale(Camera.forward, new Vector3(1, 0, 1)).normalized;
        var move = v * m_CamForward + h * Camera.right;
        MoveAnimControl(v);
        MoveControl(move);
    }
    #endregion

    void Update()
    {
        //Debug.Log(_hasClicked);
        if(skillController())
            if (attackController())
                MoveController();
    }

}
