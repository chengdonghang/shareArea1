using UnityEngine;
using System.Collections;

public class HeroBodyController : MonoBehaviour
{
    public float MoveSpeed = 15.0f;
    public float RotateSpeed = 80.0f;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void PlayAnim(string name)
    {
        animator.SetBool(name, true);
    }

    void StopAnim(string name) 
    {
        animator.SetBool(name, false);
    }

    void StopAnimUntilOver(string name)
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("simpleAttack")))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
        {
            animator.SetBool("simpleAttack", false);
        }
    }

    void MoveInSelfSpace(Vector3 vector)
    {
        transform.Translate(vector, Space.Self);
    }

    #region 控制区域
    bool attackController()
    {
        string anim = "simpleAttack";
        StopAnimUntilOver(anim);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(anim, true);
            return false;
        }
        return true;
    }

    void moveController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Approximately(v, 0.0f))
            StopAnim("walk");
        if (Mathf.Approximately(h, 0.0f))
        {
            StopAnim("walkLeft");
            StopAnim("walkRight");
        }
        if (v > 0.0f)
        {
            PlayAnim("walk");
        }
        else
        {
            if (h > 0.0f)
            {
                PlayAnim("walkRight");
            }
            else if (h < 0.0f)
            {
                PlayAnim("walkLeft");
            }
        }
        var vector = Vector3.forward * v + Vector3.right * h;
        MoveInSelfSpace(vector.normalized * MoveSpeed * Time.deltaTime);
    }
    #endregion

    void Update()
    {
        if (attackController())
            moveController();
    }

}
