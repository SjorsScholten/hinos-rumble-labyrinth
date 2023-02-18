using System;

namespace hinos.characters {
    public class CharacterData : ScriptableObject {
        [SerializeField] private string characterName;
        [SerializeField] private string characterDescription;

        [SerializeField] private GameObject characterPrefab;

        public string Name => characterName;
        public string Description => characterDescription;
    }

    [Serializable]
    public class CharacterInstance {
        private readonly CharacterData data;

        public CharacterInstance(CharacterData data) {
            this.data = CharacterData;
        }
    }
}