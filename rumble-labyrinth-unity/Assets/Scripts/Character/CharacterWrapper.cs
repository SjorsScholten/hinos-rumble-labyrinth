using UnityEngine;
using hinos.util;

namespace hinos.character
{
    public class CharacterWrapper : InstanceWrapper {
        private CharacterMovement _characterMovement;

        public CharacterMovement Movement {
            get => _characterMovement;
        }

        public CharacterWrapper(GameObject sourceObject) : base(sourceObject) {

        }

        protected override void ProcessGetComponents() {
            _characterMovement = GetComponentOnSource<CharacterMovement>();
        }
    }
}
