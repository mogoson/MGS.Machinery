/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveCollider.cs
 *  Description  :  Capsule collider for mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/15/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Capsule collider for mono curve.
    /// </summary>
    public class MonoCurveCapsuleCollider : MonoCurveCollider
    {
        /// <summary>
        /// Group root of colliders.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected Transform colliderGroup;

        /// <summary>
        /// Reset component.
        /// </summary>
        protected override void Reset()
        {
            var groupName = string.Format("Collider{0}", GetInstanceID());
            colliderGroup = transform.FindChild(groupName);
            if (colliderGroup == null)
            {
                colliderGroup = new GameObject(groupName).transform;
                colliderGroup.parent = transform;
            }
            colliderGroup.localPosition = Vector3.zero;
            colliderGroup.localEulerAngles = Vector3.zero;
            base.Reset();
        }

        /// <summary>
        /// Component start.
        /// </summary>
        protected virtual void Start() { }

        /// <summary>
        /// Rebuild collider for mono curve.
        /// </summary>
        protected override void RebuildCollider(IMonoCurve curve)
        {
            var piece = 0f;
            Segments = MonoCurveUtility.GetSegmentCount(curve, segment, out piece);
            RequireCapsules(Segments);
            SetCapsules(curve, Segments, piece);
        }

        /// <summary>
        /// Clear collider of mono curve.
        /// </summary>
        protected override void ClearCollider()
        {
            var childCount = colliderGroup.childCount;
            while (childCount > 0)
            {
                Destroy(colliderGroup.GetChild(childCount - 1).gameObject);
                childCount--;
            }
        }

        /// <summary>
        /// Set capsule colliders.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="segments"></param>
        /// <param name="piece"></param>
        protected virtual void SetCapsules(IMonoCurve curve, int segments, float piece)
        {
            for (int i = 0; i < segments; i++)
            {
                var node = colliderGroup.GetChild(i);
                node.position = curve.Evaluate((i + 0.5f) * piece);
                var tangent = (curve.Evaluate((i + 1) * piece) - curve.Evaluate(i * piece));
                node.LookAt(node.position + tangent);

                var capsule = node.GetComponent<CapsuleCollider>();
                capsule.enabled = enabled;
                capsule.center = Vector3.zero;
                capsule.direction = 2;
                capsule.height = piece + radius * 2;
                capsule.radius = radius;
                capsule.isTrigger = isTrigger;
                capsule.material = material;
            }
        }

        /// <summary>
        /// Require the count of capsule colliders.
        /// </summary>
        /// <param name="count"></param>
        protected virtual void RequireCapsules(int count)
        {
            var childCount = colliderGroup.childCount;
            while (childCount < count)
            {
                var name = string.Format("Collider {0}", childCount);
                var newCollider = new GameObject(name, typeof(CapsuleCollider));
                newCollider.transform.parent = colliderGroup;
                childCount++;
            }
            while (childCount > count)
            {
                Destroy(colliderGroup.GetChild(childCount - 1).gameObject);
                childCount--;
            }
        }

        /// <summary>
        /// On destroy component.
        /// </summary>
        protected virtual void OnDestroy()
        {
            Destroy(colliderGroup.gameObject);
        }
    }
}