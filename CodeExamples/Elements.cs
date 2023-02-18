using UnityEngine;

namespace hinos.elements {

    // ################################
    // Data Objects
    // ################################

    public class ElementData : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private Color color;
        [SerializeField] private Sprite icon;

        public string DisplayName => displayName;
        public Color Color => color;
        public Sprite Icon => icon;
    }

    // ################################
    // Interfaces
    // ################################

    public interface IEnergyAccumulator {
        void AccumulateEnergy(ElementData element, float amount);
    }

    // ################################
    // Components
    // ################################

    public class EnergyPatch : MonoBehavior {
        [SerializeField] private float transferAmount;
        [SerializeField] private ElementData data;

        private void OnTriggerEnter(Collider other) {
            var accumulator = other.GetComponent<IEnergyAccumulator>();
            if(accumulator != null) {
                ProcessEnergyTransfer(accumulator);
            }
        }

        private void OnTriggerStay(Collider other) {
            var accumulator = other.GetComponent<IEnergyAccumulator>();
            if(accumulator != null) {
                ProcessEnergyTransfer(accumulator);
            }
        }

        private void ProcessEnergyTransfer(IEnergyAccumulator accumulator) {
            var amount = transferAmount * Time.deltaTime;
            accumulator.AccumulateEnergy(element, amount);
        }
    }
}