using Godot;

// package GodotGo -- go2cs converted at 2021 January 05 08:49:42 UTC
// import "GodotGo" ==> using GodotGo = go.GodotGo_package
// Original source: D:\Projects\GodotGo\src\GodotGo\Node.go

namespace go
{
    public static partial class GodotGo_package
    {
        // Technically the the full Node class does not need to be defined since class member items will be
        // resolved at compile time by Godotgo compiler (i.e., Mono). However, these defintions may be useful
        // for intellisense type operations in Go source in the future...

        // Summary:
        //     Called when the node enters the Godot.SceneTree (e.g. upon instancing, scene
        //     changing, or after calling Godot.Node.AddChild(Godot.Node,System.Boolean) in
        //     a script). If the node has children, its Godot.Node._EnterTree callback will
        //     be called first, and then that of the children.
        //     Corresponds to the notification in Godot.Object._Notification(System.Int32).
        public partial interface IEnterTree
        {
            void _EnterTree();
        }

        // Summary:
        //     Called when the node is about to leave the Godot.SceneTree (e.g. upon freeing,
        //     scene changing, or after calling Godot.Node.RemoveChild(Godot.Node) in a script).
        //     If the node has children, its Godot.Node._ExitTree callback will be called last,
        //     after all its children have left the tree.
        //     Corresponds to the notification in Godot.Object._Notification(System.Int32) and
        //     signal tree_exiting. To get notified when the node has already left the active
        //     tree, connect to the tree_exited.
        public partial interface IExitTree
        {
            void _ExitTree();
        }

        // Summary:
        //     The string returned from this method is displayed as a warning in the Scene Dock
        //     if the script that overrides it is a tool script.
        //     Returning an empty string produces no warning.
        //     Call Godot.Node.UpdateConfigurationWarning when the warning needs to be updated
        //     for this node.
        public partial interface IGetConfigurationWarning
        {
            @string _GetConfigurationWarning();
        }

        // Summary:
        //     Called when there is an input event. The input event propagates up through the
        //     node tree until a node consumes it.
        //     It is only called if input processing is enabled, which is done automatically
        //     if this method is overridden, and can be toggled with Godot.Node.SetProcessInput(System.Boolean).
        //     To consume the input event and stop it propagating further to other nodes, Godot.SceneTree.SetInputAsHandled
        //     can be called.
        //     For gameplay input, Godot.Node._UnhandledInput(Godot.InputEvent) and Godot.Node._UnhandledKeyInput(Godot.InputEventKey)
        //     are usually a better fit as they allow the GUI to intercept the events first.
        //     Note: This method is only called if the node is present in the scene tree (i.e.
        //     if it's not orphan).
        public partial interface IInput
        {
            void _Input(InputEvent @event);
        }

        // Summary:
        //     Called during the physics processing step of the main loop. Physics processing
        //     means that the frame rate is synced to the physics, i.e. the delta variable should
        //     be constant.
        //     It is only called if physics processing is enabled, which is done automatically
        //     if this method is overridden, and can be toggled with Godot.Node.SetPhysicsProcess(System.Boolean).
        //     Corresponds to the notification in Godot.Object._Notification(System.Int32).
        //     Note: This method is only called if the node is present in the scene tree (i.e.
        //     if it's not orphan).
        public partial interface IPhysicsProcess
        {
            void _PhysicsProcess(float delta);
        }

        // Summary:
        //     Called during the processing step of the main loop. Processing happens at every
        //     frame and as fast as possible, so the delta time since the previous frame is
        //     not constant.
        //     It is only called if processing is enabled, which is done automatically if this
        //     method is overridden, and can be toggled with Godot.Node.SetProcess(System.Boolean).
        //     Corresponds to the notification in Godot.Object._Notification(System.Int32).
        //     Note: This method is only called if the node is present in the scene tree (i.e.
        //     if it's not orphan).
        public partial interface IProcess
        {
            void _Process(float delta);
        }

        // Summary:
        //     Called when the node is "ready", i.e. when both the node and its children have
        //     entered the scene tree. If the node has children, their Godot.Node._Ready callbacks
        //     get triggered first, and the parent node will receive the ready notification
        //     afterwards.
        //     Corresponds to the notification in Godot.Object._Notification(System.Int32).
        //     See also the onready keyword for variables.
        //     Usually used for initialization. For even earlier initialization, may be used.
        //     See also Godot.Node._EnterTree.
        //     Note: Godot.Node._Ready may be called only once for each node. After removing
        //     a node from the scene tree and adding again, _ready will not be called for the
        //     second time. This can be bypassed with requesting another call with Godot.Node.RequestReady,
        //     which may be called anywhere before adding the node again.
        public partial interface IReady
        {
            void _Ready();
        }

        // Summary:
        //     Called when an Godot.InputEvent hasn't been consumed by Godot.Node._Input(Godot.InputEvent)
        //     or any GUI. The input event propagates up through the node tree until a node
        //     consumes it.
        //     It is only called if unhandled input processing is enabled, which is done automatically
        //     if this method is overridden, and can be toggled with Godot.Node.SetProcessUnhandledInput(System.Boolean).
        //     To consume the input event and stop it propagating further to other nodes, Godot.SceneTree.SetInputAsHandled
        //     can be called.
        //     For gameplay input, this and Godot.Node._UnhandledKeyInput(Godot.InputEventKey)
        //     are usually a better fit than Godot.Node._Input(Godot.InputEvent) as they allow
        //     the GUI to intercept the events first.
        //     Note: This method is only called if the node is present in the scene tree (i.e.
        //     if it's not orphan).
        public partial interface IUnhandledInput
        {
            void _UnhandledInput(InputEvent @event);
        }

        // Summary:
        //     Called when an Godot.InputEventKey hasn't been consumed by Godot.Node._Input(Godot.InputEvent)
        //     or any GUI. The input event propagates up through the node tree until a node
        //     consumes it.
        //     It is only called if unhandled key input processing is enabled, which is done
        //     automatically if this method is overridden, and can be toggled with Godot.Node.SetProcessUnhandledKeyInput(System.Boolean).
        //     To consume the input event and stop it propagating further to other nodes, Godot.SceneTree.SetInputAsHandled
        //     can be called.
        //     For gameplay input, this and Godot.Node._UnhandledInput(Godot.InputEvent) are
        //     usually a better fit than Godot.Node._Input(Godot.InputEvent) as they allow the
        //     GUI to intercept the events first.
        //     Note: This method is only called if the node is present in the scene tree (i.e.
        //     if it's not orphan).
        public partial interface IUnhandledKeyInput
        {
            void _UnhandledKeyInput(InputEventKey @event);
        }
    }
}
