using UnityEngine;
using UnityEngine.InputSystem;

namespace hinos.player {
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private CharacterController character;


        private Vector2 direction;

        private CharacterMovement cMovement;
        

        private void Awake() {
            
            playerCharacter = new PlayerCharacter(CharacterController);
        }

        private void Update() {

            playerCharacter.SetMoveDirection(new Vector3(direction.x, 0 ,direction.y))
        }

        public void OnMove(InputAction.CallbackContext context) {
            direction = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context) {

        }
    }

    public class PlayerCharacter {
        private GameObject source;

        public PlayerCharacter(GameObject source){
            
        }
    }
}