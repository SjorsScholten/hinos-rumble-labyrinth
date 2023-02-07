using UnityEngine;

namespace hinos.statAttributes {
    private class StatAttribute {
        private readonly float baseValue;
        private readonly List<StatModifier> modifiers;

        private float finalValue;
        private bool isDirty;

        public StatAttribute(float baseValue) {
            this.baseValue = baseValue;
            this.modifiers = new();
        }

        private float CalculateFinalValue() {
            var result = baseValue;

            for(var i = 0; i < modifiers.Count; ++i) {
                result = modifiers[i].CalculateModifiedValue(result);
            }

            return (float)Math.Round(result, 4);
        }

        public void AddModifier(StatModifier modifier) {
            isDirty = true;
            modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier) {
            isDirty = true;
            modifiers.Remove(modifier);
        }

        public void GetValue() {
            if(isDirty) {
                finalValue = CalculateFinalValue();
                isDirty = false;
            }

            return finalValue;
        }
    }

    public abstract class StatModifier {
        public readonly float value;
        public readonly int order;
        public readonly object source;

        public StatModifier(float value, int order, object source) {
            this.value = value;
            this.order = order;
        }

        public abstract float CalculateModifiedValue(float value);
    }

    public class Flat_StatModifier : StatModifier {

        public Flat_StatModifier(float value, int order, object source) : base(value, order, source){

        }

        public override float CalculateModifiedValue(float value) {
            return value + this.value;
        }
    }

    public class Percent_StatModifier : StatModifier {

        public Percent_StatModifier(float value, int order, object source) : base(value, order, source) {

        }

        public override float CalculateModifiedValue(float value) {
            return value * (1 + this.value);
        }
    }
}