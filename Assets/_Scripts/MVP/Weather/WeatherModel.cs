using UniRx;

namespace _Scripts.MVP.Weather
{
    public class WeatherModel
    {
        public ReactiveProperty<string> WeatherInfo { get; private set; } = new("Загрузка...");
    }
}