using System.Collections.Generic;
using EventEditor;
using UnityEngine;
using HutongGames.PlayMaker;

public class FindPlayerEvent : m_Event
{
    private GameObject _player;
    public List<PlayMakerFSM> fsms;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player) _player = player;
        else Debug.LogError("没有找到玩家");
    }

    public override void PlayEvents(m_Trigger sender, object info)
    {
        Debug.Log("happen");
        foreach(var _fsm in fsms)
        {
            var player = _fsm.FsmVariables.GetFsmGameObject("enemy");
            player.Value = _player;
        }

    }

}
