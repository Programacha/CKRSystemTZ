using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVP.DogBreed
{
    public class PopupView : MonoBehaviour
    {
        public TextMeshProUGUI _titleText;
        public TextMeshProUGUI _descriptionText;
        public Button _closeButton;

        public void ShowPopup(string title, string description)
        {
            gameObject.SetActive(true);
            _titleText.text = title;
            _descriptionText.text = description;
        }

        public void HidePopup()
        {
            gameObject.SetActive(false);
        }
    }
}