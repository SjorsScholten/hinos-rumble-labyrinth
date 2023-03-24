namespace hinos.movement {
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement {
        [SerializeField] private float maxGroundAngle = 25.0f;
        [SerializeField] private float maxStairAngle = 50.0f;
        [SerializeField] private float stairMask = -1;

        private float minGroundDotProduct = 0.0f, minStairDotProduct = 0.0f;
        private int stepsSinceLastGrounded;
        private int groundContactCount, steepContactCount;
        private Vector3 contactNormal, steepNormal;

        public Vector3 ContactNormal = contactNormal;
        public int StepsSinceLastGrounded => stepsSinceLastGrounded;
        public bool OnGround => goundContactCount > 0;
        public bool OnSteep => steepContactCount > 0;

        private Vector3 velocity;

        private Rigidbody myRigidbody;
        private MovementBehaviour[] movementBehaviours;

        public Vector3 Velocity => velocity;

        private void Awake(){
            myRigidbody = GetComponent<Rigidbody>();

            movementBehaviours = GetComponents<MovementBehaviour>();

            minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            minStairDotProduct = Mathf.Cos(maxStairAngle * Mathf.Deg2Rad);
        }

        private void FixedUpate() {
            UpdateState();

            ProcessSteepContacts();
            ProcessGroundContacts();

            ProcessMovementBehaviours();

            ClearState();
        }

        private void OnCollisionEnter(Collision collision) {
            EvaluateCollision(collision);
        }

        private void OnCollisionStay(Collision collision) {
            EvaluateCollision(Collision);
        }

        private void EvaluateCollision(Collision collision) {
            for(int i = 0; i < collision.contactCount; ++i) {
                var normal = collision.GetContact(i).normal;
                var minDot = (stairMask & (1 << collision.gameObject.layer)) == 0 ? minGroundDotProduct : minStairDotProduct;

                // contact is ground
                if(normal.y >= minDot) {
                    groundContactCount += 1;
                    contactNormal += normal;
                    continue;
                }
                
                // contact is steep
                if (normal.y > -0.01f) {
                    steepContactCount += 1;
                    steepNormal += normal;
                    continue;
                }
            }
        }

        private void UpdateState() {
            velocity = myRigidbody.velocity;
            stepsSinceLastGrounded += 1;
        }

        private void ClearState() {
            groundContactCount = steepContactCount = 0;
            contactNormal = steepNormal = Vector3.zero;
        }

        private void ProcessSteepContacts() {
            if(steepContactCount > 1) {
                steepNormal.Normalize();
                if(steepNormal.y > minGroundDotProduct) {
                    groundContactCount = 1;
                    contactNormal = steepNormal;
                }
            }
        }

        private void ProcessGroundContacts() {
            if(groundContactCount > 0) {
                stepsSinceLastGrounded = 0;
                if(contactCount > 1) {
                    contactNormal.Normalize();
                }
            }
            else {
                contactNormal = Vector3.up;
            }
        }

        private void ProcessMovementBehaviours() {
            for(var i = 0; i < movementBehaviours.Length; ++i) {
                if(!movementBehaviours[i].enabled) continue;
                velocity += movementBehaviours[i].CalculateVelocityChange();
            }
        }

        public static CalculateMovementAlongAxis(Vector3 currentVelocity, float desiredSpeed, float maxSpeedChange, Vector3 axis, Vector3 normal = Vector3.up) {
            var direction = axis - normal * Vector3.Dot(axis, normal);
            var currentSpeed = Vector3.Dot(currentVelocity, direction);
            var newSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, maxSpeedChange);
            return direction * (newSpeed - currentSpeed);
        }
    }

    [RequireComponent(typeof(CharacterController))]
    public abstract class MovementBehaviour : MonoBehaviour {
        protected CharacterController myCharacterController;

        private void Awake() {
            myCharacterController = GetComponent<CharacterController>();
        }

        public abstract Vector3 CalculateVelocityChange();
    }

    public class CharacterMovement : MovementBehaviour {
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;

        [HideInInspector] public Vector3 inputDirection;

        public override Vector3 CalculateVelocityChange() {
            var velocityChange = Vector3.zero;
            velocityChange += CharacterController.CalculateMovementAlongAxis(myCharacterController.velocity, maxSpeed, acceleration, Vector3.right, myCharacterController.ContactNormal);
            velocityChange += CharacterController.CalculateMovementAlongAxis(myCharacterController.velocity, maxSpeed, acceleration, Vector3.forward, myCharacterController.contactNormal);
            return velocityChange;
        }
    }

    public class CharacterJump : MovementBehaviour {

    }
}