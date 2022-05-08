using System;
using Code.Locomotion;
using UnityEngine;

namespace Code.Combat {
    public class Fighter : MonoBehaviour {
        
        Transform m_Target;
        Mover m_Mover;

        private void Awake() {
            m_Mover = GetComponent<Mover>();
        }

        public void Attack(CombatTarget combatTarget) {
            m_Target = combatTarget.transform;
            Debug.Log("Attack!", m_Target);
        }

        private void Update() {
            if (m_Target is not null) {
                m_Mover.MoveTo(m_Target.transform.position);
            }
        }
    }
}