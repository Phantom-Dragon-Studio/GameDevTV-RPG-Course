using UnityEngine;

namespace App.Code.Core {
    public class ActionScheduler : MonoBehaviour{
        private IAction m_CurrentAction;
        
        public void StartAction(IAction action) {
            
            if (action == m_CurrentAction) 
                return;

            m_CurrentAction?.Cancel();
            m_CurrentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}