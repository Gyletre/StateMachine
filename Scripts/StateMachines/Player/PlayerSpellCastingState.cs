using Godot;
using System;
using Game.UI;

namespace Game.StateMachine.PlayerState
{
    public class PlayerSpellCastingState : PlayerBaseState
    {
        float buffDuration = 5f;
        Spell currentSpell;
        bool isCasting = false;
        double timer = -1;
        Action spellEffect = null;
        public PlayerSpellCastingState(PlayerStateMachine stateMachine, Spell spellToUse) : base(stateMachine)
        {
            currentSpell = spellToUse;
        }

        public override void Enter()
        {
            GD.Print("Entered spellcasting state");
            TextMessageWriter.Print("Casting " + Enum.GetName(currentSpell));
            if (!stateMachine.knownSpells.Contains(currentSpell)) // checks if player knows spell
            {
                TextMessageWriter.Print(Enum.GetName(currentSpell) + " spell not known");
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
                return;
            }
            stateMachine.animator.SwitchAnimation(AnimationType.CastSpell, OnEnd: CastSpell);

        }



        public override void Tick(double delta)
        {
            if (isCasting)
            {
                timer -= delta;
                if (timer <= 0)
                {
                    spellEffect?.Invoke();
                }
            }
        }

        public override void Exit()
        {

        }
        private void CastSpell()
        {

            switch (currentSpell)
            {
                case Spell.Fireball:
                    StartSpell(0.5, Fireball);
                    break;
                case Spell.Heal:
                    StartSpell(0.75, Heal);
                    break;
                case Spell.Buff:
                    StartSpell(0.5, Buff);
                    break;
                default:
                    break;
            }
            isCasting = true;
        }
        private void StartSpell(double windup, Action spell)
        {
            timer = windup;
            spellEffect += spell;
        }

        private void Heal()
        {
            stateMachine.hp = Mathf.Min(stateMachine.maxHP, stateMachine.hp + 30);
            stateMachine.healthBar.UpdateHealthBar(stateMachine.hp);
            TextMessageWriter.Print("heal");
            //spawn particle effect
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        private void Fireball()
        {
            TextMessageWriter.Print("FIREBALL!");
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
        private void Buff()
        {
            TextMessageWriter.Print("Buff activated");
            stateMachine.damageMultiplier++;
            stateMachine.GetTree().CreateTimer(buffDuration).Timeout += () =>
            {
                TextMessageWriter.Print("Buff no longer active");
                stateMachine.damageMultiplier--;
            };
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }


    }
}
public enum Spell
{
    None,
    Heal,
    Fireball,
    Buff
}
public enum Buffs
{
    Buff,
    Regen
}