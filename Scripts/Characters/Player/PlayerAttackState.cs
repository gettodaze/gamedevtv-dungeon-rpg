using Godot;
using System;

public partial class PlayerAttackState : CharacterState

{
	protected override string AnimationString => null;
	private int comboCount = 0;
	[Export(PropertyHint.Range, "1,10,1")] private int fullComboCount = 3;
	private TimerHelper comboTimer;
	[Export(PropertyHint.Range, "0,5,0.1")] private float comboTimerDuration = 1.0f;

	public override void _Ready()
	{
		base._Ready();
		comboTimer = new(this, OnComboTimeout, comboTimerDuration, oneShot: true);

	}

	private void OnComboTimeout()
	{
		comboCount = 0;
	}

	public override void EnableState()
	{
		base.EnableState();
		characterNode.AnimPlayerNode.AnimationFinished += OnAnimationFinished;
		characterNode.HitBoxNode.AreaEntered += HandleHitBoxEntered;
		NewCombo();

	}

	private void HandleHitBoxEntered(Area3D area)
	{
		Log($"HIT: Player attack combo {comboCount} entered {area.Name}");

	}

	public override void DisableState()
	{
		base.DisableState();
		characterNode.AnimPlayerNode.AnimationFinished -= OnAnimationFinished;
		characterNode.HitBoxNode.AreaEntered -= HandleHitBoxEntered;


	}

	private void OnAnimationFinished(StringName animName)
	{
		characterNode.StateMachine.SwitchState<PlayerIdleState>();
	}

	private void NewCombo()
	{
		string animString;
		if (comboCount >= fullComboCount) throw new InvalidOperationException($"comboCount {comboCount} exceeds fullComboCount {fullComboCount}");
		if (comboCount == fullComboCount - 1) animString = GameConstants.ANIM_ATTACK_SWORD;
		else animString = GameConstants.ANIM_ATTACK_KICK;
		characterNode.AnimPlayerNode.Play(animString, customSpeed: 2);
		comboCount = (comboCount + 1) % fullComboCount;
		comboTimer.StartOrReset();

	}
}
