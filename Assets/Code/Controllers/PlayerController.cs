using Code.Combat;
using Code.Core;
using Code.Locomotion;
using UnityEngine;

namespace Code.Controllers {
    public class PlayerController : MonoBehaviour {
        
        [SerializeField] private LayerMask movableMask;
        [SerializeField] private LayerMask attackableMask;
        [SerializeField] private new Camera camera;
        [SerializeField] private Fighter fighter;

        private void Awake() {
            m_RaycastHandler = new RaycastHandler();
            m_Mover = GetComponent<Mover>();
        }
        
        private void Update() {
            if(TryAttack())
                return;
            
            if(TryMoveToCursor())
                return;

            Debug.Log("Nothing to do");
        }

        private bool TryMoveToCursor() {
            RaycastHit? hitData = m_RaycastHandler.RaycastPointFromCamera(camera, Input.mousePosition, movableMask);
            if (hitData is null) return false;
            
            if(Input.GetMouseButton(0))
                m_Mover.MoveTo(hitData.Value.point);

            return true;
        }
        
        private bool TryAttack() {
            RaycastHit[] hitsData = m_RaycastHandler.RaycastAll(camera, Input.mousePosition, attackableMask);
            if (hitsData is null) return false;

            foreach (RaycastHit hitData in hitsData) {
                CombatTarget target = hitData.transform.GetComponent<CombatTarget>();

                if (target is null)
                    continue;
                
                if(Input.GetMouseButtonDown(0))
                    fighter.Attack(target);
                return true;
            }

            return false;
        }
        
        private RaycastHandler m_RaycastHandler;
        private Mover m_Mover;
    }
}