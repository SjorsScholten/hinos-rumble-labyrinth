using UnityEngine;

namespace hinos.player
{
    public class PracticeTarget : MonoBehaviour, IDamageHandler
    {
        public void HandleDamage(float damageAmount) {
            Debug.Log($"{this.gameObject.name} has been damaged for {damageAmount} points");
        }
    }
}

