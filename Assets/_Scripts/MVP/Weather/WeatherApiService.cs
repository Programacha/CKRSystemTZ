using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Threading;

public class WeatherApiService
{
    private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

    public async UniTask<string> GetWeatherData(CancellationToken cancellationToken)
    {
        using (var request = UnityWebRequest.Get(ApiUrl))
        {
            await request.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка получения погоды: " + request.error);
                return null;
            }

            return request.downloadHandler.text;
        }
    }
}