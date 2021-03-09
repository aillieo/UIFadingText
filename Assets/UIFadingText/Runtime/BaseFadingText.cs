using System.Collections;
using UnityEngine;

namespace AillieoUtils.UI
{
    public abstract class BaseFadingText: MonoBehaviour
    {
        public float smoothFactor = 1f;

        public bool useAlphaOnly = true;

        [Range(0, 1)][SerializeField]
        private float mValue = 0.5f;
        public float Value
        {
            get
            {
                return mValue;
            }
            set
            {
                if (mValue != value)
                {
                    mValue = value;
                    OnValueChanged();
                }
            }
        }

        [SerializeField]
        private Color mStartColor = Color.white;
        public Color startColor
        {
            get
            {
                return mStartColor;
            }
            set
            {
                if (mStartColor != value)
                {
                    mStartColor = value;
                    OnValueChanged();
                }
            }
        }

        [SerializeField]
        private Color mEndColor = Color.black;
        public Color endColor
        {
            get
            {
                return mEndColor;
            }
            set
            {
                if (mEndColor != value)
                {
                    mEndColor = value;
                    OnValueChanged();
                }
            }
        }

        protected float InternalEvaluate(int index, int total)
        {
            int idx = index + 1;
            float rate = (float)idx / total;
            float innerRate = Mathf.Clamp01((rate - Value) * total);
            return innerRate;
        }

        protected virtual float EvaluateValueForChar(int index, int total)
        {
            float v = InternalEvaluate(index, total);

            if (0 < v && v < 1)
            {
                v = v * smoothFactor;
            }

            return 1 - v;
        }

        protected Color EvaluateColorForChar(int index, int total)
        {
            float v = EvaluateValueForChar(index, total);
            return Color.Lerp(startColor, endColor, v);
        }

        protected abstract void OnValueChanged();
    }
}
