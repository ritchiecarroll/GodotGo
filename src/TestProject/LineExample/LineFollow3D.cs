// This is currently a manual conversion - a tool needs to be developed, i.e.,
// "godot-target.exe" to automate this class wrapper generation step.

// ** Example Post Build Operation Header **
// post-build[godot-target: type=Node; filename=LineFollow3D.cs; namespace=LineExample]
// build target: "type LineFollow3D struct" from LineExample.go, line 15
// godot-target operation executed at 2021 January 03 23:13:04 UTC

// TODO: Determine if other imports are needed (maybe just use those from: "go2cs: inject-code[using")
using Godot;
using go;
using GodotGo = go.GodotGo_package;
using static go.builtin;

namespace LineExample
{
    // Simple Node-based class wrapper for Go LineFollow3D structure
    public class LineFollow3D : Node
    {
        // Heap allocated instance of converted Go structure
        private readonly ptr<GodotGo.LineFollow3D> m_LineFollow3D;

        // Go interface implementations representing optional Godot message handlers
        // TODO: will need scan for all possible, could use an attribute marker
        private readonly GodotGo.IReady m_Ready;
        private readonly GodotGo.IProcess m_Process;

        LineFollow3D()
        {
            // Create an instance of Go-based Node
            m_LineFollow3D = addr(new GodotGo.LineFollow3D { parent = this });

            // Get an instance of each implemented interface
        #if NET5_0
            // C# 9.0 allows methods in an interface - Godot Mono version may not support this yet
            m_Ready = GodotGo.IReady.As(m_LineFollow3D);
            m_Process = GodotGo.IProcess.As(m_LineFollow3D);
        #else
            m_Ready = new GodotGo.IReady<GodotGo.LineFollow3D>(m_LineFollow3D);
            m_Process = new GodotGo.IProcess<GodotGo.LineFollow3D>(m_LineFollow3D);
        #endif
        }

        /* Implemented Godot Message Handler Overrides */

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() => m_Ready._Ready();

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta) => m_Process._Process(delta);
    }
}
