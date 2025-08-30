using TMPro;
using UnityEngine;

namespace _Scripts.MVP.Weather
{
    public class WeatherView : MonoBehaviour
    {
        public TextMeshProUGUI _weatherText;
    
        public void SetWeatherInfo(string info)
        {
            Debug.Log(info);
            _weatherText.text = info;
        }
    }
}