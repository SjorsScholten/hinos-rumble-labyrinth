namespace hinos.statusConditions {

    public abstract class StatusCondition {
        protected GameObject affectedObject;
        protected float duration;
        protected int stack;

        protected float lifeTime = 0;

        public StatusCondition(GameObject affectedObject, float duration, int stack) {
            this.affectedObject = affectedObject;
            this.duration = duration;
            this.stack = stack;
        }

        public void Tick() {
            lifeTime += Time.deltaTime;
            OnTick();
        }

        public bool IsExpired() {
            return lifeTime > duration;
        }

        protected abstract void OnTick();

        public virtual void Upgrade(StatusCondition condition) {
            this.stack += condition.stack;
        }
    }

    public class Burned_StatusCondition : StatusCondition {
        private DamageHandler damageHandler;

        public Burned_StatusCondition(GameObject affectedObject, float duration, int stack) : base(affectedObject, duration, stack) {
            damageHandler = affectedObject.GetComponent<DamageHandler>();
        }

        protected override OnTick() {
            damageHandler.ApplyDamage();
        }
    }

    public class Status : MonoBehaviour {
        private readonly HashSet<statusConditions> statusConditons = new();

        private void Update() {
            ProcessStatusConditionTick();
            ProcessStatusConditionDissolve();
        }

        private void ProcessStatusConditionTick() {
            for(var i = 0; i < statusConditions.Count; i++) {
                statusConditions[i].Tick();
            }
        }

        private void ProcessStatusConditionDissolve() {
            statusConditions.RemoveWhere(x => x.IsExpired());
        }

        public void ApplyStatusCondition(StatusCondition condition) {
            if(statusConditions.TryGetValue(condition, out var result)){
                result.Upgrade(condition);
            }
            else {
                statusConditions.Add(conditon);
            }
        }
    }

    [RequireComponent(typeof(ElementAccumulator))]
    public class Burnable : MonoBehaviour {
        [SerializeField] private ElementData burnElement;
        [SerializeField] private float burnThreshold;
        public bool canBurn = true;

        private Status myStatus;
        private ElementAccumulator myAccumulator;

        private void Awake() {
            var accumulationManager = GetComponent<AccumulatorManager>();
            myAccumulator = accumulationManager.GetAccumulatorOfElement(burnElement);
        }

        private void Update() {
            if(canBurn) {
                if(accumulator.value >= burnThreshold) {
                    isBurning = true;
                }
            }


        }
        
    }
}