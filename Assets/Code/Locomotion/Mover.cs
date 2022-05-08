using UnityEngine;
using UnityEngine.AI;

namespace Code.Locomotion {
    public class Mover : MonoBehaviour {
        [SerializeField] private Transform target;

        private void Awake() {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
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
            Debug.Log("Moving");
            target.position = point;
            m_Agent.destination = target.position;
        }

        private NavMeshAgent m_Agent;
        private Animator m_Animator;
        private static readonly int _FORWARD_SPEED = Animator.StringToHash("forwardSpeed");
    }
}