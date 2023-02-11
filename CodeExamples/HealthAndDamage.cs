using UnityEngine;

namespace hinos.damage {
    public interface IDamageable {
        void ApplyDamage(float amount, ElementType element);
    }

    public class HitBox : MonoBehaviour, IDamageable {

    }

    public class CharacterData : ScriptableObject {
        private float vitality;

        private float baseHeatResistance;
        private float baseColdResistance;
        private float baseKineticResistance;
        private float baseStaticRestistance;
    }

    public class CharacterInstance {
        private Stat vitality;
        private Stat damageMultiplier

        private Dictionary<ElementType, Stat> resitances;

        public CharacterInstance(CharacterData data) {
            vitality = new Stat(data.Vitality)

            resitances[ElementType] = new Stat(data.HeatResistance);
        }

        public float GetResistance(ElementType type){
            if(resitances.ContainsKey(type)) {
                return heatResistanceStat.GetValue();
            }

            return 1;
        }
    }

    public class DamageInfo {
        public float value;
        public ElementData type;
        public float accumulationModifier;
    }

    public class DamageProcessor : MonoBehaviour {

        public void ApplyDamage(DamageInfo damage) {

        }
    }

    public class Health : MonoBehaviour {
        public float value;
    }
}