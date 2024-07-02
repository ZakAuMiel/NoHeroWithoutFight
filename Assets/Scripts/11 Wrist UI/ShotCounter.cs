using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShotCounter : MonoBehaviour
{
    
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
       
        UpdateText();
    }
    private void OnDestroy()
    {
        
    }

    public void ResetCounter()
    {
       
        UpdateText();
    }

    private void IncreaseCounter()
    {
        
        UpdateText();
    }

    private void UpdateText()
    {
       
    }
}
