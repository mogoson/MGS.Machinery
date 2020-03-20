/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankLinkEditor.cs
 *  Description  :  Custom editor for CrankLink.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/21/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/20/2018
 *  Description  :  Optimize display of coordinate system.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    public class CrankLinkEditor : MechanismEditor
    {
        #region Field and Property
        protected CrankLinkMechanism Target { get { return target as CrankLinkMechanism; } }
        protected readonly string[] HingeEditorButtons = { "Free", "Hinge", "Lock" };
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.right, LineLength, 0, "x");
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.up, LineLength, 0, "y");
        }

        protected void DrawHingeEditorTool()
        {
            if (Application.isPlaying)
            {
                Target.editMode = EditMode.Lock;
                GUILayout.SelectionGrid((int)Target.editMode, HingeEditorButtons, HingeEditorButtons.Length);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                Target.editMode = (EditMode)GUILayout.SelectionGrid((int)Target.editMode, HingeEditorButtons, HingeEditorButtons.Length);

                if (Target.editMode == EditMode.Free)
                {
                    if (Target.enabled)
                    {
                        Target.enabled = false;
                        Target.isInitialized = false;
                    }
                }
                else
                {
                    if (!Target.enabled)
                    {
                        Target.enabled = true;
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(Target);
                    MarkSceneDirty();
                }
            }
        }

        protected Vector3 CorrectAngles(Vector3 angles)
        {
            return new Vector3(0, 0, angles.z);
        }

        protected Vector3 CorrectPosition(Vector3 position)
        {
            return new Vector3(position.x, position.y);
        }
        #endregion
    }
}