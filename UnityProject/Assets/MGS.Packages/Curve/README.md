[TOC]

# MGS.Curve

## Summary

- Smooth 3D curve component for Unity project develop.

## Platform

- Windows

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Technology

### Check Rebuild

```C#
protected virtual void Awake() //I do not want to use Start.
{
    StartCoroutine(CheckRebuild());
}
protected virtual IEnumerator CheckRebuild()
{
    if (transform is RectTransform)
    {
        //Wait one frame to require the RectTransform is initialized (after Awake).
        yield return null;
    }
    Rebuild();
}
```

### Transform


```C#
//World to local position.
return transform.TransformPoint(worldPos);
//Local to world position.
transform.InverseTransformPoint(localPos);

//World to local vector.
return transform.TransformPoint(worldVector);
//Local to world vector.
transform.InverseTransformPoint(localVector);
```

### Differentiation

```C#
//AwayFromZero means that 12.5 -> 13
var units = (int)Math.Round(curve.Length * detail.unit, MidpointRounding.AwayFromZero);
var count = Mathf.Clamp(units, detail.min, detail.max);
differ = curve.Length / count;
return count;
```

### Calculus

```C#
//Evaluate length of MonoSinCurve.
var halfPI = Mathf.PI * 0.5f;
var angularAbs = Mathf.Abs(args.angular);
var piece = Vector2.Distance(Vector2.zero, new Vector2(halfPI / angularAbs, args.amplitude));
var pieces = piece * angularAbs;
var segments = Mathf.RoundToInt(radian / halfPI);
return pieces * segments;

//Evaluate length of MonoEllipseCurve.
var ratio = Mathf.Abs(radian) / (Mathf.PI * 2);
if (args.semiMinorAxis == 0 || args.semiMajorAxis == 0)
{
    return 2 * Mathf.Abs(args.semiMinorAxis + args.semiMajorAxis) * ratio;
}
var minor = Mathf.Abs(args.semiMinorAxis);
var major = Mathf.Abs(args.semiMajorAxis);
var a = Mathf.Max(minor, major);
var b = Mathf.Min(minor, major);
return (2 * Mathf.PI * b + 4 * (a - b)) * ratio;

//Evaluate length of MonoHelixCurve, MonoBezierCurve and MonoHermiteCurve.
var length = 0f;
var t = 0f;
var p0 = EvaluateNormalized(t);
while (t < 1.0f)
{
    t = Mathf.Min(t + differ, 1.0f);
    var p1 = EvaluateNormalized(t);
    length += Vector3.Distance(p0, p1);
    p0 = p1;
}
return length;
```

## Usage

- Attach mono curve component to a game object.

```tex
MonoHermiteCurve MonoBezierCurve MonoHelixCurve MonoEllipseCurve MonoSinCurve
```

- Adjust the parameters of curve component or edit curve in scene editor.

```tex
Select the MonoBezierCurve and drag the handle to adjust the anchor to see effect.
Press and hold the ALT into Tangent Edit mode, drag the handle to adjust the tangent of anchor.
If the start and end points are close, they will stick together.

Select the MonoHermiteCurve and drag the handle to adjust the anchor to see effect.
Press and hold the CTRL, click the green handle to add a new anchor.
Press and hold the CTRL+SHIFT, click the red handle to remove a anchor.
If do not use Auto Smooth,
Press and hold the ALT, click the blue handle to open Tangent editor and drag the
 cyan handle to adjust the tangent of anchor; on this mode, Press and hold the SHIFT
 to open In and Out tangent editor, drag the cyan and green handle to adjust the
 tangents of anchor.
Press and hold the ALT+COMMAND into All Tangents mode.
If the start and end points are close, they will stick together.
```

- Attach mono curve renderer component to the mono curve game object to renderer curve in scene  if need.
```tex
MonoCurveLineRenderer
```

- Attach mono curve collider component to the mono curve game object if need.
```tex
MonoCurveCapsuleCollider
```

- Attach mono curve cacher component to the mono curve game object if need.
```tex
MonoBezierCurveCacher MonoHermiteCurveCacher
```

- Evaluate point on the mono curve if need.

```C#
//Evaluate point on the mono curve at length.
var len = 0f;
var p0 = curve.Evaluate(len);
while (len < curve.Length)
{
    len = Mathf.Min(len + 0.01f, curve.Length);
    var p1 = curve.Evaluate(len);
    //Just for demo, you can use p0,p1 to do more things.
    Gizmos.DrawLine(p0, p1);
    p0 = p1;
}

//Evaluate point on the mono curve at normalized time in the range[0,1].
var t = 0f;
var p0 = curve.EvaluateNormalized(t);
while (t < 1.0f)
{
    t = Mathf.Min(t + differ, 1.0f);
    var p1 = curve.EvaluateNormalized(t);
    //Just for demo, you can use p0,p1 to do more things.
    Gizmos.DrawLine(p0, p1);
    p0 = p1;
}
```

## Demo

- Demos in the path "MGS.Packages/Curve/Demo/" provide reference to you.

## Source

- https://github.com/mogoson/MGS.Curve.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com