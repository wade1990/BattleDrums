using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Spearmen : Unit
    {
        [SerializeField] private float _defense = 0;
        [SerializeField] private float _holdingDamage;
        private bool _isHolding;

        public override void MoveForward()
        {
            StopHolding();
            base.MoveForward();
        }

        public override void MoveBackWards()
        {
            StopHolding();
            base.MoveBackWards();
        }

        public override void StopMoving()
        {
            StartHolding();
            base.StopMoving();
        }

        public override void ApplyDamage(float damage)
        {
            if (_isHolding)
            {
                damage -= _defense;
                if (damage < 0)
                    damage = 0;
            }

            base.ApplyDamage(damage);
        }

        private void StartHolding()
        {
            if (_isHolding)
                return;

            _isHolding = true;
            AttackController.TriggerEntered += DoSpearDamage;
        }

        private void StopHolding()
        {
            if (!_isHolding)
                return;

            _isHolding = false;
            AttackController.TriggerEntered -= DoSpearDamage;
        }

        private void DoSpearDamage(Unit enemy)
        {
            enemy.StopMoving();
            enemy.ApplyDamage(_holdingDamage);
        }
    }
}
