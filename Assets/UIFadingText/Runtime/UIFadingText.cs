using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AillieoUtils.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class UIFadingText : BaseFadingText, IMeshModifier
    {
        Text mGraphic;
        public Text graphic
        {
            get
            {
                if(mGraphic == null)
                {
                    mGraphic = GetComponent<Text>();
                }
                return mGraphic;
            }
        }

        private static readonly List<UIVertex> list = new List<UIVertex>();

        public void ModifyMesh(VertexHelper vh)
        {
            list.Clear();
            vh.GetUIVertexStream(list);

            int total = list.Count / 6;

            for (int i = 0; i < list.Count; ++i)
            {
                UIVertex u = list[i];
                int idx = i / 6;
                Color c;

                if (useAlphaOnly)
                {
                    c = u.color;
                    c.a = EvaluateValueForChar(idx, total);
                }
                else
                {
                    c = EvaluateColorForChar(idx, total);
                }

                u.color = c;
                list[i] = u;
            }

            vh.Clear();
            vh.AddUIVertexTriangleStream(list);
            list.Clear();
        }

        public void ModifyMesh(Mesh mesh)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnValueChanged()
        {
            graphic.SetVerticesDirty();
        }
    }
}
