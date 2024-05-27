using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextoSlider : MonoBehaviour
{
    public Slider slider;
    private TextMeshProUGUI texto; // Texto que actualizamos para copiar al slider

  
    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        ActualizaTexto();
    }

    public void ActualizaTexto()
    {
        texto.text = slider.value.ToString();
    }
    
}
