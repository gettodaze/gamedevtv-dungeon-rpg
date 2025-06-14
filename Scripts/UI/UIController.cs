using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private Main main;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary(
            (element) => element.container
        );

        var startUI = containers[ContainerType.Start];
        main = GetParent<Main>();
        startUI.Visible = true;
        startUI.buttonNode.Pressed += () => { GetTree().Paused = false; startUI.Visible = false; };
    }
}
