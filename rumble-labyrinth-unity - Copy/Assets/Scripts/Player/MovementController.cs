using UnityEngine;

namespace hinos.player
{
    public class MovementController
    {
        public Vector3 CalculateVelocityChange(Vector3 velocity, Vector3 targetVelocity, float maxSpeedChange, Vector3 normal) {
            var projectedZ = GetProjectedDirection(Vector3.forward, normal);
            var projectedX = GetProjectedDirection(Vector3.right, normal);

            var currentZ = Vector3.Dot(velocity, projectedZ);
            var currentX = Vector3.Dot(velocity, projectedX);

            var newZ = Mathf.MoveTowards(currentZ, targetVelocity.z, maxSpeedChange);
            var newX = Mathf.MoveTowards(currentX, targetVelocity.x, maxSpeedChange);

            return projectedZ * (newZ - currentZ) + projectedX * (newX - currentX);
        }

        public Vector3 GetProjectedDirection(Vector3 axis, Vector3 normal) {
            return axis - normal * Vector3.Dot(axis, normal);
        }
    }
}

