using System.Threading;
using _Scripts.Network;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.MVP.DogBreed
{
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