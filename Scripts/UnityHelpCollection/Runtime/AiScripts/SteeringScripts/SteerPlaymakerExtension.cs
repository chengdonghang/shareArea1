
using UnityEngine;
using System.Collections;
using SteeringSys;

namespace HutongGames.PlayMaker.Actions {
    [ActionCategory(ActionCategory.Physics)]
    public class SteerPlaymakerExtension : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(AILocomotion))]


        public AILocomotion sys;
        public FsmBool useOwner = true;
        public ForceType forceType = ForceType.m_null;

        public FsmBool closeIfExit = true;
        public FsmBool ifStop = false;

        public FsmGameObject target;

        public FsmFloat wanderRadius;
        public FsmFloat wanderDis;
        public FsmFloat wanderJitter;

        public override void Reset()
        {
            sys = null;
            useOwner = true;
            forceType = ForceType.m_null;
            closeIfExit = true;
            ifStop = false;
        }

        public override void Awake()
        {
            if (useOwner.Value) sys = Owner.GetComponent<AILocomotion>();
            else if (sys == null) Debug.LogError("Do not have sys!");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            switch (forceType) {
                case ForceType.arrive:
                    sys.ArriveTo(target.Value);
                    break;
                case ForceType.pursuit:
                    sys.ChaseTo(target.Value);
                    break;
                case ForceType.flee:
                    sys.FleeFrom(target.Value);
                    break;
                case ForceType.wander:
                    sys.WanderOn(wanderRadius.Value, wanderJitter.Value, wanderDis.Value);
                    break;
                default:
                    break;
            }
       }

        public override void OnExit()
        {
            base.OnExit();
            if (ifStop.Value) sys.Stop();
            if (closeIfExit.Value) {
                sys.DisableForce(forceType);
            }
        }
    }
}

