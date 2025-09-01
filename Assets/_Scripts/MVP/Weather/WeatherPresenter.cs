using System;
using System.Threading;
using _Scripts.Network;
using UniRx;

namespace _Scripts.MVP.Weather
{
    public class WeatherPresenter : IDisposable, ITabPresenter
    {
        private readonly WeatherModel _model;
        private readonly WeatherView _view;
        private readonly NetworkRequestQueue _queue;
        private readonly WeatherApiService _apiService;
    
        private CancellationTokenSource _cancellationTokenSource;
        private readonly CompositeDisposable _disposables = new();

        public WeatherPresenter(WeatherModel model, WeatherView view, NetworkRequestQueue queue, WeatherApiService apiService)
        {
            _model = model;
            _view = view;
            _queue = queue;
            _apiService = apiService;
        }

        public void OnTabActivated()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            Observable.Interval(TimeSpan.FromSeconds(5))
                .Subscribe(_ => RequestWeather())
                .AddTo(_disposables);
            
            _model.WeatherInfo.Subscribe(info => _view.SetWeatherInfo(info)).AddTo(_disposables);
        }
    
        public void OnTabDeactivated()
        {
            _model.WeatherInfo.Value = "Загрузка...";
            _disposables.Clear();
            
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            
            _queue.CancelAndRemoveRequest<WeatherRequest>();
        }

        private void RequestWeather()
        {
            var request = new WeatherRequest(_apiService, _model, _cancellationTokenSource.Token);
            
            _queue.AddRequest(request);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
            }
        }
    }
}