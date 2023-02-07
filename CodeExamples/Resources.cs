namespace hinos.resources {
    public class Resource : ScriptableObject {
        [SerializeField] private string resourceName;
        [SerializeField] private string resourceDescription;
        [SerializeField] private Sprite resourceIcon;

        public string Name => resourceName;
        public string Description => resourceDescription;
        public Sprite Icon => resourceIcon;
    }

    public class Satchel {
        private Resource minerals;
        private Resource organic;
        private Resource coin;
    }
}