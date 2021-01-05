// TODO: Modify go2cs to properly pickup the following directives
//go2cs: inject-code[using Godot;]
//go2cs: inject-code[using System;]
//go2cs: inject-code[#pragma warning disable CS0660, CS0661]

// package GodotGo -- go2cs converted at 2021 January 05 09:50:08 UTC
// import "GodotGo" ==> using GodotGo = go.GodotGo_package
// Original source: D:\Projects\GodotGo\src\GodotGo\LineExample.go
using fmt = go.fmt_package;
using static go.builtin;

namespace go
{
    public static partial class GodotGo_package
    {
        // TODO: Directive that will call a post-build utility on specifed target, in this case
        // wrapping converted LineFollow3D structure with a class that inherits a Godot Node
        //go2cs: post-build[godot-target.exe type=Node filename=LineFollow3D.cs namespace=LineExample]
        public partial struct LineFollow3D
        {
            public Node parent;
            public slice<Vector3> points;
            public bool doLoop;
            public CSGBox cube;
            public float speed;
            public float currentDistance;
            public slice<float> lineDistances;
        }

        // Called when the node enters the scene tree for the first time.
        private static void _Ready(this ptr<LineFollow3D> _addr_goNode) => func((_, panic, __) =>
        {
            ref LineFollow3D goNode = ref _addr_goNode.val;
 
            // Setup any default values
            goNode.doLoop = true;
            goNode.speed = 0.25F;

            var node = goNode.parent;
            Spatial (spatial, ok) = node.GetTree().Root.GetChild(0)._<Spatial>();

            if (!ok)
            {
                panic("Failed to find Spatial node");
            }
            slice<CSGSphere> spheres = default;
            ImmediateGeometry lineRenderer = default;

            foreach (var (item) in spatial.GetChildren())
            {
                Node (childNode, ok) = item._<Node>();

                if (!ok)
                {
                    continue;
                }
                var name = childNode.Name;

                CSGSphere (sphere, ok) = childNode._<CSGSphere>();

                if (ok && name[..(int)6] == "Sphere")
                {
                    spheres = append(spheres, sphere);
                    continue;
                }
                CSGBox (cube, ok) = childNode._<CSGBox>();

                if (ok && name == "Cube")
                {
                    goNode.cube = cube;
                    continue;
                }
                ImmediateGeometry (lineRenderer, ok) = childNode._<ImmediateGeometry>();
            }
            var sphereCount = len(spheres);

            if (sphereCount < 2)
            {
                panic("Failed to find at least two Spheres");
            }
            if (goNode.cube == null)
            {
                panic("Failed to find Cube");
            }
            if (lineRenderer == null)
            {
                panic("Failed to find LineRenderer");
            }
            var sphereOrigins = make_slice<Vector3>(sphereCount, sphereCount + 1);

            {
                nint i = 0;

                while (i < sphereCount)
                {
                    sphereOrigins[i] = spheres[i].Transform.origin;
                    i += 1;
                }

            }

            if ((goNode.doLoop))
            {
                sphereOrigins = append(sphereOrigins, sphereOrigins[0]);
            }
            goNode.points = sphereOrigins;

            lineRenderer.Set("globalCoords", false);
            lineRenderer.Set("points", goNode.points.Array);
        });

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        private static void _Process(this ptr<LineFollow3D> _addr_goNode, float delta)
        {
            ref LineFollow3D goNode = ref _addr_goNode.val;

            if ((goNode.currentDistance < 1.0F))
            { 
                // Make the cube "ride" the line at a constant speed
                goNode.currentDistance += delta * goNode.speed;
                goNode.cube.Transform = new Transform(goNode.cube.Transform.basis,CalculatePositionAlongLine(goNode));
            }
            else if ((doLoop))
            {
                goNode.currentDistance = 0.0F;
            }
        }

        public static Vector3 CalculatePositionAlongLine(ptr<LineFollow3D> _addr_goNode)
        {
            ref LineFollow3D goNode = ref _addr_goNode.val;

            InitializeLineDistances(_addr_goNode);

            float distance = goNode.currentDistance;
            var pointCount = len(goNode.points);

            distance = Mathf.Lerp(0.0F, goNode.lineDistances[pointCount - 1], distance);
            nint index = 1;

            while (distance > goNode.lineDistances[index] && index < pointCount - 1)
            {
                index++;
            }


            float weight = Mathf.InverseLerp(goNode.lineDistances[index - 1], goNode.lineDistances[index], distance);
            return goNode.points[index - 1].LinearInterpolate(goNode.points[index], weight);
        }

        public static void InitializeLineDistances(ptr<LineFollow3D> _addr_goNode)
        {
            ref LineFollow3D goNode = ref _addr_goNode.val;

            if ((len(goNode.lineDistances) > 0))
            {
                return ;
            }
            var pointCount = len(goNode.points);
            goNode.lineDistances = make_slice<float>(pointCount);

            double distance = default;
            Vector3 offset = default;

            {
                nint i = 0;

                while (i < pointCount - 1)
                {
                    offset = goNode.points[i] - goNode.points[i + 1];
                    distance += Math.Sqrt(offset.x * offset.x + offset.y * offset.y + offset.z * offset.z);
                    goNode.lineDistances[i + 1] = float32(distance);
                    i += 1;
                }

            }
        }
    }
}
