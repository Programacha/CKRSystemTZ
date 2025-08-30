using UniRx;
using UnityEngine;

namespace _Scripts.MVP.Clicker
{
    public class ClickerModel
    {
        public ReactiveProperty<int> Money { get; private set; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> Energy { get; private set; } = new ReactiveProperty<int>(1000); 
    
        private readonly GameSettings _settings;

        public ClickerModel(GameSettings settings)
        {
            _settings = settings;
            Energy.Value = settings.MaxEnergy;
        }

        public void AddMoney(int amount)
        {
            Money.Value += amount;
        }

        public void SpendEnergy(int amount)
        {
            Energy.Value = Mathf.Max(0, Energy.Value - amount);
        }
    
        public void AddEnergy(int amount)
        {
            Energy.Value = Mathf.Min(_settings.MaxEnergy, Energy.Value + amount);
        }
    }
}