using UnityEngine;
using HutongGames.PlayMaker;

public class AIDataSettinger : MonoBehaviour
{
    public float alarmRange = 1.0f;
    public float meleeAttackRange = 1.0f;
    PlayMakerFSM _fsm;

    private void Start()
    {
        _fsm = GetComponent<PlayMakerFSM>();
        var v1 = _fsm.FsmVariables.GetFsmFloat("alarmAttackRange");
        v1.Value = alarmRange;
        var v2 = _fsm.FsmVariables.GetFsmFloat("attackRange");
        v2.Value = meleeAttackRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alarmRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
    }
}
