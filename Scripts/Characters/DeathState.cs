using System;
using Godot;

public partial class DeathState : CharacterState

{
    public override bool IsEligibleForRandom => false;
    protected override string AnimationString => GameConstants.ANIM_DEATH;

    public override void EnableState()
    {
        base.EnableState();
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.HandleDeath();
    }

    public override void DisableState()
    {
        base.DisableState();
        // TODO: handle entering death state twice because of double damage.
        throw new InvalidOperationException("Cannot disable death state");
    }
}