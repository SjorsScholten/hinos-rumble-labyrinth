using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hinos.character {
    public interface IMovementHandler {
        float Speed { get; }
        float SpeedChange { get; }

        void HandleMove(Vector3 direction);
    }

    public interface IMovementController {
        void Move();
    }

    public class CharacterMovement : MonoBehaviour {
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 10f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Vector3 _groundNormal = Vector3.up;

        private Vector3 _desiredVelocity;
        private float _desiredSpeed;

        // Input
        private Vector3 _moveDirection;
        private float _moveValue;
        private bool _moveRequested;

        // Components
        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            _desiredVelocity = _moveDirection * _speed;
        }

        private void FixedUpdate() {
            var maxSpeedChange = GetMaxSpeedChange(_moveRequested);
            ProcessMovement(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);
        }

        private void ProcessMovement(Vector3 velocity, Vector3 targetVelocity, float maxSpeedChange) {
            var lonVelChange = CalculateVelocityChange(velocity, Vector3.forward, targetVelocity.z, maxSpeedChange);
            var latVelChange = CalculateVelocityChange(velocity, Vector3.right, targetVelocity.x, maxSpeedChange);
            _rigidbody.velocity += lonVelChange + latVelChange;
            _moveRequested = false;
        }

        public Vector3 CalculateVelocityChange(Vector3 velocity, Vector3 axis, float targetSpeed, float maxSpeedChange) {
            var projectedDirection = GetProjectedDirection(axis);
            var speedOnAxis = Vector3.Dot(velocity, projectedDirection);
            var newSpeed = Mathf.MoveTowards(speedOnAxis, targetSpeed, maxSpeedChange);
            return projectedDirection * (newSpeed - speedOnAxis);
        }

        public Vector3 GetProjectedDirection(Vector3 axis) {
            return axis - _groundNormal * Vector3.Dot(axis, _groundNormal);
        }

        public void HandleMove(Vector3 direction) {
            _moveDirection = direction;
            _moveRequested = true;
        }

        private float GetMaxSpeedChange(bool isMoving) {
            var maxSpeedChange = (isMoving) ? _acceleration : _deceleration;
            return maxSpeedChange * Time.deltaTime;
        }
    }

    public class CharacterMovementController : IMovementController {

        public void Move() {

        }
    }
}
