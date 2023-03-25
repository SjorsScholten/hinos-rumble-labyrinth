using System;
using UnityEngine;

namespace hinos.item
{
    public class HoldableItem : MonoBehaviour {
        public bool droppable;
        public bool stowable;
        public bool throwable;

        public bool CanBeDiscarded() {
            return !droppable && !throwable;
        }

        public void HandleOnHold() {

        }

        public void HandleOnDrop() {

        }
    }
}