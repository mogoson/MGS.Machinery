/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorHoseEditor.cs
 *  Description  :  Editor for AnchorHose component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.CurveHose
{
    [CustomEditor(typeof(AnchorHose), true)]
    [CanEditMultipleObjects]
    public class AnchorHoseEditor : CurveHoseEditor
    {
        #region Field and Property
        protected new AnchorHose Target { get { return target as AnchorHose; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            if (Application.isPlaying)
            {
                return;
            }
            DrawAnchorCurveEditor();
        }

        protected override void DrawHoseCenterCurve()
        {
            Handles.color = Blue;
            var scaleDelta = Mathf.Max(Delta, Delta * GetHandleSize(Target.transform.position));
            for (float t = 0; t < Target.MaxKey; t += scaleDelta)
            {
                Handles.DrawLine(Target.GetPointAt(t), Target.GetPointAt(Mathf.Min(Target.MaxKey, t + scaleDelta)));
            }
        }

        protected void DrawAnchorCurveEditor()
        {
            for (int i = 0; i < Target.AnchorsCount; i++)
            {
                var anchorItem = Target.GetAnchorAt(i);
                if (Event.current.alt)
                {
                    Handles.color = Color.green;
                    DrawAdaptiveButton(anchorItem, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
                    {
                        var offset = Vector3.zero;
                        if (i > 0)
                        {
                            offset = (anchorItem - Target.GetAnchorAt(i - 1)).normalized * GetHandleSize(anchorItem);
                        }
                        else
                        {
                            offset = Vector3.forward * GetHandleSize(anchorItem);
                        }
                        Target.InsertAnchor(i + 1, anchorItem + offset);
                        Target.Rebuild();
                    });
                }
                else if (Event.current.shift)
                {
                    Handles.color = Color.red;
                    DrawAdaptiveButton(anchorItem, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
                    {
                        if (Target.AnchorsCount > 1)
                        {
                            Target.RemoveAnchorAt(i);
                            Target.Rebuild();
                        }
                    });
                }
                else
                {
                    Handles.color = Blue;
                    DrawFreeMoveHandle(anchorItem, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                    {
                        Target.SetAnchorAt(i, position);
                        Target.Rebuild();
                    });
                }
            }
        }
        #endregion
    }
}