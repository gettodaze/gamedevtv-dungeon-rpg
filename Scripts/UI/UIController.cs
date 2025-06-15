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
        containers[ContainerType.Start].buttonNode.Pressed += HandleStartButtonPressed;
        containers[ContainerType.Victory].buttonNode.Pressed += HandleRestartButtonPressed;
        containers[ContainerType.Defeat].buttonNode.Pressed += HandleRestartButtonPressed;
        GameEvents.OnVictory += HandleOnVictory;
        GameEvents.OnDefeat += HandleOnDefeat;

        foreach (var pair in containers) pair.Value.Visible = pair.Key == ContainerType.Start;

    }

    private void HandleOnDefeat()
    {
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleOnVictory()
    {
        containers[ContainerType.Victory].Visible = true;
    }


    private void HandleRestartButtonPressed()
    {
        main.Restart();
    }

    private void HandleStartButtonPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        GameEvents.RaiseStartGame();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        containers[ContainerType.Start].buttonNode.Pressed -= HandleStartButtonPressed;
        containers[ContainerType.Victory].buttonNode.Pressed -= HandleRestartButtonPressed;
        containers[ContainerType.Defeat].buttonNode.Pressed -= HandleRestartButtonPressed;
        GameEvents.OnVictory -= HandleOnVictory;
        GameEvents.OnDefeat -= HandleOnDefeat;
    }
}
