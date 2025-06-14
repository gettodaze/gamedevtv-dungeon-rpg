using Godot;
using System;

public partial class UIContainer : VBoxContainer
{
	[Export] public ContainerType container { get; private set; }
	[Export] public Button buttonNode { get; private set; }
}
