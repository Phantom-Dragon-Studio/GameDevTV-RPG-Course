using App.Code.Core;
using UnityEngine;
using UnityEngine.AI;

namespace App.Code.Locomotion {
    public class Mover : MonoBehaviour, IAction {
        [SerializeField] private Lifepoints lifepoints;

        private void Awake()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            m_Scheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            m_Agent.enabled = !lifepoints.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(m_Agent.velocity);
            float speed = localVelocity.z;

            m_Animator.SetFloat(_FORWARD_SPEED, speed);
        }

        public void MoveTo(Vector3 point)
        {
            m_Agent.destination = point;
            m_Agent.isStopped = false;
        }

        public void MoveToAction(Vector3 point)
        {
            m_Scheduler.StartAction(this);
            MoveTo(point);
        }

        public void Cancel()
        {
            m_Agent.isStopped = true;
        }

        private NavMeshAgent m_Agent;
        private Animator m_Animator;
        private ActionScheduler m_Scheduler;
        private static readonly int _FORWARD_SPEED = Animator.StringToHash("forwardSpeed");
    }
}