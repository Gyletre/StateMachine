using Godot;
using System.Collections.Generic;
using System;

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
            if (currentSpell == Spell.None) return;
            GD.Print("Casting " + Enum.GetName(currentSpell));
            if (!stateMachine.knownSpells.Contains(currentSpell)) // checks if player knows spell
            {
                GD.Print(Enum.GetName(currentSpell) + " spell not known");
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
                    StartFireball();
                    break;
                case Spell.Heal:
                    StartHeal();
                    break;
                case Spell.Buff:
                    StartBuff();
                    break;
                default:
                    break;
            }
            isCasting = true;
        }
        private void StartFireball()
        {
            timer = 0.75;
            spellEffect += Fireball;

        }
        private void StartBuff()
        {
            timer = 0.5;
            spellEffect += Buff;
        }

        private void StartHeal()
        {
            timer = 0.5;
            spellEffect += Heal;
        }

        private void Heal()
        {
            stateMachine.hp = Mathf.Min(stateMachine.maxHP, stateMachine.hp + 30);
            stateMachine.healthBar.UpdateHealthBar(stateMachine.hp);
            GD.Print("heal");
            //spawn particle effect
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        private void Fireball()
        {
            GD.Print("FIREBALL!");
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
        private void Buff()
        {
            GD.Print("Buff activated");
            stateMachine.damageMultiplier++;
            stateMachine.GetTree().CreateTimer(buffDuration).Timeout += () =>
            {
                GD.Print("Buff no longer active");
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