using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerState[] states = GetChildren().OfType<PlayerState>().ToArray();
		var stateMap = states.ToDictionary(s => s.Name);
		GD.Print(string.Join(", ", states.Select(s => s.Name)));
		GD.Print(stateMap);
		var expectedKeys = new HashSet<string> { "Idle", "Run", "Jump" };
		var actualKeys = stateMap.Keys.ToHashSet();

		if (!actualKeys.Equals(expectedKeys))
		{
			var unexpected = actualKeys.Except(expectedKeys);
			var missing = expectedKeys.Except(actualKeys);

			throw new InvalidOperationException(
				$"Unexpected state keys.\nMissing: {string.Join(", ", missing)}\nUnexpected: {string.Join(", ", unexpected)}"
			);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
