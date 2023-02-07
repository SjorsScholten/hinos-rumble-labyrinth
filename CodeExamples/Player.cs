using UnityEngine;
using UnityEngine.InputSystem;

namespace hinos.player {
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private CharacterController character;

        private Vector2 direction;

        private PlayerCharacter playerCharacter;
        

        private void Awake() {
            
            playerCharacter = new PlayerCharacter(CharacterController);
        }

        private void Update() {

            playerCharacter.SetMoveDirection(new Vector3(direction.x, 0 ,direction.y))
        }

        public void OnMove(InputAction.CallbackContext context) {
            direction = context.ReadValue<Vector2>();
        }
    }

    public class PlayerCharacter {
        private readonly CharacterController characterController;
        private readonly CharacterMovement characterMovement;

        public PlayerCharacter(CharacterController characterController) {
            this.characterController = characterController;
            this.characterMovement = characterController.GetComponent<CharacterMovement>();
        }

        public void SetMoveDirection(Vector3 direction) {
            characterMovement.inputDirection = direction;
        }
    }
}