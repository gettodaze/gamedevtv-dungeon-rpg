using Godot;
using System;

public partial class Player : Character
{
	private int enemyCount = 0;
	public void SetInputDirection()
	{
		// var oldDir = direction;
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		// Log($"Set character direction from {oldDir} to {direction}");
	}

	public bool CheckAndHandleAttackInput()
	{
		if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
		{
			StateMachine.SwitchState<PlayerAttackState>();
			return true;
		}
		return false;
	}

	public override void HandleDeath()
	{
		GameEvents.RaiseDefeat();
	}

	public override void _Ready()
	{
		base._Ready();
		GameEvents.OnStartGame += HandleStartGame;
		GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		GameEvents.OnStartGame -= HandleStartGame;
		GameEvents.OnNewEnemyCount -= HandleNewEnemyCount;
	}

	private void HandleNewEnemyCount(int newCount)
	{
		if (newCount > enemyCount) Stats.Heal(5);
		enemyCount = newCount;
	}

	private void HandleStartGame()
	{
		Stats.CurrentHealth = Stats.MaxHealth;
	}


}
