using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class CardGameManager : MonoBehaviour
{
    public TextMeshPro TextoATK;
    public TextMeshPro TextoDEF;
    public VuforiaBehaviour prueba;

    public ARCard carta;

    private ARCard[] cartasEnEscena; // Lista de cartas en escena 
    private List<ARCard> cartasTrackeadas; // Lista de cartas trackeadas 

    public int nCartasTrackeadas = 0; // Cuenta de cuantas cartas han sido trackeadas 

    public GameObject botonCombate; // Boton


    private int nCartasDerrotadas;


  


    public Slider sliderJ1;
    public Slider sliderJ2;



    public Transform PosJ1;
    public Transform PosJ2;

    private ARCard cartaJ1;
    private ARCard cartaJ2;

    //public int CartasEnEscena;


    void Start()
    {
        //sliderJ1.value

        cartasEnEscena = FindObjectsOfType<ARCard>();

        prueba.SetMaximumSimultaneousTrackedImages(10);
    }

    // Update is called once per frame
    void Update()
    {
        //ActualizaListaCartas();
        CompruebaBotonCombate();
        // CompruebaPropietarioCartas();
        checkCartasTrackeadas();

    }

    private void checkCartasTrackeadas()
    {
        nCartasTrackeadas = 0;
        cartasTrackeadas.Clear();
        
        foreach(ARCard carta in cartasEnEscena) 
        {
          if(carta.GetStatus() != Status.NO_POSE)
            {
                nCartasTrackeadas++;
                cartasTrackeadas.Add(carta);
            }    
        
        }
    }


 

    public void Combate()
    {
        /*
        int statJ1 = cartaJ1.GetCardStat();
       
        int statJ2 = cartaJ2.GetCardStat();

        if(statJ1 > statJ2) 
        {
            cartaJ2.PonEstado(ARCard.CardPose.DESTROYED);
            sliderJ2.value -= statJ1 - statJ2;
            nCartasDerrotadas++;
        }

        else if (statJ2 > statJ1)
        
        {
            cartaJ1.PonEstado(ARCard.CardPose.DESTROYED);
            sliderJ1.value -= statJ2 - statJ1;
            nCartasDerrotadas++;
        }

        else
        {
            cartaJ1.PonEstado(ARCard.CardPose.DESTROYED);
            cartaJ2.PonEstado(ARCard.CardPose.DESTROYED);
            nCartasDerrotadas+= 2;
        }*/

        CheckGameOver();
    }

    private void CompruebaBotonCombate() // CheckBotonCombate
    {
        if(nCartasTrackeadas >= 2) // Si hay 2 o mas cartas trackeadas, comprobamos estadados
        {
            botonCombate.SetActive(true);
        }

        else { botonCombate.SetActive(false);}
    }

    private void CompruebaPropietarioCartas()
    {
        /*
        if(nCartasTrackeadas >= 2)
        {
            Transform posC1 = CartasTrackeadas[0].transform; // Posicion carta numero 1
            Transform posC2 = CartasTrackeadas[1].transform; // Posicion carta numero 2

            // Calculamos distancias entre cada jugador y las dos cartas

            float dist_J1_C1 = Vector3.Distance(PosJ1.position, posC1.position);
            float dist_J1_C2 = Vector3.Distance(PosJ1.position, posC2.position);

            float dist_J2_C1 = Vector3.Distance(PosJ2.position, posC1.position);
            float dist_J2_C2 = Vector3.Distance(PosJ2.position, posC2.position);

            if (dist_J1_C1 > dist_J1_C2)
            {
                cartaJ1 = CartasTrackeadas[0];
                cartaJ2 = CartasTrackeadas[1];
            }
            else
            {
                cartaJ1 = CartasTrackeadas[1];
                cartaJ2 = CartasTrackeadas[0];
            }

            cartaJ1.AsignaPropietario(false);
            cartaJ2.AsignaPropietario(true);


            if (dist_J2_C1 < dist_J2_C2) ;
            else;

            

            
        }*/
    }

    private void CheckGameOver()
    {
        if(sliderJ1.value <= 0)
        {

        }

        else if (sliderJ2.value <= 0)
        {
             
        }

        if(nCartasDerrotadas >= cartasEnEscena.Length-1)
        {
            //Gana el que tenga mas vida
            if (sliderJ1.value > sliderJ2.value)
            {
                // Gana J2
            }

            else if (sliderJ2.value > sliderJ1.value)
            {
                // Gana J1
            }

            else; // Empatan
        }
    }
}
