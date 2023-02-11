using UnityEngine;

namespace hinos.elements {
    public class ElementData : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private Color color;
        [SerializeField] private Sprite icon;

        public string DisplayName => displayName;
        public Color Color => color;
        public Sprite Icon => icon;
    }

    public class ElementAccumulator : MonoBehaviour {
        [SerializeField] private ElementData data;
        public float value;

        private void Update() {
            value -= Time.deltaTime;
        }
    }

    public class AccumulatorManager : MonoBehaviour {
        private readonly Dictionary<ElementData, ElementAccumulator> accumulatorTable = new();

        private void Awake() {
            Initialize();
        }

        public void Initialize() {
            var accumulators = GetComponents<ElementAccumulator>();
            for(var i = 0; i < accumulators.Length; ++i) {
                accumulatorTable.TryAdd(accumulators[i].element, accumulators[i])
            }
        }

        public ElementAccumulator GetAccumulatorOfElement(ElementData element) {
            if(accumulatorTable.TryGetValue(element, out var result)){
                return result;
            }
            else {
                return null;
            }
        }
    }
}