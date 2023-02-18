using UnityEngine;

namespace hinos.damage {
    // ###################################
    // Interfaces
    // ###################################

    public interface IDamageable {
        void ApplyDamage(float amount, ElementType element);
    }

    // ###################################
    // Domain Models
    // ###################################

    public class DamageInfo {
        public float value;
        public ElementData type;
        public float accumulationModifier;
    }

    // ###################################
    // Controllers
    // ###################################

    public class DamageController {
        public Status status;

        public void ApplyDamage(DamageInfo data) {
            var defenseModifier = status.defenseStat.GetValue();
            var resistanceModifier = 1f;

            var resistance = status.GetResistance(data.element);
            if(resistance != null) {
                resistanceModifier = resistance.GetValue();
            }

            status.health -= data.damage * defenseModifier * resistanceModifier;
        }
    }
}