using UniRx;

namespace _Scripts.MVP.Weather
{
    public class WeatherModel
    {
        public const string DOWNLOAD = "Загрузка...";
        public ReactiveProperty<string> WeatherInfo { get; private set; } = new(DOWNLOAD);
    }
}