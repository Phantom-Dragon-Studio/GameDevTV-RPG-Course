using UnityEngine;
using UnityEngine.Serialization;

namespace App.Code.Combat {
    public class Lifepoints : MonoBehaviour {
        [SerializeField] private float healthPoints = 100f;

        public float Health {
            get => healthPoints;
            private set => healthPoints = value;
        }

        public void TakeDamage(float damage)
        {
            Health = Mathf.Max(Health - damage, 0);

            if (healthPoints == 0) {
                Die();
            }
        }

        public bool IsDead()
        {
            return m_IsDead;
        }

        private void Die()
        {
            if (m_IsDead)
                return;

            m_IsDead = true;
            GetComponent<Animator>().SetTrigger(_DIE);
        }

        private static readonly int _DIE = Animator.StringToHash("die");
        private bool m_IsDead = false;
    }
}