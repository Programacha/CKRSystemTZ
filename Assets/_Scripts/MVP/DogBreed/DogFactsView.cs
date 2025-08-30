using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVP.DogBreed
{
    public class DogFactsView : MonoBehaviour
    {
        public RectTransform _listParent;
        public GameObject _listItemPrefab;
        public GameObject _loadingIndicator;

        private DogFactsPresenter _presenter;

        public void Init(DogFactsPresenter presenter)
        {
            _presenter = presenter;
        }

        public void ShowLoadingIndicator(bool show)
        {
            _loadingIndicator.SetActive(show);
        }

        public void DisplayBreedsList(List<DogBreedParsing.BreedData> breeds)
        {
            foreach (Transform child in _listParent)
            {
                Destroy(child.gameObject);
            }
            
            int i = 1;
            foreach (var breed in breeds)
            {
                var listItem = Instantiate(_listItemPrefab, _listParent);
                listItem.GetComponentInChildren<TextMeshProUGUI>().text = $"{i} - {breed.attributes.name}";
                listItem.GetComponent<Button>().onClick.AddListener(() => _presenter.OnBreedClicked(breed.id));
                i++;
            }
        }
    }
}