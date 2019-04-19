using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventEditor
{
    using UnityEngine.Events;

    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class TransformTrigger3D : m_Trigger
    {
        private Collider m_trigger;
        [Tooltip("具有该种tag值的物体才能被检测到，如果为空则所有都检测")]
        public string checkTag;
        public event UnityAction<Collider> enter;
        public event UnityAction<Collider> stay;
        public event UnityAction<Collider> exit;

        private bool ifFirst = false;

        // Use this for initialization
        void Start()
        {
            if ((m_trigger = this.GetComponent<Collider>()) == null)
                m_trigger = gameObject.AddComponent<BoxCollider>();
            m_trigger.isTrigger = true;
            gameObject.layer = 2;
        }

        void OnDrawGizmos()
        {
            if (m_trigger == null)
                return;
            else
            {
                Bounds bound;
                bound = m_trigger.bounds;
                Gizmos.color = new Color(1, 0, 0, 0.3f);
                Gizmos.DrawCube(bound.center, bound.size);
            }
        }
        

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log(collider.name + "triggers" + this.name);
            Send(this, collider);
        }

        void OnTriggerStay(Collider collider)
        {
            if (!ifFirst)
            {
                Send(this, collider);
                ifFirst = true;
            }
        }

        void OnTriggerExit(Collider collider)
        {

        }

    }
}
