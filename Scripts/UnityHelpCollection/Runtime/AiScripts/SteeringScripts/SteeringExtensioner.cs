using UnityEngine;
using System.Collections;
using SteeringSys;

namespace HutongGames.PlayMaker.Actions {
    public class SteeringExtensioner : FsmStateAction
    {
        public ForceType forceType = ForceType.m_null;

        public override void OnEnter()
        {
            var comp = Owner.GetComponent<AILocomotion>();
            switch (forceType)
            {

            }


        }
    }
}

