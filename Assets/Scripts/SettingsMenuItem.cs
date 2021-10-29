using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{   
    [HideInInspector] public Image Image;
    [HideInInspector] public Transform Trans;

    private SettingsMenu _settingsMenu;
    private int _index;
    private Button _button;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Trans = transform;

        _settingsMenu = Trans.parent.GetComponent<SettingsMenu>();
        _index = Trans.GetSiblingIndex() - 1;

        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnItemClick);
    }
    private void OnItemClick()
    {
        _settingsMenu.OnItemClick(_index);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnItemClick);
    }
}


