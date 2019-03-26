using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace Sensors
{
    [RequireComponent(typeof(PlayMakerFSM))]
    public class Sensor : MonoBehaviour
    {
        public TriggerSys manager;
        //add sensor's type from here
        public enum SensorType
        {
            sight,
            sound,
            health
        }

        public SensorType sensorType;
        public virtual void Notify(Trigger t) { }
        protected void SendEvent(SensorType sensorType,GameObject target)
        {
            var fsm = GetComponent<PlayMakerFSM>();
            switch (sensorType)
            {
                case SensorType.sight:
                    fsm.SendEvent("SIGHT");
                    var sight = fsm.FsmVariables.GetFsmGameObject("SIGHT_THING");
                    sight.Value = target;
                    break;
                case SensorType.sound:
                    fsm.SendEvent("HEAR");
                    var hear = fsm.FsmVariables.GetFsmGameObject("HEAR_THING");
                    hear.Value = target;
                    break;
            }

        }
    }


}

