using System;
using System.Threading;
using _Scripts.Network;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.MVP.Weather
{
    public class WeatherRequest : IRequest
    {
        private readonly WeatherApiService _apiService;
        private readonly WeatherModel _model;
        private readonly CancellationToken _cancellationToken;

        public WeatherRequest(WeatherApiService service, WeatherModel model, CancellationToken token)
        {
            _apiService = service;
            _model = model;
            _cancellationToken = token;
        }

        public async UniTask Execute()
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cancellationToken);
    
            try
            {
                var jsonData = await _apiService.GetWeatherData(linkedCts.Token);
        
                if (jsonData != null)
                {
                    var response = JsonUtility.FromJson<WeatherForecastResponse>(jsonData);

                    if (response != null && response.properties.periods.Length > 0)
                    {
                        var todayForecast = response.properties.periods[0];
                        
                        string formattedString = $"Сегодня {todayForecast.temperature}{todayForecast.temperatureUnit}";
                        
                        _model.WeatherInfo.Value = formattedString;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Запрос на погоду был отменен.");
            }
            finally
            {
                linkedCts.Dispose();
            }
        }
    }
    
    [Serializable]
    public class WeatherForecastResponse
    {
        public Properties properties;
    }

    [Serializable]
    public class Properties
    {
        public Period[] periods;
    }

    [Serializable]
    public class Period
    {
        public string name;
        public int temperature;
        public string temperatureUnit;
        public string shortForecast;
        public string icon;
    }
}