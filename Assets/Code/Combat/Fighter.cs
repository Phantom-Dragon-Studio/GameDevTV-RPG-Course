using UnityEngine;

namespace Code.Combat {
    public class Fighter : MonoBehaviour {
        public void Attack(CombatTarget target) {
            Debug.Log("Attack!", target);
        }
    }
}