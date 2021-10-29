using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    [Header("Space between menu items")]
    [SerializeField] private Vector2 _spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Ease _rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField] private float _expandDuration;
    [SerializeField] private float _collapseDuration;
    [SerializeField] private Ease _expandEase;
    [SerializeField] private Ease _collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] private float _expandFadeDuration;
    [SerializeField] private float _collapseFadeDuration;

    private Button _mainButton;
    private SettingsMenuItem[] _itemMenu;
    private bool _isExpanded = false; 

    private Vector2 _mainButtonPosition;
    private int _itemCount;

    private void Start()
    {
        _itemCount = transform.childCount - 1;
        _itemMenu = new SettingsMenuItem[_itemCount];

        for (int i = 0; i < _itemCount; i++)
        {
            _itemMenu[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }

        _mainButton = transform.GetChild(0).GetComponent<Button>();
        _mainButton.transform.SetAsLastSibling();

        _mainButton.onClick.AddListener(ToggleMenu);

        _mainButtonPosition = _mainButton.transform.position;

        ResetPosition();        
    }
    private void ResetPosition()
    {
        for (int i = 0; i < _itemCount; i++)
        {
            _itemMenu[i].Trans.position = _mainButtonPosition;
        }
    }
    private void ToggleMenu()
    {
        _isExpanded = !_isExpanded;

        if (_isExpanded)
        {
            for (int i = 0; i < _itemCount; i++)
            {
                //_itemMenu[i].Trans.position = _mainButtonPosition + _spacing * (i + 1);
                _itemMenu[i].Trans.DOMove(_mainButtonPosition + _spacing * (i + 1), _expandDuration).SetEase(_expandEase);
                _itemMenu[i].Image.DOFade(1f, _expandFadeDuration).From(0f);
            }            
        }
        else
        {
            for (int i = 0; i < _itemCount; i++)
            {
                //_itemMenu[i].Trans.position = _mainButtonPosition;
                _itemMenu[i].Trans.DOMove(_mainButtonPosition, _collapseDuration).SetEase(_collapseEase);
                _itemMenu[i].Image.DOFade(0f, _collapseFadeDuration);
            }
        }
        _mainButton.transform.DORotate(Vector3.forward * 180f, _rotationDuration).From(Vector3.zero).SetEase(_rotationEase);
    }

    public void OnItemClick(int index)
    {
        Debug.Log("item" + index + "clicked");

        switch (index)
        {
            case (0):
                Debug.Log("Music");
                break;
            case (1):
                Debug.Log("Sound");
                break;
            case (2):
                Debug.Log("Vibration");
                break;
        }
    }
    private void OnDestroy()
    {
        _mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
