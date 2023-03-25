using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hinos.interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Vector3 _point;
        [SerializeField] private Vector3 _bounds;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private int _pollCount;

        private int _count;
        private Collider[] _results;

        private Interactable _closestInteractable;

        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();

            _results = new Collider[_pollCount];
        }

        void Update() {
            var origin = _transform.TransformPoint(_point);
            var halfBounds = _bounds * 0.5f;
            
            //QueryInteractables(origin, halfBounds, out var interactables);

            // Get the interactable from the scene
            var results = Physics.OverlapBox(origin, halfBounds, Quaternion.identity, _mask, QueryTriggerInteraction.Ignore);
            if(results.Length > 0) {
                if(results[0].TryGetComponent<Interactable>(out var interactable)) {
                    _closestInteractable = interactable;
                }
            }

        }

        private void OnGUI() {
            GUILayout.BeginArea(new Rect(10, 10, 100, 100));
            GUILayout.Label("Results");
            for(var i = 0; i < _count; i++) {
                GUILayout.Label(_results[i].name);
            }
            GUILayout.EndArea();
        }

        private void OnDrawGizmos() {
            if (_transform == null) return;

            Gizmos.color = (_count > 0) ? Color.green : Color.red;
            Gizmos.DrawWireCube(_transform.TransformPoint(_point), _bounds * 2);
        }

        private void QueryInteractables(Vector3 point, Vector3 halfBounds, out Interactable[] interactables) {
            interactables = new Interactable[_pollCount];
            
            var results = new Collider[_pollCount];
            var count = Physics.OverlapBoxNonAlloc(point, halfBounds, results, Quaternion.identity, _mask, QueryTriggerInteraction.Ignore);
            for(var i = 0; i < count; i++) {
                if(results[i].TryGetComponent<Interactable>(out var interactable)) {
                    interactables[i] = interactable;
                }
            }

            //Array.Sort(interactables, (x, y) => Vector3.Distance(_transform.position, x.transform.position) < Vector3.Distance(_transform.position, y.transform.position));
        }

        private void ProcessInteractables() {

        }
    }
}