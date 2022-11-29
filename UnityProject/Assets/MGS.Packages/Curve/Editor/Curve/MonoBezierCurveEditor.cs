/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoBezierCurveEditor.cs
 *  Description  :  Editor for MonoBezierCurve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/3/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Curve.Editors
{
    [CustomEditor(typeof(MonoBezierCurve), true)]
    public class MonoBezierCurveEditor : MonoCurveEditor
    {
        protected new MonoBezierCurve Target { get { return target as MonoBezierCurve; } }

        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            if (!Application.isPlaying)
            {
                DrawEditor();
            }
        }

        protected virtual void DrawEditor()
        {
            DrawMoveEditor();
            var mode = CheckEditMode();
            if (mode == EditMode.Tangent)
            {
                DrawTangentEditor();
            }
        }

        protected virtual void DrawMoveEditor()
        {
            Handles.color = NormalColor;
            var from = Target.From;
            DrawFreeMoveHandle(from.point, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                if (Vector3.Distance(position, Target.To.point) <= NodeSize * GetHandleSize(position))
                {
                    position = Target.To.point;
                }
                from.point = position;
                Target.From = from;
                Target.Rebuild();
            });

            var to = Target.To;
            DrawFreeMoveHandle(to.point, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                if (Vector3.Distance(position, Target.From.point) <= NodeSize * GetHandleSize(position))
                {
                    position = Target.From.point;
                }
                to.point = position;
                Target.To = to;
                Target.Rebuild();
            });
        }

        protected virtual void DrawTangentEditor()
        {
            Handles.color = TangentColor;
            var from = Target.From;
            var frTangent = from.point + from.tangent;
            Handles.DrawLine(from.point, frTangent);
            DrawFreeMoveHandle(frTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                from.tangent = position - from.point;
                Target.From = from;
                Target.Rebuild();
            });

            var to = Target.To;
            var toTangent = to.point + to.tangent;
            Handles.DrawLine(to.point, toTangent);
            DrawFreeMoveHandle(toTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
            {
                to.tangent = position - to.point;
                Target.To = to;
                Target.Rebuild();
            });
        }

        protected virtual EditMode CheckEditMode()
        {
            if (Event.current.alt)
            {
                return EditMode.Tangent;
            }
            return EditMode.Normal;
        }

        protected enum EditMode
        {
            Normal = 0,
            Tangent = 1
        }
    }
}