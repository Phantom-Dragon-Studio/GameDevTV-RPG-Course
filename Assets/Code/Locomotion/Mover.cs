using Code.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Locomotion {
    public class Mover : MonoBehaviour {
        private void Awake() {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            m_Fighter = GetComponent<Fighter>();
        }

        private void Update() {
            UpdateAnimator();
        }

        private void UpdateAnimator() {
            Vector3 localVelocity = transform.InverseTransformDirection(m_Agent.velocity);
            float speed = localVelocity.z;

            m_Animator.SetFloat(_FORWARD_SPEED, speed);
        }

        public void MoveTo(Vector3 point) {
            m_Agent.destination = point;
            m_Agent.isStopped = false;
        }
        
        public void MoveToAction(Vector3 point) {
            m_Fighter.Cancel();
            MoveTo(point);
        }

        public void Stop() {
            m_Agent.isStopped = true;
        }

        private NavMeshAgent m_Agent;
        private Animator m_Animator;
        private Fighter m_Fighter;
        private static readonly int _FORWARD_SPEED = Animator.StringToHash("forwardSpeed");
    }
}