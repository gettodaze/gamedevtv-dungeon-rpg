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

        main = GetParent<Main>();
        containers[ContainerType.Start].buttonNode.Pressed += HandleGameStart;
    }

    private void HandleGameStart()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        GameEvents.RaiseStartGame();
    }
}
