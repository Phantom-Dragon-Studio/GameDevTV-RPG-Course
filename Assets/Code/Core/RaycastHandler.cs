using UnityEngine;

namespace Code.Core {
    public class RaycastHandler {
        private Ray m_Ray;

        public RaycastHit? RaycastPointFromCamera(Camera camera, Vector3 point)
        {
            m_Ray = camera.ScreenPointToRay(point);
            bool hasHit = Physics.Raycast(m_Ray.origin, m_Ray.direction, out RaycastHit raycastHit);

            if (hasHit)
                return raycastHit;

            return null;
        }

        public RaycastHit? RaycastPointFromCamera(Camera camera, Vector3 point, LayerMask mask)
        {
            m_Ray = camera.ScreenPointToRay(point);
            bool hasHit = Physics.Raycast(m_Ray, out RaycastHit raycastHit, Mathf.Infinity, mask);

            if (hasHit)
                return raycastHit;

            return null;
        }

        public RaycastHit[] RaycastAll(Camera camera, Vector3 point, LayerMask mask)
        {
            m_Ray = camera.ScreenPointToRay(point);
            RaycastHit[] raycastHits = Physics.RaycastAll(m_Ray, Mathf.Infinity, mask);

            return raycastHits.Length > 0 ? raycastHits : null;
        }
    }
}