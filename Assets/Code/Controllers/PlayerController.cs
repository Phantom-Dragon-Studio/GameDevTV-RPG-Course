using App.Code.Combat;
using App.Code.Core;
using App.Code.Locomotion;
using UnityEngine;

namespace App.Code.Controllers {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private LayerMask movableMask;
        [SerializeField] private LayerMask attackableMask;
        [SerializeField] private new Camera camera;
        [SerializeField] private Fighter fighter;

        private void Awake()
        {
            m_RaycastHandler = new RaycastHandler();
            m_Mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (InteractWithCombat())
                return;

            InteractWithMovement();
        }

        private bool InteractWithMovement()
        {
            RaycastHit? hitData = m_RaycastHandler.RaycastPointFromCamera(camera, Input.mousePosition, movableMask);
            if (hitData is null) return false;

            if (Input.GetMouseButton(0))
                m_Mover.MoveToAction(hitData.Value.point);

            return true;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hitsData = m_RaycastHandler.RaycastAll(camera, Input.mousePosition, attackableMask);
            if (hitsData is null) return false;

            foreach (RaycastHit hitData in hitsData) {
                CombatTarget target = hitData.transform.GetComponent<CombatTarget>();

                if (!fighter.CanAttack(target.gameObject)) {
                    continue;
                }

                if (Input.GetMouseButton(0)) {
                    fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private RaycastHandler m_RaycastHandler;
        private Mover m_Mover;
    }
}