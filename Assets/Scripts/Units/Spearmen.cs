using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Spearmen : Unit
    {
        [SerializeField] private float _defense = 0;
        [SerializeField] private float _holdingDamage;
        [SerializeField] private float _movementDelay;
        private bool _isHolding;

        public override void MoveForward()
        {
            base.MoveForward();
            StopHolding();
        }

        public override void MoveBackWards()
        {
            base.MoveBackWards();
            StopHolding();
        }

        public override void StopMoving()
        {
            base.StopMoving();
            StartHolding();
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
            
            base.StopMoving();
            Invoke("StartMoving", _movementDelay);
        }

        private new void StartMoving()
        {
            base.StartMoving();
        }

        private void DoSpearDamage(Unit enemy)
        {
            enemy.StopMoving();
            enemy.ApplyDamage(_holdingDamage);
        }
    }
}
