namespace hinos.resources {
    public class ResourceData : ScriptableObject {
        [SerializeField] private string dispayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        public string DisplayName => dispayName;
        public string Description => description;
        public Sprite Icon => icon;
    }

    [System.Serializable]
    public class ResourceInstance {
        private ResourceData data;
        private int amount;

        public ResourceInstance(ResourceData data, int amount) {
            this.data = data;
            this.amount = amount;
        }
    }

    public class Satchel {
        private ResourceInstance minerals;
        private ResourceInstance organic;
        private ResourceInstance coin;
    }
}