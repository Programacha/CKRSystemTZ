using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Scripts.MVP.DogBreed
{
    public class DogApiService
    {
        private const string BaseApiUrl = "https://dogapi.dog/api/v2";

        public async UniTask<string> GetBreedsList(CancellationToken cancellationToken)
        {
            string url = $"{BaseApiUrl}/breeds";
            return await SendRequest(url, cancellationToken);
        }

        public async UniTask<string> GetBreedFacts(string breedId, CancellationToken cancellationToken)
        {
            string url = $"{BaseApiUrl}/breeds/{breedId}";
            return await SendRequest(url, cancellationToken);
        }

        private async UniTask<string> SendRequest(string url, CancellationToken cancellationToken)
        {
            using (var request = UnityWebRequest.Get(url))
            {
                await request.SendWebRequest().ToUniTask(cancellationToken: cancellationToken);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error fetching data from {url}: {request.error}");
                    return null;
                }

                return request.downloadHandler.text;
            }
        }
    }
}