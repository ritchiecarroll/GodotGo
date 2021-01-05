// TODO: Modify go2cs to properly pickup the following directives
//go2cs: inject-code[using Godot;]
//go2cs: inject-code[using System;]
//go2cs: inject-code[#pragma warning disable CS0660, CS0661]

package GodotGo

import (
    "fmt"
)

// TODO: Directive that will call a post-build utility on specifed target, in this case
// wrapping converted LineFollow3D structure with a class that inherits a Godot Node
//go2cs: post-build[godot-target.exe type=Node filename=LineFollow3D.cs namespace=LineExample]
type LineFollow3D struct {
    parent Node
    points []Vector3
    doLoop bool
    cube CSGBox
    speed float32
    currentDistance float32
    lineDistances []float32
}

// Called when the node enters the scene tree for the first time.
func (goNode *LineFollow3D) _Ready() {
    // Setup any default values
    goNode.doLoop = true
    goNode.speed = 0.25

    node := goNode.parent
    spatial, ok := node.GetTree().Root.GetChild(0).(Spatial)

    if !ok {
        panic("Failed to find Spatial node")
    }

    var spheres []CSGSphere
    var lineRenderer ImmediateGeometry

    for item := range spatial.GetChildren() {
        childNode, ok := item.(Node)

        if !ok {
            continue
        }

        name := childNode.Name

        sphere, ok := childNode.(CSGSphere)

        if ok && name[:6] == "Sphere" {
            spheres = append(spheres, sphere)
            continue
        }

        cube, ok := childNode.(CSGBox)

        if ok && name == "Cube" {
            goNode.cube = cube
            continue
        }

        lineRenderer, ok := childNode.(ImmediateGeometry)
    }

    sphereCount := len(spheres)

    if sphereCount < 2 {
        panic("Failed to find at least two Spheres")
    }

    if goNode.cube == nil {
        panic("Failed to find Cube")
    }

    if lineRenderer == nil {
        panic("Failed to find LineRenderer")
    }

    sphereOrigins := make([]Vector3, sphereCount, sphereCount + 1)

    for i := 0; i < sphereCount; i+=1 {
        sphereOrigins[i] = spheres[i].Transform.origin
    }

    if (goNode.doLoop) {
        sphereOrigins = append(sphereOrigins, sphereOrigins[0])
    }

    goNode.points = sphereOrigins

    lineRenderer.Set("globalCoords", false)
    lineRenderer.Set("points", goNode.points.Array)
}

// Called every frame. 'delta' is the elapsed time since the previous frame.
func (goNode *LineFollow3D) _Process(delta float32) {
    if (goNode.currentDistance < 1.0) {
        // Make the cube "ride" the line at a constant speed
        goNode.currentDistance += delta * goNode.speed
        goNode.cube.Transform = Transform{goNode.cube.Transform.basis, CalculatePositionAlongLine(goNode)}
    } else if (doLoop) {
        goNode.currentDistance = 0.0
    }
}

func CalculatePositionAlongLine(goNode *LineFollow3D) Vector3 {
    InitializeLineDistances(goNode)

    var distance float32 = goNode.currentDistance
    pointCount := len(goNode.points)

    distance = Mathf.Lerp(0.0, goNode.lineDistances[pointCount - 1], distance)
    index := 1

    for distance > goNode.lineDistances[index] && index < pointCount - 1 {
        index++
    }

    var weight float32 = Mathf.InverseLerp(goNode.lineDistances[index - 1], goNode.lineDistances[index], distance)
    return goNode.points[index - 1].LinearInterpolate(goNode.points[index], weight)
}

func InitializeLineDistances(goNode *LineFollow3D) {
    if (len(goNode.lineDistances) > 0) {
        return
    }

    pointCount := len(goNode.points)
    goNode.lineDistances = make([]float32, pointCount)

    var distance float64
    var offset Vector3

    for i := 0; i < pointCount - 1; i+=1 {
        offset = goNode.points[i] - goNode.points[i + 1]
        distance += Math.Sqrt(offset.x * offset.x + offset.y * offset.y + offset.z * offset.z)
        goNode.lineDistances[i + 1] = float32(distance)
    }
}