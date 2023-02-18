namespace hinos.status {

    // ###################################
    // Domain Models
    // ###################################

    [System.Serializable]
    public class Status {
        /* Health attributes */
        public StatAttribute vitalityStat;
        public float health;

        /* Movement attributes */
        public StatAttribute mobilityStat;

        /* Offensive attributes */
        public StatAttribute attackStat;
        public StatAttribute accuracyStat;
        public StatAttribute offensiveCriticalStat;

        /* Defensive attributes */
        public StatAttribute defenseStat;
        public StatAttribute evasionStat;
        public StatAttribute vulnerabilityStat;
        public Dictionary<ElementData, StatAttribute> resistanceStats = new();
        public StatAttribute defensiveCriticalStat;

        /* Status Conditions */
        private readonly HashSet<statusConditions> statusConditons = new();
        private readonly Dictionary<ElementData, float> accumulatedEnergy = new();

        public void ApplyStatusCondition(StatusCondition condition) {
            if(statusConditions.TryGetValue(condition, out var result)){
                result.Upgrade(condition);
            }
            else {
                statusConditions.Add(conditon);
            }
        }

        public void RemoveStatusCondition(StatusCondition condition) {
            statusConditions.Remove(condition);
        }

        public StatAttribute GetResistance(ElementData element) {
            if(resistanceStats.TryGetValue(element, out var stat)){
                return stat;
            }
            else {
                return null;
            }
        }

        public float GetAccumulatedEnergy(ElementData element) {
            if(accumulatedEnergy.TryGetValue(element, out var amount)) {
                return amount;
            }
            else {
                return 0;
            }
        }
    }

    [System.Serializable]
    public class StatAttribute {
        private readonly float baseValue;
        private readonly List<StatModifier> modifiers;

        private float finalValue;
        private bool isDirty;

        public StatAttribute(float baseValue) {
            this.baseValue = baseValue;
            this.modifiers = new();
        }

        private float CalculateFinalValue() {
            var result = baseValue;

            for(var i = 0; i < modifiers.Count; ++i) {
                result = modifiers[i].CalculateModifiedValue(result);
            }

            return (float)Math.Round(result, 4);
        }

        public void AddModifier(StatModifier modifier) {
            isDirty = true;
            modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier) {
            isDirty = true;
            modifiers.Remove(modifier);
        }

        public void GetValue() {
            if(isDirty) {
                finalValue = CalculateFinalValue();
                isDirty = false;
            }

            return finalValue;
        }
    }

    [System.Serializable]
    public class StatusCondition {
        private int stack;
        private float duration;
        private float lifeTime = 0;

        public StatusCondition(float duration, int stack) {
            this.duration = duration;
            this.stack = stack;
        }

        public void Tick() {
            lifeTime += Time.deltaTime;
        }

        public bool IsExpired() {
            return lifeTime > duration;
        }

        public void Upgrade(StatusCondition condition) {
            this.stack += condition.stack;
            lifeTime = 0f;
        }
    }

    [System.Serializable]
    public abstract class StatModifier {
        public readonly float value;
        public readonly int order;
        public readonly object source;

        public StatModifier(float value, int order, object source) {
            this.value = value;
            this.order = order;
        }

        public abstract float CalculateModifiedValue(float value);
    }

    public class Flat_StatModifier : StatModifier {

        public Flat_StatModifier(float value, int order, object source) : base(value, order, source){

        }

        public override float CalculateModifiedValue(float value) {
            return value + this.value;
        }
    }

    public class Percent_StatModifier : StatModifier {

        public Percent_StatModifier(float value, int order, object source) : base(value, order, source) {

        }

        public override float CalculateModifiedValue(float value) {
            return value * (1 + this.value);
        }
    }

    // ###################################
    // Controllers
    // ###################################

    public class StatusController {
        public Status status;

        public void HandleStatusConditionTick() {
            var conditions = status.GetConditions();
            for(var i = 0; i < conditions.Count; i++) {
                conditions[i].Tick();
            }

            for(var i = conditions.count - 1; i > 0; i--) {
                if(conditions[i].IsExpired()){
                    status.RemoveStatusCondition(conditions[i]);
                }
            }
        }
    }

    public class BurnStatusController {
        public Status status;

        public void ApplyBurnStatus() {
            
        }

        public void HandleBurnDamage() {
            status.health -= 
        }
    }
    
    // ###################################
    // Components
    // ###################################

    public class Burnable : MonoBehaviour {
        [SerializeField] private ElementData burnElement;
        [SerializeField] private float burnThreshold;
        public bool canBurn = true;
        public bool isBurning = false;

        private BurnStatusController controller;

        private Status myStatus;

        private void Awake() {
        }

        private void Update() {
            if(canBurn) {
                if(accumulator.value >= burnThreshold) {
                    isBurning = true;
                }
            }

            if(isBurning) {
                controller.HandleBurnDamage();
            }
        }

        private void ProcessBurnApplication() {

        }

        private void ProcessBurnTick() {

        }
    }

    // ###################################
    // Views
    // ###################################

    public class StatusConditionGUI : MonoBehaviour {
        [SerializeField] private ProgressBar accumulationBar;
        [SerializeField] private ProgressBar statusActivebar;
        [SerializeField] private TMPro_Text stackCounter;
    }
}