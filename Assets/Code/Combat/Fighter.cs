using App.Code.Combat;
using Code.Core;
using Code.Locomotion;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private ActionScheduler scheduler;

        public float AttackSpeed {
            get => attackSpeed;
            set => attackSpeed = value;
        }

        private void Awake()
        {
            m_Mover = GetComponent<Mover>();
            m_Animator = GetComponent<Animator>();
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget is null)
                return false;

            Lifepoints targetToTest = combatTarget.GetComponent<Lifepoints>();

            return targetToTest is not null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            if (!combatTarget) return;

            m_Target = combatTarget.transform.GetComponent<Lifepoints>();
            scheduler.StartAction(this);
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger(_STOP_ATTACK);
            m_Target = null;
        }

        private bool InRange()
        {
            return attackRange >= Vector3.Distance(m_Target.transform.position, transform.position);
        }

        private void Update()
        {
            m_TimeSinceLastAttack += Time.deltaTime;

            if (m_Target is null || m_Target.IsDead()) return;

            if (InRange()) {
                AttackBehavior();
            }
            else
                m_Mover.MoveTo(m_Target.transform.position);
        }

        //! AttackBehavior calls Hit() via animation trigger.
        private void AttackBehavior()
        {
            if (m_TimeSinceLastAttack < attackSpeed) {
                return;
            }

            transform.LookAt(m_Target.transform);
            m_Animator.ResetTrigger(_STOP_ATTACK); //Prevents bug from Lecture 44
            m_Animator.SetTrigger(_ATTACK); //Calls Hit
            m_TimeSinceLastAttack = 0;
        }

        //? Hit is called via animation trigger.
        private void Hit()
        {
            if (m_Target is null) {
                return;
            }
            
            m_Target.TakeDamage(weaponDamage);
            Debug.Log("Remaining Health: " + m_Target.Health);
        }

        private Lifepoints m_Target;
        private Mover m_Mover;
        private Animator m_Animator;
        private float m_TimeSinceLastAttack;
        private static readonly int _ATTACK = Animator.StringToHash("attack");
        private static readonly int _STOP_ATTACK = Animator.StringToHash("stopAttack");
    }
}