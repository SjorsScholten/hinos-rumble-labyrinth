using UnityEngine;
using hinos.character;

namespace hinos.player
{
    public class PlayerCharacterBrain : MonoBehaviour {
        [SerializeField] private GameObject _characterObject = default;

        private CharacterWrapper _character = null;

        // Components
        private PlayerInputProcessor _inputProcessor;

        public GameObject Character {
            get => _characterObject;
            set => SetCharacter(value);
        }

        private void Awake() {
            _inputProcessor = GetComponent<PlayerInputProcessor>();

            if(_characterObject) {
                SetCharacter(_characterObject);
            }
        }

        private void Update() {
            if(_character != null) {
                _character.Movement.HandleMove(_inputProcessor.MoveDirection);
            }
        }

        public void SetCharacter(GameObject characterObject) {
            _characterObject = characterObject;

            if(_characterObject){
                _character = new CharacterWrapper(_characterObject);
            }
            else {
                _character = null;
            }
            
        }
    }
}