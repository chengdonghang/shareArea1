using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace SteeringSys
{
    /// <summary>
    /// 根据加速度具体调用三个动力组件运动,其也可被调用来控制物体的运动
    /// </summary>
    public class AILocomotion:Vehicle
    {

        public bool ifTurning = true;
        private CharacterController controller;
        private Rigidbody theRigidbody;
        private Animator animator;

        private Vector3 moveDistance;
        private float sumDegree;

        #region 特殊力参数区
        public float MAX_SEE_AHEAD = 2.0f;
        #endregion

        #region 查找与删除力
        public bool DisableForce<T>()where T:Steering
        {
            var force = GetComponent<T>();
            if (!force) throw new NullReferenceException("没有那种类型的力");
            force.enabled = false;
            return true;
        }

        public bool DisableForce(ForceType forceType)
        {
            switch (forceType)
            {
                case ForceType.arrive:
                    DisableForce<SteeringForArrive>();
                    return true;
                case ForceType.flee:
                    DisableForce<SteeringForFlee>();
                    return true;
                case ForceType.pursuit:
                    DisableForce<SteeringForPursuit>();
                    return true;
                case ForceType.wander:
                    DisableForce<SteeringForWander>();
                    return true;
                default:
                    return false;
            }
        }

        public bool DisableAllForce()
        {
            var list = GetComponents<Steering>();
            if (list.Length == 0) return false;
            foreach(var force in list)
            {
                force.enabled = false;
            }
            return true;
        }

        public bool DisableAndStop()
        {
            base.Stop();
            return DisableAllForce();
        }
        #endregion

        #region 增添操纵力

        public SteeringForCollisionAvoidance AvoidWallOn()
        {
            var comp = GetComponent<SteeringForCollisionAvoidance>();
            if (!comp)
                comp = gameObject.AddComponent<SteeringForCollisionAvoidance>();
            comp.isPlanar = isPlanar;
            steerings.Add(comp);
            return comp;
        }

        public SteeringForArrive ArriveTo(GameObject destination)
        {
            var comp = GetComponent<SteeringForArrive>();
            if (!comp)
            {
                comp = gameObject.AddComponent<SteeringForArrive>();
                steerings.Add(comp);
            }
            comp.arrivalDistance = StoppingDis;
            comp.characterRadius = characterRadius;
            comp.slowDownDistance = SlowDownDis;
            comp.isPlanar = isPlanar;
            comp.target = destination;
            return comp;
        }

        public SteeringFollowPath FollowPath(GameObject fatherNode,bool ifWhile)
        {
            List<Transform> path = new List<Transform>();
            foreach(Transform o in fatherNode.transform)
            {
                if (o!=fatherNode.transform)
                {
                    path.Add(o);
                }
            }
            return FollowPath(path,ifWhile);
        }

        public SteeringFollowPath FollowPath(List<Transform> path,bool ifWhile)
        {
            var comp = GetComponent<SteeringFollowPath>();
            if (!comp)
            {
                comp = gameObject.AddComponent<SteeringFollowPath>();
                steerings.Add(comp);
            }
            comp.arrivalDistance = StoppingDis;
            comp.pointRadius = characterRadius;
            comp.slowDownDistance = SlowDownDis;
            comp.isPlanar = isPlanar;
            comp.ifWhile = ifWhile;
            comp.SetTargets(path);
            return comp;
        }
     
        public SteeringForPursuit ChaseTo(GameObject target)
        {
            var comp = GetComponent<SteeringForPursuit>();
            if (!comp)
            {
                comp = gameObject.AddComponent<SteeringForPursuit>();
                steerings.Add(comp);
            }
            comp.target = target;
            return comp;
        }

        public SteeringForFlee FleeFrom(GameObject target)
        {
            var comp = GetComponent<SteeringForFlee>();
            if (!comp)
            {
                comp = gameObject.AddComponent<SteeringForFlee>();
                steerings.Add(comp);
            }
            comp.target = target;
            return comp;
        }

        public SteeringForWander WanderOn(float wanderRadius,float wanderJitter,float wanderDis)
        {
            var comp = GetComponent<SteeringForWander>();
            if (comp)
            {
                comp.wanderJitter = wanderJitter;
                comp.wanderDistance = wanderDis;
                comp.wanderRadius = wanderRadius;
            }
            else
            {
                comp = gameObject.AddComponent<SteeringForWander>();
                steerings.Add(comp);
                comp.wanderJitter = wanderJitter;
                comp.wanderDistance = wanderDis;
                comp.wanderRadius = wanderRadius;
            }
            return comp;
        }
        

        /// <summary>
        /// 增添一个操纵力，不改变其默认参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T AddForce<T>()where T : Steering
        {
            if (!GetComponent<T>())
            {
                var comp = gameObject.AddComponent<T>();
                steerings.Add(comp);
                return comp;
            }
            return null;
        }
        #endregion

        void Start()
        {
            controller = GetComponent<CharacterController>();
            theRigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            moveDistance = new Vector3(0, 0, 0);
            sumDegree = 0.0f;
        }

        void FixedUpdate()
        {
            velocity += acceleration * Time.fixedDeltaTime;
            if (velocity.sqrMagnitude > sqrMaxSpeed)
                velocity = velocity.normalized * maxSpeed;
            moveDistance = velocity * Time.fixedDeltaTime;

            if(isPlanar)
            {
                velocity.y = 0;
                moveDistance.y = 0;
            }

            if (controller != null)
                controller.SimpleMove(velocity);
            else if (theRigidbody == null || theRigidbody.isKinematic)
                transform.position += moveDistance;
            else
                theRigidbody.MovePosition(theRigidbody.position + moveDistance);

            if(velocity.sqrMagnitude>0.00001&&ifTurning)
            {
                Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
                if (isPlanar)
                {
                    newForward.y = 0;
                }
                transform.forward = newForward;
            }
            
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, StoppingDis);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, SlowDownDis);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, characterRadius);
            Gizmos.DrawRay(transform.position, transform.forward * MAX_SEE_AHEAD);
        }
    }
}
