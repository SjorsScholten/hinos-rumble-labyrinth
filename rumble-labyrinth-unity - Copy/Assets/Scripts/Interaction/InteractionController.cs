using System;
using UnityEngine;

namespace hinos.interaction
{
    public class InteractionController
    {
        public void SortInteractablesByDistance(Vector3 position, InteractableData[] interactables) {
            Array.Sort<InteractableData>(interactables, (x, y) => CompareDistanceToOrigin(position, x.transform.position, y.transform.position));
        }

        public int CompareDistanceToOrigin(Vector3 origin, Vector3 pointA, Vector3 pointB) {
            var distToA = Vector3.Distance(origin, pointA);
            var distToB = Vector3.Distance(origin, pointB);
            return distToA.CompareTo(distToB);
        }
    }
}

