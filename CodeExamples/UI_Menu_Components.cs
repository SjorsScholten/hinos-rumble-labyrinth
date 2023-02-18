namespace hinos.UI {
    public class GridLayoutGroup {

    }

    public class GridLayoutCell {
        public int colSpan, rowSpan;
    }

    public class ProgressBar : MonoBehaviour {
        [SerializeField] private Image fillMask;
        [SerializeField] private float lowerBound, upperBound;
        [SerializeField] private float currentValue;
        public float fillSettleTime;

        private float percentValue;
        private float fillSettleVelocity;

        public float Value {
            get => currentValue;
            set {
                currentValue = value;
                percentValue = CalculatePercentValue();
            }
        }

        public float LowerBound {
            get => lowerBound;
            set {
                lowerBound = value;
                percentValue = CalculatePercentValue();
            }
        }

        public float UpperBound {
            get => upperBound;
            set {
                upperBound = value;
                percentValue = CalculatePercentValue();
            }
        }

        private void OnValidate() {
            if(fillMask != null) {
                fillMask.type = Image.Type.Filled;
                percentValue = CalculatePercentValue();
                fillMask.fillAmount = percentValue;
            }
        }

        private void Update() {
            if(!Mathf.Approximately(percentValue, fillMask.fillAmount)) {
                fillMask.fillAmount = Mathf.SmoothDamp(fillMask.fillAmount, percentValue, ref fillSettleVelocity, fillSettleTime);
            }
        }

        public float CalculatePercentValue() {
            var currentOffset = currentValue - lowerBound;
            var maximumOffset = upperBound - lowerBound;
            return currentOffset / maximumOffset;
        }
    }
}