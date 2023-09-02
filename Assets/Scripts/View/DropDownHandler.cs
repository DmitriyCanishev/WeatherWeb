using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace View
{
    public class DropDownHandler : ViewController
    {
        [SerializeField] private WeatherDataUrls _weatherDataUrls = null;

        private TMP_Dropdown _dropDown;
        private List<string> _dropdownItems;

        public void DropDownItemSelected(TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            AppState.SetCityIndex(index);
        }

        private void Awake()
        {
            _dropDown = GetComponent<TMP_Dropdown>();
            _dropdownItems = new List<string>(_weatherDataUrls.WeatherUrls.Count);
        }

        private void Start()
        {
            _dropDown.options.Clear();
            _dropDown.captionText.text = _weatherDataUrls.WeatherUrls[0].CityName;
        
            FillDropDownItems();
            DropDownFillOptions();
        }

        private void FillDropDownItems()
        {
            foreach (var item in _weatherDataUrls.WeatherUrls)
            {
                _dropdownItems.Add(item.CityName);
            }
        }

        private void DropDownFillOptions()
        {
            foreach (var item in _dropdownItems)
            {
                _dropDown.options.Add(new TMP_Dropdown.OptionData() {text = item});
            }
        }
    }
}