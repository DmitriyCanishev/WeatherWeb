using App;
using UnityEngine;

namespace View
{
    public abstract class ViewController : MonoBehaviour
    {
        private AppState _appState = null;
        
        protected AppState AppState 
        {
            get
            {
                return _appState = _appState == null 
                    ? FindObjectOfType<AppState>() 
                    : _appState;
            }
        }
    }
}