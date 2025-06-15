using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private Main main;
    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary(
            (element) => element.container
        );

        main = GetParent<Main>();
        containers[ContainerType.Start].buttonNode.Pressed += HandleStartButtonPressed;
        containers[ContainerType.Victory].buttonNode.Pressed += HandleRestartButtonPressed;
        containers[ContainerType.Defeat].buttonNode.Pressed += HandleRestartButtonPressed;
        containers[ContainerType.Pause].buttonNode.Pressed += HandleRestartButtonPressed;
        GameEvents.OnVictory += HandleOnVictory;
        GameEvents.OnDefeat += HandleOnDefeat;

        foreach (var pair in containers) pair.Value.Visible = pair.Key == ContainerType.Start;

    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (Input.IsActionJustPressed(GameConstants.INPUT_PAUSE) && canPause)
        {
            bool pause = !containers[ContainerType.Pause].Visible;
            containers[ContainerType.Pause].Visible = pause;
            main.SetTreePaused(pause);
        }
    }

    private void HandlePauseButtonPressed()
    {
        throw new NotImplementedException();
    }

    private void HandleOnDefeat()
    {
        containers[ContainerType.Defeat].Visible = true;
        canPause = false;
    }

    private void HandleOnVictory()
    {
        containers[ContainerType.Victory].Visible = true;
        canPause = false;
    }


    private void HandleRestartButtonPressed()
    {
        main.Restart();
    }

    private void HandleStartButtonPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
        canPause = true;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        containers[ContainerType.Start].buttonNode.Pressed -= HandleStartButtonPressed;
        containers[ContainerType.Victory].buttonNode.Pressed -= HandleRestartButtonPressed;
        containers[ContainerType.Defeat].buttonNode.Pressed -= HandleRestartButtonPressed;
        containers[ContainerType.Pause].buttonNode.Pressed -= HandleRestartButtonPressed;
        GameEvents.OnVictory -= HandleOnVictory;
        GameEvents.OnDefeat -= HandleOnDefeat;
    }
}
