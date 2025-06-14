using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : CharacterState
{
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => GameConstants.ANIM_ATTACK;
	[Export(PropertyHint.Range, "0,2,0.1")] private float postAttackFatigueDuration = 0.5f;

	private TimerHelper fatigueTimer;

	public override void _Ready()
	{
		base._Ready();
		fatigueTimer = new(this, HandleFatigueTimerTimeout, postAttackFatigueDuration, true);
	}

	private void HandleFatigueTimerTimeout()
	{
		Log($"attack fatigue timer ended");
		characterNode.StateMachine.SwitchState<EnemyIdleState>();
	}

	public override void EnableState()
	{
		base.EnableState();
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationEnd;
		characterNode.HitBoxNode.AreaEntered += HandleHitBoxEntered;
	}

	private void HandleHitBoxEntered(Area3D body)
	{
		Log("HIT");
		characterNode.Hit(body);
	}

	private void HandleAnimationEnd(StringName animName)
	{
		Log($"{animName} animation ended");
		fatigueTimer.Start();
	}

	public override void DisableState()
	{
		base.DisableState();
		characterNode.HitBoxNode.AreaEntered -= HandleHitBoxEntered;
		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationEnd;
	}


}
