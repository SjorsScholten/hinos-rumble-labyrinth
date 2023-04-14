using System;
using UnityEngine;

namespace hinos.interaction
{
    public class InteractionBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _origin = default;
        [SerializeField] private Vector3 _boxSize = Vector3.one * 0.5f;
        [SerializeField] private LayerMask _layerMask = -1;
        [SerializeField] private int _queryCount = 5;

        private Vector3 _halfExtends;
        private int _count;
        private Interactable[] _interactables;

        private void Awake() {
            _count = 0;
            _interactables = new Interactable[_queryCount];
            _halfExtends = CalculateHalfExtends(_boxSize);
        }

        private void FixedUpdate() {
            Clear();
            _count = QueryInteractables(_interactables);
        }

        private void Clear() {
            Array.Clear(_interactables, 0, _count);
            _count = 0;
        }

        private int QueryInteractables(Interactable[] interactables) {
            var results = new Collider[interactables.Length];
            var resCount = Physics.OverlapBoxNonAlloc(_origin.position, _halfExtends, results, Quaternion.identity, _layerMask, QueryTriggerInteraction.Ignore);
            
            var count = 0;
            for(var i = 0; i < resCount; i += 1) {
                if(results[i].TryGetComponent<Interactable>(out var interactable)) {
                    interactables[count] = interactable;
                    count += 1;
                }
            }

            return count;
        }

        private Vector3 CalculateHalfExtends(Vector3 boxSize) => boxSize * 0.5f;

        private void SetBoxSize(Vector3 boxSize) {
            _boxSize = boxSize;
            _halfExtends = CalculateHalfExtends(_boxSize);
        }

    }
}
