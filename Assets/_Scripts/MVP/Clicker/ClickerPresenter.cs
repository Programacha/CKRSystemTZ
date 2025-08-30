using UniRx;
using Zenject;
using System;

namespace _Scripts.MVP.Clicker
{
    public class ClickerPresenter : IInitializable, IDisposable, ITabPresenter
    {
        private readonly ClickerModel _model;
        private readonly ClickerView _view;
        private readonly GameSettings _settings;
        private readonly CompositeDisposable _disposables = new(); 
        private readonly CompositeDisposable _timerDisposables = new(); 

        public ClickerPresenter(ClickerModel model, ClickerView view, GameSettings settings)
        {
            _model = model;
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _view.Init(this);
            _model.Money.Subscribe(value => _view.UpdateCurrency(value)).AddTo(_disposables);
            _model.Energy.Subscribe(value => _view.UpdateEnergy(value)).AddTo(_disposables);
        }

        public void OnTabActivated()
        {
            Observable.Interval(TimeSpan.FromSeconds(_settings.AutoCollectInterval))
                .Subscribe(_ => OnAutoCollect())
                .AddTo(_timerDisposables);
            
            Observable.Interval(TimeSpan.FromSeconds(_settings.EnergyRegenInterval))
                .Subscribe(_ => OnEnergyRegen())
                .AddTo(_timerDisposables);
        }

        public void OnTabDeactivated()
        {
            _timerDisposables.Clear();
        }

        public void OnTap()
        {
            if (_model.Energy.Value > 0)
            {
                _model.AddMoney(_settings.TapCurrencyAmount);
                _model.SpendEnergy(_settings.TapEnergyCost);
            }
        }
    
        private void OnAutoCollect()
        {
            if (_model.Energy.Value > 0)
            {
                _model.AddMoney(_settings.TapCurrencyAmount);
                _model.SpendEnergy(_settings.TapEnergyCost);
            }
        }

        private void OnEnergyRegen()
        {
            _model.AddEnergy(_settings.EnergyRegenAmount);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _timerDisposables.Dispose();
        }
    }
}