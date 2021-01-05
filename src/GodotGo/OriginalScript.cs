using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class LineFollow3D : Node
{
    private Vector3[] points;
    private bool doLoop = true;
    private CSGBox cube;
    private float speed = 0.25f;
    private float currentDistance;
    private float[] lineDistances;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Spatial spatial = GetTree().Root.GetChild(0) as Spatial;

        if (spatial is null)
            throw new NullReferenceException("Failed to find Spatial node");

        List<CSGSphere> spheres = new List<CSGSphere>();
        ImmediateGeometry lineRenderer = null;

        foreach (Node node in spatial.GetChildren())
        {
            Type type = node.GetType();
            string name = node.Name;

            if (type == typeof(CSGSphere) && name.StartsWith("Sphere"))
                spheres.Add(node as CSGSphere);
            else if (type == typeof(CSGBox) && name.Equals("Cube"))
                cube = node as CSGBox;
            else if (type == typeof(ImmediateGeometry) && name.Equals("LineRenderer"))
                lineRenderer = node as ImmediateGeometry;
        }

        if (spheres.Count < 2)
            throw new NullReferenceException("Failed to find at least two Spheres");

        if (cube is null)
            throw new NullReferenceException("Failed to find Cube");

        if (lineRenderer is null)
            throw new NullReferenceException("Failed to find LineRenderer");

        List<Vector3> sphereOrigins = spheres.Select(sphere => sphere.Transform.origin).ToList();

        if (doLoop)
            sphereOrigins.Add(sphereOrigins[0]);

        points = sphereOrigins.ToArray();

        lineRenderer.Set("globalCoords", false);
        lineRenderer.Set("points", points);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (currentDistance < 1.0F)
        {
            // Make the cube "ride" the line at a constant speed
            currentDistance += delta * speed;
            cube.Transform = new Transform(cube.Transform.basis, CalculatePositionAlongLine(currentDistance));
        }
        else if (doLoop)
        {
            currentDistance = 0.0F;
        }
    }

    private Vector3 CalculatePositionAlongLine(float distance)
    {
        InitializeLineDistances();

        distance = Mathf.Lerp(0f, lineDistances.Last(), distance);

        int pointCount = points.Length;
        int index = 1;

        while (distance > lineDistances[index] && index < pointCount - 1)
            index++;

        float weight = Mathf.InverseLerp(lineDistances[index - 1], lineDistances[index], distance);
        return points[index - 1].LinearInterpolate(points[index], weight);
    }

    private void InitializeLineDistances()
    {
        if (!(lineDistances is null))
            return;

        int pointCount = points.Length;
        lineDistances = new float[pointCount];

        double distance = 0.0D;

        for (int i = 0; i < pointCount - 1; i++)
        {
            Vector3 offset = points[i] - points[i + 1];
            distance += Math.Sqrt(offset.x * offset.x + offset.y * offset.y + offset.z * offset.z);
            lineDistances[i + 1] = (float)distance;
        }
    }
}
