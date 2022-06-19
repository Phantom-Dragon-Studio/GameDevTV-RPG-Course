using System;
using App.Code.Combat;
using App.Code.Core;
using App.Code.Locomotion;
using UnityEditor;
using UnityEngine;

namespace App.Code.Controllers {
    public class AIController : MonoBehaviour {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private Fighter fighter;
        [SerializeField] private Lifepoints lifepoints;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float patrolPause = 1f;
        [SerializeField] private float waypointTolerance = 0.1f;

        private void Start()
        {
            m_Player = GameObject.FindWithTag("Player");
            m_GuardPosition = transform.position;
            m_Mover = GetComponent<Mover>();
            m_Scheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (lifepoints.IsDead())
                return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(m_Player)) {
                m_TimeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (m_TimeSinceLastSawPlayer < suspicionTime)
                SuspicionBehavior();
            else
                PatrolBehavior();

            m_TimeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = m_GuardPosition;

            if (patrolPath != null) {
                if (AtWaypoint()) {
                    Dwell();
                }
                nextPosition = GetCurrentWaypoint();
            }
            
            m_Mover.MoveToAction(nextPosition);
        }

        private void Dwell()
        {
            m_TimeAtWaypoint += Time.deltaTime;
            
            if (patrolPause <= m_TimeAtWaypoint) {
                CycleWaypoint();
                m_TimeAtWaypoint = 0;
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(m_CurrentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            m_CurrentWaypointIndex = patrolPath.GetNextIndex(m_CurrentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            m_Scheduler.CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(m_Player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(m_Player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private ActionScheduler m_Scheduler;
        private Vector3 m_GuardPosition;
        private GameObject m_Player;
        private Mover m_Mover;

        private int m_CurrentWaypointIndex = 0;
        private float m_TimeSinceLastSawPlayer = Mathf.Infinity;
        private float m_TimeAtWaypoint = 0;
    }
}