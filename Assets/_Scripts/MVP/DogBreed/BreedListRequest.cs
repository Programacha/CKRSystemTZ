using System.Threading;
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
    
    public class BreedFactsRequest :IRequest
    {
        private readonly DogFactsModel _model;
        private readonly DogApiService _apiService;
        private readonly string _breedId;
        private readonly CancellationToken _token;
        private readonly DogFactsPresenter _presenter;

        public BreedFactsRequest(DogFactsModel model, DogApiService apiService, string breedId, CancellationToken token, DogFactsPresenter presenter)
        {
            _model = model;
            _apiService = apiService;
            _breedId = breedId;
            _token = token;
            _presenter = presenter;
        }

        public async UniTask Execute()
        {
            _model.IsLoading.Value = true;
            try
            {
                var jsonData = await _apiService.GetBreedFacts(_breedId, _token);
                if (jsonData != null)
                {
                    var response = JsonUtility.FromJson<DogBreedParsing.BreedFactsResponse>(jsonData);
                    _model.CurrentBreedFacts.Value = response;
                    
                    _presenter.ShowFactsPopup(response.data.attributes.name, response.data.attributes.description);
                }
            }
            catch (System.OperationCanceledException)
            {
                Debug.Log("Запрос на факты о породе отменен.");
            }
            finally
            {
                _model.IsLoading.Value = false;
            }
        }
    }
}