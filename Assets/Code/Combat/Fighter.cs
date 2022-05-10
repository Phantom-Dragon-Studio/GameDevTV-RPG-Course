using System;
using Code.Locomotion;
using UnityEngine;

namespace Code.Combat {
    public class Fighter : MonoBehaviour {
        [SerializeField] private float m_attackRange;

        private void Awake() {
            m_Mover = GetComponent<Mover>();
        }

        public void Attack(CombatTarget combatTarget) {
            m_Target = combatTarget.transform;
            Debug.Log("Attack!", m_Target);
        }

        public void Cancel() {
            m_Target = null;
        }

        private bool InRange() {
            return m_attackRange >= Vector3.Distance(m_Target.position, transform.position);
        }

        private void Update() {
            if (m_Target is null) return;
            
            if (InRange())
                m_Mover.Stop();
            else 
                m_Mover.MoveTo(m_Target.transform.position);
        }

        private Transform m_Target;
        private Mover m_Mover;
    }
}