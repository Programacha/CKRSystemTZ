using System.Threading;
using _Scripts.Network;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.MVP.DogBreed
{
    public class BreedsListRequest : IRequest
    {
        private readonly DogFactsModel _model;
        private readonly DogApiService _apiService;
        private readonly CancellationToken _token;

        public BreedsListRequest(DogFactsModel model, DogApiService apiService, CancellationToken token)
        {
            _model = model;
            _apiService = apiService;
            _token = token;
        }

        public async UniTask Execute()
        {
            _model.IsLoading.Value = true;
            try
            {
                var jsonData = await _apiService.GetBreedsList(_token);
                if (jsonData != null)
                {
                    var response = JsonUtility.FromJson<DogBreedParsing.BreedsResponse>(jsonData);

                    if (response != null && response.data != null)
                    {
                        var breeds = new System.Collections.Generic.List<DogBreedParsing.BreedData>();
                        for (int i = 0; i < Mathf.Min(10, response.data.Length); i++)
                        {
                            breeds.Add(response.data[i]);
                        }
                        _model.BreedsList.Value = breeds;
                    }
                }
            }
            catch (System.OperationCanceledException)
            {
                Debug.Log("Запрос на список пород отменен.");
            }
            finally
            {
                _model.IsLoading.Value = false;
            }
        }
    }
}