using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringSys
{
    public class SteeringForCollisionAvoidance : Steering
    {
        public bool isPlanar;
        public bool isQueueMode;
        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;
        private float maxForce;
        public float MAX_SEE_AHEAD = 2.0f;
        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            maxSpeed = m_vehicle.maxSpeed;
            maxForce = m_vehicle.maxForce;
            isPlanar = m_vehicle.isPlanar;
        }

        public override Vector3 Force()
        {
            RaycastHit hit;
            Vector3 force = new Vector3(0, 0, 0);
            Vector3 velocity = m_vehicle.velocity;
            Vector3 normaizedVelocity = velocity.normalized;
            float ratio = 1.0f;
            if(!isQueueMode)
                ratio = velocity.magnitude / maxSpeed;
            if (Physics.Raycast(transform.position, normaizedVelocity,out hit,MAX_SEE_AHEAD * ratio))
            {
                Vector3 ahead = transform.position + normaizedVelocity * MAX_SEE_AHEAD * ratio;
                force = ahead - hit.collider.transform.position;
                var v = Vector3.Dot(force,hit.normal);
                force = v * hit.normal;
                if(isPlanar)
                {
                    force.y = 0;
                }
            }
            return force;
        }

    }
}
