using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hinos.character {
    public class CharacterMovement : MonoBehaviour {
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 10f;
        [SerializeField] private float _speed = 10f;

        private Vector3 _moveDirection;
        private bool _moveRequested;

        private Vector3 _velocity, _desiredVelocity;

        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            _desiredVelocity = _moveDirection * _speed;
        }

        private void FixedUpdate() {
            var maxSpeedChange = GetMaxSpeedChange(_moveRequested);
            ProcessMovement(maxSpeedChange);
        }

        private void ProcessMovement(float maxSpeedChange) {
            _velocity = _rigidbody.velocity;
            _velocity = Vector3.MoveTowards(_velocity, _desiredVelocity, maxSpeedChange);

            _rigidbody.velocity = _velocity;
            _moveRequested = false;
        }

        public void HandleMove(Vector3 direction) {
            _moveDirection = direction;
            _moveRequested = true;
        }

        public void HandleRotate() {

        }

        private float GetMaxSpeedChange(bool isMoving) {
            var maxSpeedChange = (isMoving) ? _acceleration : _deceleration;
            return maxSpeedChange * Time.deltaTime;
        }
    }
}
