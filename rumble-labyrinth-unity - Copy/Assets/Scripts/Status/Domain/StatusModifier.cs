namespace hinos.status {

    public enum StatusModifierType {
        FLAT = 10,
        PERCENT_ADDITIVE = 20,
    }

    [System.Serializable]
    public class StatusModifier {
        public readonly float value;
        public readonly StatusModifierType type;
        public readonly object source;
        public readonly int order;

        public StatusModifier(float value, StatusModifierType type, object source, int order) {
            this.value = value;
            this.type = type;
            this.source = source;
            this.order = order;
        }
    }
}