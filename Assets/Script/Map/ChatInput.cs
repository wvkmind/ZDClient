using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatInput : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image bk;
    private UnityEngine.UI.InputField _inputField;
    private Vector2 _adaptPanelOriginPos;
    private RectTransform _adaptPanelRt;
    private float RESOULUTION_HEIGHT = 1920F;
    
    void Start()
    {
        _inputField = this.GetComponent<UnityEngine.UI.InputField>();
        _inputField.onEndEdit.AddListener(OnEndEdit);
        _adaptPanelRt = bk.GetComponent<RectTransform>(); 
        _adaptPanelOriginPos = _adaptPanelRt.anchoredPosition;
    }
    private void OnEndEdit(string currentInputString) {
        _adaptPanelRt.anchoredPosition = _adaptPanelOriginPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputField.isFocused) {
            float keyboardHeight = GetH() * RESOULUTION_HEIGHT / Screen.height;   
            _adaptPanelRt.anchoredPosition = Vector3.up * keyboardHeight;
        }
    }
    float GetH()
    {
        if(TouchScreenKeyboard.area.height==0)return 400;//for test
        else return TouchScreenKeyboard.area.height;
    }
}
