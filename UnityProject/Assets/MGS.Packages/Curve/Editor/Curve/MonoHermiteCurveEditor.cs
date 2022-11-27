/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoHermiteCurveEditor.cs
 *  DeTargetion  :  Editor for MonoHermiteCurve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Curve.Editors
{
    [CustomEditor(typeof(MonoHermiteCurve), true)]
    public class MonoHermiteCurveEditor : MonoCurveEditor
    {
        protected new MonoHermiteCurve Target { get { return target as MonoHermiteCurve; } }
        protected int adsorbent = -1;
        protected int select = -1;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Target.IsClose)
            {
                adsorbent = Target.AnchorsCount - 1;
            }
        }

        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            if (!Application.isPlaying)
            {
                DrawEditor();
            }
        }

        protected override string CollectCaption()
        {
            return string.Format("{0}  Anchors: {1}", base.CollectCaption(), Target.AnchorsCount);
        }

        protected override void OnInspectorChanged()
        {
            base.OnInspectorChanged();
            if (Target.autoSmooth)
            {
                Target.InverseAnchorTangents();
            }
        }

        protected virtual void DrawEditor()
        {
            DrawDefaultEditor();

            var mode = CheckEditMode();
            if (mode == EditMode.Anchor)
            {
                if (CheckAnchorCtrl() == AnchorCtrl.Insert)
                {
                    for (int i = 0; i < Target.AnchorsCount; i++)
                    {
                        DrawInsertEditor(i, Target.GetAnchor(i));
                    }
                }
                else
                {
                    for (int i = 0; i < Target.AnchorsCount; i++)
                    {
                        DrawRemoveEditor(i, Target.GetAnchor(i));
                    }
                }
            }
            else
            {
                if (mode == EditMode.Normal || Target.autoSmooth)
                {
                    for (int i = 0; i < Target.AnchorsCount; i++)
                    {
                        DrawMoveEditor(i, Target.GetAnchor(i));
                    }
                }
                else
                {
                    if (CheckTangentMode() == TangentMode.Single)
                    {
                        for (int i = 0; i < Target.AnchorsCount; i++)
                        {
                            if (i == select)
                            {
                                DrawMoveEditor(i, Target.GetAnchor(i));
                            }
                            else
                            {
                                DrawSelectEditor(i, Target.GetAnchor(i));
                            }
                        }

                        if (select >= 0)
                        {
                            if (CheckTangentCtrl() == TangentCtrl.Unify)
                            {
                                DrawUnifyTangentEditor(select, Target.GetAnchor(select));
                                if ((select == 0 || select == Target.AnchorsCount - 1) && Target.IsClose)
                                {
                                    var index = Target.AnchorsCount - 1 - select;
                                    DrawUnifyTangentEditor(index, Target.GetAnchor(index));
                                }
                            }
                            else
                            {
                                DrawSeparateTangentEditor(select, Target.GetAnchor(select));
                                if ((select == 0 || select == Target.AnchorsCount - 1) && Target.IsClose)
                                {
                                    var index = Target.AnchorsCount - 1 - select;
                                    DrawSeparateTangentEditor(index, Target.GetAnchor(index));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CheckTangentCtrl() == TangentCtrl.Unify)
                        {
                            for (int i = 0; i < Target.AnchorsCount; i++)
                            {
                                DrawMoveEditor(i, Target.GetAnchor(i));
                                DrawUnifyTangentEditor(i, Target.GetAnchor(i));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Target.AnchorsCount; i++)
                            {
                                DrawMoveEditor(i, Target.GetAnchor(i));
                                DrawSeparateTangentEditor(i, Target.GetAnchor(i));
                            }
                        }
                    }
                }
            }
        }

        protected virtual void DrawDefaultEditor()
        {
            if (Target.AnchorsCount == 0)
            {
                //Considering that it may be used on UI, so use Vector2.one.
                var point = Target.transform.position + Target.transform.TransformVector(Vector2.one).normalized * GetHandleSize(Target.transform.position);
                Target.AddAnchor(new HermiteAnchor(point));
            }
        }

        protected virtual void DrawInsertEditor(int index, HermiteAnchor anchor)
        {
            Handles.color = Color.green;
            DrawAdaptiveButton(anchor.point, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
            {
                var offset = Vector3.zero;
                if (index > 0)
                {
                    offset = (anchor.point - Target.GetAnchor(index - 1).point).normalized * GetHandleSize(anchor.point);
                }
                else
                {
                    //Considering that it may be used on UI, so use Vector2.one.
                    offset = Target.transform.TransformVector(Vector2.one).normalized * GetHandleSize(anchor.point);
                }

                select = index + 1;
                if (adsorbent > 0)
                {
                    adsorbent++;
                }
                Target.InsertAnchor(index + 1, new HermiteAnchor(anchor.point + offset));
                Target.Rebuild();
            });
        }

        protected virtual void DrawRemoveEditor(int index, HermiteAnchor anchor)
        {
            Handles.color = Color.red;
            DrawAdaptiveButton(anchor.point, Quaternion.identity, NodeSize, NodeSize, SphereCap, () =>
            {
                if (select >= index)
                {
                    select--;
                }
                if (adsorbent > 0)
                {
                    adsorbent--;
                }
                Target.RemoveAnchor(index);
                Target.Rebuild();
            });
        }

        protected virtual void DrawSelectEditor(int index, HermiteAnchor anchor)
        {
            var nodeSize = NodeSize;
            if (index == 0 || index == Target.AnchorsCount - 1)
            {
                if (Target.IsClose)
                {
                    if (index != adsorbent)
                    {
                        return;
                    }
                    nodeSize = NodeSize * 1.25f;
                }
            }

            Handles.color = Color.blue;
            DrawAdaptiveButton(anchor.point, Target.transform.rotation, nodeSize, nodeSize, SphereCap, () => select = index);
        }

        protected virtual void DrawMoveEditor(int index, HermiteAnchor anchor)
        {
            Handles.color = Color.white;
            if (index == 0 || index == Target.AnchorsCount - 1)
            {
                var nodeSize = NodeSize;
                if (Target.IsClose)
                {
                    if (index != adsorbent && index > 0)
                    {
                        return;
                    }
                    nodeSize = NodeSize * 1.25f;
                }

                DrawFreeMoveHandle(anchor.point, Quaternion.identity, nodeSize, MoveSnap, SphereCap, position =>
                {
                    var adjacent = Target.GetAnchor(Target.AnchorsCount - 1 - index);
                    if (Vector3.Distance(position, adjacent.point) <= NodeSize * GetHandleSize(position))
                    {
                        adsorbent = index;
                        position = adjacent.point;
                    }
                    else
                    {
                        adsorbent = -1;
                    }
                    anchor.point = position;
                    Target.SetAnchor(index, anchor);
                    Target.Rebuild();
                });
            }
            else
            {
                DrawFreeMoveHandle(anchor.point, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                {
                    anchor.point = position;
                    Target.SetAnchor(index, anchor);
                    Target.Rebuild();
                });
            }
        }

        protected virtual void DrawUnifyTangentEditor(int index, HermiteAnchor anchor)
        {
            Handles.color = Color.cyan;
            if (index > 0)
            {
                var inTangent = anchor.point - anchor.inTangent;
                Handles.DrawLine(anchor.point, inTangent);
                DrawFreeMoveHandle(inTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                {
                    anchor.inTangent = anchor.outTangent = anchor.point - position;
                    Target.SetAnchor(index, anchor);
                    if (Target.IsClose && index == Target.AnchorsCount - 1)
                    {
                        var firstAnchor = Target.GetAnchor(0);
                        firstAnchor.outTangent = anchor.inTangent;
                        Target.SetAnchor(0, firstAnchor);
                    }
                    Target.Rebuild();
                });
            }

            if (index < Target.AnchorsCount - 1)
            {
                var outTangent = anchor.point + anchor.outTangent;
                Handles.DrawLine(anchor.point, outTangent);
                DrawFreeMoveHandle(outTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                {
                    anchor.inTangent = anchor.outTangent = position - anchor.point;
                    Target.SetAnchor(index, anchor);
                    if (Target.IsClose && index == 0)
                    {
                        var lastAnchor = Target.GetAnchor(Target.AnchorsCount - 1);
                        lastAnchor.inTangent = anchor.outTangent;
                        Target.SetAnchor(Target.AnchorsCount - 1, lastAnchor);
                    }
                    Target.Rebuild();
                });
            }
        }

        protected virtual void DrawSeparateTangentEditor(int index, HermiteAnchor anchor)
        {
            if (index > 0)
            {
                Handles.color = Color.cyan;
                var inTangent = anchor.point - anchor.inTangent;
                Handles.DrawLine(anchor.point, inTangent);
                DrawFreeMoveHandle(inTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                {
                    anchor.inTangent = anchor.point - position;
                    Target.SetAnchor(index, anchor);
                    Target.Rebuild();
                });
            }

            if (index < Target.AnchorsCount - 1)
            {
                Handles.color = Color.green;
                var outTangent = anchor.point + anchor.outTangent;
                Handles.DrawLine(anchor.point, outTangent);
                DrawFreeMoveHandle(outTangent, Quaternion.identity, NodeSize, MoveSnap, SphereCap, position =>
                {
                    anchor.outTangent = position - anchor.point;
                    Target.SetAnchor(index, anchor);
                    Target.Rebuild();
                });
            }
        }

        protected virtual EditMode CheckEditMode()
        {
            if (Event.current.control)
            {
                return EditMode.Anchor;
            }
            else if (Event.current.alt)
            {
                return EditMode.Tangent;
            }
            return EditMode.Normal;
        }

        protected virtual AnchorCtrl CheckAnchorCtrl()
        {
            if (Event.current.shift)
            {
                return AnchorCtrl.Delete;
            }
            return AnchorCtrl.Insert;
        }

        protected virtual TangentMode CheckTangentMode()
        {
            if (Event.current.command)
            {
                return TangentMode.Multi;
            }
            return TangentMode.Single;
        }

        protected virtual TangentCtrl CheckTangentCtrl()
        {
            if (Event.current.shift)
            {
                return TangentCtrl.Separate;
            }
            return TangentCtrl.Unify;
        }

        protected enum EditMode
        {
            Anchor = -1,
            Normal = 0,
            Tangent = 1
        }

        protected enum AnchorCtrl
        {
            Insert = 0,
            Delete = 1
        }

        protected enum TangentMode
        {
            Single = 0,
            Multi = 1
        }

        protected enum TangentCtrl
        {
            Unify = 0,
            Separate = 1
        }
    }
}