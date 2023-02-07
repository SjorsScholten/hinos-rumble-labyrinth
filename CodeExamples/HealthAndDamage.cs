using UnityEngine;

namespace hinos.damage {
    public class ElementType : ScriptableObject {
        [SerializeField] private string elementName;

        public string Name => elementName;
    }

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

    public class LivingEntity : MonoBehaviour {
        [SerializeField] private float maxValue;
        private float value;

        public void ApplyValue(float value) {
            var diff = this.value + value;

            if(diff > maxHealth) {
                health = maxHealth;
                return diff - maxHealth;
            }

            else if(diff < 0) {
                health = 0;
                return diff;
            }

            return 0;
        }
    }
}