using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.MVP.DogBreed
{
    public class DogFactsPresenter : IInitializable, System.IDisposable, ITabPresenter
    {
        private readonly DogFactsModel _model;
        private readonly DogFactsView _view;
        private readonly PopupView _popupView;
        private readonly NetworkRequestQueue _queue;
        private readonly DogApiService _apiService;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly CompositeDisposable _disposables = new ();

        public DogFactsPresenter(
            DogFactsModel model,
            DogFactsView view,
            PopupView popupView,
            NetworkRequestQueue queue,
            DogApiService apiService)
        {
            _model = model;
            _view = view;
            _popupView = popupView;
            _queue = queue;
            _apiService = apiService;
        }

        public void Initialize()
        {
            _view.Init(this);
            _popupView._closeButton.OnClickAsObservable().Subscribe(_ => _popupView.HidePopup()).AddTo(_disposables);

            _model.IsLoading.Subscribe(isLoading => _view.ShowLoadingIndicator(isLoading)).AddTo(_disposables);
            _model.BreedsList.Subscribe(breeds => _view.DisplayBreedsList(breeds)).AddTo(_disposables);
        }

        public void OnTabActivated()
        {
            Debug.Log("OnTabActivated");
            _cancellationTokenSource = new CancellationTokenSource();
            
            _queue.AddRequest(new BreedsListRequest(_model, _apiService, _cancellationTokenSource.Token));
        }

        public void OnTabDeactivated()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            
            _queue.CancelAndRemoveRequest<BreedsListRequest>();
            _queue.CancelAndRemoveRequest<BreedFactsRequest>();
            _popupView.HidePopup();
        }

        public void OnBreedClicked(string breedId)
        {
            // Отменяем предыдущий запрос и удаляем его из очереди, если он есть
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            _cancellationTokenSource = new CancellationTokenSource();
        
            // Добавляем новый запрос
            _queue.AddRequest(new BreedFactsRequest(_model, _apiService, breedId, _cancellationTokenSource.Token, this));
        }

        public void Dispose()
        {
            _disposables.Dispose();
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
            }
        }
        
        public void ShowFactsPopup(string title, string description)
        {
            _popupView.ShowPopup(title, description);
        }
    }
}