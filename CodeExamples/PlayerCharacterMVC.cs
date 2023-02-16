namespace hinos.player {
    public class CharacterData : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private string description;

        [SerializeField] private GameObject prefab;

        public string DisplayName => displayName;
        public string Description => description;
    }

    [System.Serializable]
    public class CharacterInstance {
        private CharacterData data;

        private StatAttribute vitalityStat;

        private StatAttribute mobilityStat;

        private StatAttribute attackStat;
        private StatAttribute accuracyStat;
        private StatAttribute offensiveCriticalStat;

        private StatAttribute defenseStat;
        private StatAttribute evasionStat;
        private StatAttribute vulnerabilityStat;
        private StatAttribute heatResistanceStat;
        private StatAttribute coldResistanceStat;
        private StatAttribute kineticResistanceStat;
        private StatAttribute staticResistanceStat;
        private StatAttribute defensiveCriticalStat;

        private float health;

        public CharacterInstance(CharacterData data) {
            this.data = data;
        }


    }

    [System.Serializable]
    public class Loadout {
        private Armor armor;
        private Weapon weapon;
        private Trinket trinket;

        public event Action<Armor> OnArmorChangedEvent;

        public void DonArmor(Armor armor) {
            
        }
    }

    public class PlayerController {
        public CharacterInstance character;
        public Loadout loadout;

        public PlayerController() {

        }

        public void HandleHit(){

        }

        public void HandleDonEquipment() {
            character.defenseStat.AddModifier
        }

        public void HandleDoffEquipment() {

        }
    }

    public class PlayerCharacter : MonoBehaviour, IStateMachine<PlayerCharacter> {
        private PlayerController controller;
        public CharacterInstance character;

        private PlayerStateFactory playerStateFactory;
        private State<PlayerCharacter> currentState;

        private Rigidbody myRigidbody;
        private Interactor myInteractor;
        private ItemContainer myItemContainer;

        private void Awake() {
            myRigidbody = GetComponent<Rigidbody>();
            myInteractor = GetComponent<Interactor>();
            myItemContainer = GetComponent<ItemContainer>();

            controller = new PlayerController();

            playerStateFactory = new PlayerStateFactory(this);
            currentState = playerStateFactory.GetIdleState();
        }

        private void Update() {
            currentState.Update();
        }

        private void FixedUpdate() {
            var velocity = myRigidbody.velocity;

            velocity = Vector3.MoveTowards(velocity, desiredVelocity, maxSpeedChange);

            myRigidbody.velocity = velocity;
        }

        public void OnMove(InputAction.CallbackContext context) {
            var moveInputVector = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context) {

        }

        public void OnInteract(InputAction.CallbackContext context) {
            if(context.performed) {
                ProcessInteraction();
            }
        }

        public void SwitchState(State<PlayerCharacter> oldState, State<PlayerCharacter> newState) {
            oldState.Exit();
            newState.Enter();
            currentState = newState;
        }
    }

    public class PlayerStateFactory {
        private readonly PlayerIdleState idleState;

        public PlayerStateFactory(playerCharacter source) {
            idleState = new Idle_PlayerState(source, this);
        }

        public PlayerIdleState GetIdleState() => idleState;
    }

    public abstract class PlayerState : State<PlayerCharacter> {
        protected readonly PlayerStateFactory factory;
        
        public PlayerState(PlayerCharacter source, PlayerStateFactory factory, string name) : base(source, name) {
            this.factory = factory;
        }
    }

    public class Idle_PlayerState : PlayerState {

        public Idle_PlayerState(PlayerCharacter source, PlayerStateFactory factory) : base(source, factory, "Idle") {

        }

        public override void Enter() {

        }

        public override void Exit() {

        }

        public override void Update() {

        }

        public override void ChangeState() {

        }
    }

}