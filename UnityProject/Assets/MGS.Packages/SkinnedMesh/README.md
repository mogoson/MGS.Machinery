[TOC]

# MGS.SkinnedMesh

## Summary

- Unity plugin for create skinned mesh in scene.

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Platform

- Windows

## Version

- 1.2.1

## Technology

### Build Vertices

```C#
//Create polygon vertices.
var vertices = new List<Vector3>();
var sector = 2 * Mathf.PI / edge;
var radian = 0f;
for (int i = 0; i <= edge; i++)
{
    radian = sector * i;
    vertices.Add(center + rotation * new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * radius);
}
return vertices;
```

### Build Triangles

```C#
//Create polygon triangles index base on center vertice.
var triangles = new List<int>();
var offset = clockwise ? 0 : 1;
for (int i = 0; i < edge; i++)
{
    triangles.Add(start + i + offset);
    triangles.Add(start + i - offset + 1);
    triangles.Add(center);
}
return triangles;

//Create prism triangles index.
var triangles = new List<int>();
var polygonVs = polygon + 1;
var currentSegment = 0;
var nextSegment = 0;
for (int s = 0; s < segment - 1; s++)
{
    // Calculate start index.
    currentSegment = polygonVs * s;
    nextSegment = polygonVs * (s + 1);
    for (int p = 0; p < polygon; p++)
    {
        // Left-Bottom triangle.
        triangles.Add(start + currentSegment + p);
        triangles.Add(start + currentSegment + p + 1);
        triangles.Add(start + nextSegment + p + 1);

        // Right-Top triangle.
        triangles.Add(start + currentSegment + p);
        triangles.Add(start + nextSegment + p + 1);
        triangles.Add(start + nextSegment + p);
    }
}
return triangles;
```

### Build UV

```C#
//Create polygon uv.
var uv = new List<Vector2>();
var sector = 2 * Mathf.PI / edge;
var radian = 0f;
var center = Vector2.one * 0.5f;
for (int i = 0; i <= edge; i++)
{
    radian = sector * (clockwise ? i : edge - i);
    uv.Add(center + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * 0.5f);
}
return uv;

//Create prism uv.
var uv = new List<Vector2>();
var polygonVs = polygon + 1;
var vertices = polygonVs * segment;
var slice = 1.0f / polygon;
var u = 0f;
var v = 0f;
for (int i = 0; i < vertices; i++)
{
    u = slice * (i % polygonVs);
    v = (i / polygonVs) % 2;
    uv.Add(new Vector2(u, v));
}
return uv;
```

### Build Mesh

```C#
//Rebuild the mesh of hose.
mesh.vertices = CreateVertices(curve, segments, differ, isSeal);
mesh.uv = CreateUV(segments, isSeal);
mesh.triangles = CreateTriangles(segments, isSeal);

mesh.RecalculateNormals();
#if UNITY_5_6_OR_NEWER
mesh.RecalculateTangents();
#endif
mesh.RecalculateBounds();
```

### Shared Mesh

```C#
meshRenderer.sharedMesh = mesh;
meshRenderer.localBounds = mesh.bounds;
if (meshCollider)
{
    meshCollider.sharedMesh = null;
    meshCollider.sharedMesh = mesh;
}
```

### Combine Mesh

```C#
var combines = new List<CombineInstance>();
foreach (var filter in filters)
{
    var pos = filter.transform.position - center;
    var combine = new CombineInstance
    {
        mesh = filter.sharedMesh,
        transform = Matrix4x4.TRS(pos, filter.transform.rotation, filter.transform.lossyScale)
    };
    combines.Add(combine);
}
var rebornMesh = new Mesh();
rebornMesh.CombineMeshes(combines.ToArray(), mergeSubMeshes);

#if !UNITY_5_5_OR_NEWER
    //Mesh.Optimize was removed in version 5.5.2p4.
    rebornMesh.Optimize();
#endif
```

## Usage

### Curve Hose

- Attach mono curve component to a game object.

  ```tex
  MonoHermiteCurve MonoBezierCurve MonoHelixCurve MonoEllipseCurve MonoSinCurve
  ```

- Attach mono curve hose component to the game object.

  ```tex
  MonoCurveSkinnedHose
  ```

- Rebuild mono curve runtime, and mono curve hose component will auto Rebuild.

```C#
var curve = GetComponent<MonoHermiteCurve>();
curve.AddAnchor(new HermiteAnchor(point));
curve.Rebuild();//The mono curve hose component will auto Rebuild.
```

## Demo

- Demos in the path "MGS.Packages/SkinnedMesh/Demo/" provide reference to you.

## Source

- https://github.com/mogoson/MGS.SkinnedMesh.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com