using UnityEngine;

namespace AillieoUtils.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class UIFadingTMPro : BaseFadingText
    {
        TMPro.TextMeshProUGUI mGraphic;
        public TMPro.TextMeshProUGUI graphic
        {
            get
            {
                if (mGraphic == null)
                {
                    mGraphic = GetComponent<TMPro.TextMeshProUGUI>();
                }
                return mGraphic;
            }
        }


        protected override void OnValueChanged()
        {
            graphic.ForceMeshUpdate();

            if (!isActiveAndEnabled)
            {
                return;
            }

            TMPro.TMP_TextInfo textInfo = graphic.textInfo;

            Color32[] vertColors;
            Color c;

            int total = textInfo.characterCount;
            for (int i = 0; i < total; ++i)
            {
                int mi = textInfo.characterInfo[i].materialReferenceIndex;
                vertColors = textInfo.meshInfo[mi].colors32;
                int vi = textInfo.characterInfo[i].vertexIndex;
                if (textInfo.characterInfo[i].isVisible)
                {
                    if(useAlphaOnly)
                    {
                        float a = EvaluateValueForChar(i, total);
                        for (int j = 0; j < 4; ++ j)
                        {
                            c = vertColors[vi + 0];
                            c.a = a;
                            vertColors[vi + j] = c;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            c = EvaluateColorForChar(i, total);
                            vertColors[vi + j] = c;
                        }
                    }
                }
            }

            graphic.UpdateVertexData(TMPro.TMP_VertexDataUpdateFlags.Colors32);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
        }
#endif

    }
}
