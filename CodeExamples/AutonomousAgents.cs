namespace hinos.agent {
    [RequireComponent(typeof(Rigidbody))]
    public class Agent : MonoBehaviour {

        private SteeringBehaviour[] steeringBehaviours;

        private Vector3 velocity;
        private float speed;
        
        private Transform myTransform;
        private Rigidbody myRigidbody;

        public Vector3 Position => myTransform.position;
        public Vector3 Heading => myTransform.forward;
        public Vector3 Velocity => myRigidbody.velocity;
        public float Speed => speed

        private void Awake() {
            myTransform = GetComponent<Transform>();
            myRigidbody = GetComponent<Rigidbody>();
            steeringBehaviours = GetComponents<SteeringBehaviour>();
        }

        private void FixedUpate() {
            velocity = myRigidbody.velocity;
            speed = velocity.magnitude;


            myRigidbody.velocity = velocity;
        }

        private Vector3 Calculate() {
            var steeringForce = Vector3.zero;

            for(var i = 0; i < steeringBehaviours.Length; ++i) {
                if(steeringBehaviours.enabled) {
                    if(!AccumulateForce(ref steeringForce, steeringBehaviours[i].Calculate)){
                        return steeringForce;
                    }
                }
            }

            return steeringForce;
        }

        private void AccumulateForce(ref Vector3 currentForce, Vector3 additiveForce) {
            var remainingForce = maxForce - currentForce.magnitude;
            if(remainingForce <= 0) return false;
            if(additiveForce.magnitude < remainingForce) {
                currentForce += additiveForce;
            }
            else {
                currentForce += additiveForce.normalized * remainingForce;
            }

            return true;
        }

        public Vector3 Seek(Vector3 targetPoint) {
            var diff = targetPoint - this.Position;
            var desiredVelocity = diff.normalized * maxSpeed;
            return desiredVelocity - this.Velocity;
        }

        public Vector3 Flee(Vector3 targetPoint, float panicDistance) {
            var diff = this.Position - targetPoint;

            if(diff.sqrMagnitude > panicDistance * panicDistance) {
                return Vector3.zero;
            }

            var desiredVelocity = diff.normalized * maxSpeed;
            return desiredVelocity - this.Velocity;
        }

        public Vector3 Pursuit(Agent targetAgent) {
            var diff = targetAgent.Position - this.Position;
            var relativeHeading = this.Heading.Dot(targetAgent.Heading);

            if(diff.Dot(this.Heading) && relativeHeading < -0.95) {
                return Seek(targetAgent.position);
            }

            var lookAheadTime = diff.magnitude / (maxSpeed + targetAgent.Speed);
            return Seek(this.Position + targetAgent.Velocity * lookAheadTime);
        }

        public Vector3 Evade(Agent targetAgent) {
            var diff = targetAgent.Position - this.Position;
            var lookAheadTime = diff.magnitude / (maxSpeed + targetAgent.Speed);
            return Flee(targetAgent.position + targetAgent.velocity * lookAheadTime);
        }
    }

    [RequireComponent(typeof(Agent))]
    public abstract class SteeringBehaviour : MonoBehaviour {
        protected Agent myAgent;

        private void Awake(){
            myAgent = GetComponent<Agent>();
        }

        public abstract Vector3 Calculate();
    }

    public class Seek_SteeringBehaviour : SteeringBehaviour {

        public override Vector3 Calculate() {
            return myAgent.Seek(myAgent.targetPoint);
        }
    }

    public class Flee_SteeringBehaviour : SteeringBehaviour {
        [SerializeField] private float panicDistance;

        public override Vector3 Calculate() {
            return myAgent.Flee(myAgent.targetPoint, panicDistance);
        }
    }

    public class Pursuit_SteeringBehaviour : SteeringBehaviour {

        public override Vector3 Calculate() {
            return myAgent.Pursuit(myAgent.targetAgent)
        }
    }
}