using System;
using UnityEngine;

namespace hinos.interaction
{
    public class InteractionController
    {
        public void BeginInteraction(object source, IInteractionHandler handler) {
            if(handler.IsInteractable) {
                handler.HandleInteraction(source);
            }
        }

        public int QueryInteractables(Vector3 center, Vector3 halfExtends, InteractableData[] interactables, int mask = -1) {
            var resultSet = new Collider[interactables.Length];
            var count = Physics.OverlapBoxNonAlloc(center, halfExtends, resultSet, Quaternion.identity, mask, QueryTriggerInteraction.Ignore);

            for(var i = count - 1; i >= 0; i--) {
                if(resultSet[i].TryGetComponent<IInteractionHandler>(out var interactable)) {
                    interactables[i] = new InteractableData(resultSet[i], interactable);
                }
                else {
                    count -= 1;
                }
            }

            return count;
        }

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

