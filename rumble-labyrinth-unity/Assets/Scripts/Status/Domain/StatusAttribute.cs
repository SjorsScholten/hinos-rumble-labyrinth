using System;
using System.Collections.Generic;

namespace hinos.status {
    
    [System.Serializable]
    public class StatusAttribute : IComparer<StatusModifier> {
        private float baseValue;

        private List<StatusModifier> modifiers;
        private float modifiedValue;
        private bool isDirty;

        public float BaseValue {
            get => this.baseValue;
        }

        public StatusAttribute(float baseValue) {
            this.baseValue = baseValue;
            this.modifiers = new List<StatusModifier>();
            this.modifiedValue = baseValue;
            this.isDirty = false;
        }

        private float CalculateModifiedValue() {
            var mv = baseValue;
            var tempPercent = 0f;

            for(var i = 0; i < modifiers.Count; i++){
                var mod = modifiers[i];

                if(mod.type == StatusModifierType.FLAT) {
                    mv += modifiers[i].value;
                }
                else if(mod.type == StatusModifierType.PERCENT_ADDITIVE) {
                    tempPercent += mod.value;

                    if(i + 1 > modifiers.Count || modifiers[i + 1].type != StatusModifierType.PERCENT_ADDITIVE) {
                        mv *= 1 + tempPercent;
                        tempPercent = 0f;
                    }
                }
            }

            return mv;
        }

        public int Compare(StatusModifier x, StatusModifier y) {
            return x.order.CompareTo(y.order);
        }

        public float GetValue() {
            if(isDirty) {
                modifiedValue = CalculateModifiedValue();
                isDirty = false;
            }

            return modifiedValue;
        }

        public void AddModifier(StatusModifier modifier) {
            modifiers.Add(modifier);
            modifiers.Sort(this);
            isDirty = true;
        }

        public void RemoveModifier(StatusModifier modifier) {
            if(modifiers.Remove(modifier)) {
                modifiers.Sort(this);
                isDirty = true;
            }
        }

        public void RemoveAllModifiersFromSource(object source) {
            for (var i = modifiers.Count - 1; i >= 0; i--) {
                if(modifiers[i].source == source){
                    RemoveModifier(modifiers[i]);
                }
            }
        }
    }
}