using System;
using UnityEngine;

namespace hinos.movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidMovementBehaviour : MonoBehaviour
    {
        private Vector3 _targetVelocity;
        private float _maxSpeedChange;
        private Vector3 _surfaceNormal;

        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate() {
            ProcessMovement();
        }

        private void ProcessMovement() {
            var velocityChange = CalculateVelocityChange(_rigidbody.velocity, _targetVelocity, _maxSpeedChange, _surfaceNormal);
            _rigidbody.velocity += velocityChange;
        }

        private Vector3 CalculateVelocityChange(Vector3 velocity, Vector3 targetVelocity, float maxSpeedChange, Vector3 normal) {
            var projectedZ = GetProjectedDirection(Vector3.forward, normal);
            var projectedX = GetProjectedDirection(Vector3.right, normal);

            var currentZ = Vector3.Dot(velocity, projectedZ);
            var currentX = Vector3.Dot(velocity, projectedX);

            var newZ = Mathf.MoveTowards(currentZ, targetVelocity.z, maxSpeedChange);
            var newX = Mathf.MoveTowards(currentX, targetVelocity.x, maxSpeedChange);

            return projectedZ * (newZ - currentZ) + projectedX * (newX - currentX);
        }
        private Vector3 GetProjectedDirection(Vector3 axis, Vector3 normal) {
            return axis - normal * Vector3.Dot(axis, normal);
        }

        /// <summary>
        /// Will tell the behaviour to move along a surface with a velocity.
        /// </summary>
        /// <param name="targetVelocity">Desired move velocity</param>
        /// <param name="maxSpeedChange">Rate of change to reach velocity</param>
        /// <param name="surfaceNormal">Normal of the surface to move along</param>
        public void MoveOnSurface(Vector3 targetVelocity, float maxSpeedChange, Vector3 surfaceNormal) {
            _targetVelocity = targetVelocity;
            _maxSpeedChange = maxSpeedChange;
            _surfaceNormal = surfaceNormal;
        }
    }
}
