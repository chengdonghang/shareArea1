using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteeringSys
{
    [RequireComponent(typeof(Radar))]
    public class Vehicle : MonoBehaviour
    {
        protected List<Steering> steerings = new List<Steering>(5);
        public float maxSpeed = 10;
        public float maxForce = 100;
        protected float sqrMaxSpeed;
        public float mass = 1;
        public Vector3 velocity;
        public float damping = 0.9f;
        public float computeInterval = 0.2f;
        public bool isPlanar = true;
        private Vector3 steeringForce = new Vector3();
        protected Vector3 acceleration = new Vector3();
        public float timer;
        public bool DebugMode = true;

        /// <summary>
        /// 角色在何距离时开始减速 
        /// </summary>
        public float SlowDownDis;
        /// <summary>
        /// 角色的大小
        /// </summary>
        public float characterRadius;
        /// <summary>
        /// 角色距离物体多近时停止
        /// </summary>
        public float StoppingDis;

        public void Stop()
        {
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
        }

        void Awake()
        {
            steeringForce = new Vector3(0, 0, 0);
            sqrMaxSpeed = maxSpeed * maxSpeed;
            velocity = new Vector3(0, 0, 0);
            timer = 0;
            
            steerings.AddRange(GetComponents<Steering>());
            foreach (var s in steerings)
            {
                s.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            steeringForce = new Vector3(0, 0, 0);
            if(timer > computeInterval)
            {
                foreach(Steering s in steerings)
                {
                    if (DebugMode)
                        Debug.Log("name:"+s.ToString()+"return force:" + s.Force());
                    if (s.enabled)
                    {
                        steeringForce += s.Force() * s.weight;
                    }
                        
                }
                steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
                acceleration = steeringForce / mass;
                timer = 0;
            } 
        }
    }

}


