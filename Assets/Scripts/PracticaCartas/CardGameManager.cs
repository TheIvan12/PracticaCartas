using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using static ARCard;


public class CardGameManager : MonoBehaviour
{
    [SerializeField] public ARCard[] cartasEnEscena;
    [SerializeField] private List<ARCard> cartasTrackeadas;



    //Nº cartas traqueadas
    public int nCartasTrackeadas = 0;
    public GameObject botonCombate;
    public GameObject botonReset;
    public Slider sliderJ1;
    public Slider sliderJ2;
    public int vidaMaxima = 4000;
    public Transform posJ1;
    public Transform posJ2;
    private ARCard cartaJ1;
    private ARCard cartaJ2;
    private int nCartasDerrotadas = 0;
    public TextMeshProUGUI TextoVictoriaJ1;
    public TextMeshProUGUI TextoVictoriaJ2;
    public TextMeshProUGUI TextoEmpate;

    private bool gameOver = false;
   
    // Start is called before the first frame update
    void Start()
    {
        
        cartasEnEscena = FindObjectsOfType<ARCard>();

        TextoVictoriaJ1.gameObject.SetActive(false);
        TextoVictoriaJ2.gameObject.SetActive(false); 
        TextoEmpate.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ActualizaListaCartas();
        CompruebaBotonCombate();
        CompruebaPropietarioCartas();
    }

    public void ResetGame()
    {
        //Reseteamos todas las cartas
        foreach (ARCard card in cartasEnEscena)
        {
            card.ResetCarta();
            TextoVictoriaJ1.gameObject.SetActive(false);
            TextoVictoriaJ2.gameObject.SetActive(false);
            TextoEmpate.gameObject.SetActive(false);
        }
        //Reset cartas destruidas
        nCartasDerrotadas = 0;
        //Reset a la vida de cada Jugador
        sliderJ1.maxValue = sliderJ1.value = vidaMaxima;
        sliderJ2.maxValue = sliderJ2.value = vidaMaxima;

       
    }

    public void Combate()
    {
        ARCard cartaJ1 = cartasTrackeadas[0];
        ARCard cartaJ2 = cartasTrackeadas[1];
        //Cogemos las 2 primeras cartas traqueadas
        //Obtendremos sus estadisticas (ATK si está en ataque, DEF si está en defensa)
        int statJ1 = cartaJ1.GetCardStat();
        int statJ2 = cartaJ2.GetCardStat();

        //Comparamos las dos estadísticas 
        //Si gana un jugador, el otro pierde la diferencia de estadísstica en vida 
        //La carta que pierde se destruye 
        //Si hay empate, se destruyen las dos
        if (statJ1 > statJ2) //Gana J1
        {
            //Destruimos carta J2
            cartaJ2.PonEstado(CardPose.DESTROYED);
            nCartasDerrotadas++;
            //Restar vida a J2
            sliderJ2.value -= statJ1 - statJ2;
        }
        else if (statJ2 > statJ1) //Gana J2
        {
            //Restar vida a J1
            cartaJ1.PonEstado(CardPose.DESTROYED);
            nCartasDerrotadas++;
            //Restar vida a J2
            sliderJ1.value -= statJ2 - statJ1;
        }
        else //Empatan
        {
            cartaJ1.PonEstado(CardPose.DESTROYED);
            cartaJ2.PonEstado(CardPose.DESTROYED);
            nCartasDerrotadas += 2;
        }
        CheckGameOver();
    }
    private void CompruebaPropietarioCartas()
    {
        if (nCartasTrackeadas >= 2)
        {
            Transform posC1 = cartasTrackeadas[0].transform; // posición carta nº 1 
            Transform posC2 = cartasTrackeadas[1].transform; // posición carta nº 2 

            float dist_J1_C1 = Vector3.Distance(posJ1.position, posC1.position);
            float dist_J1_C2 = Vector3.Distance(posJ1.position, posC2.position);

            float dist_J2_C1 = Vector3.Distance(posJ2.position, posC1.position);
            float dist_J2_C2 = Vector3.Distance(posJ2.position, posC2.position);

            if (dist_J1_C1 < dist_J1_C2)
            {
                cartaJ1 = cartasTrackeadas[0]; // C1 es de J1
                cartaJ2 = cartasTrackeadas[1]; // C2 es de J2

            }
            else
            {
                cartaJ1 = cartasTrackeadas[1]; // C2 es de J1
                cartaJ2 = cartasTrackeadas[0]; // C1 es de J2
            }
            cartaJ1.AsignaPropietario(true);
            cartaJ2.AsignaPropietario(false);
        }
    }

    public void CompruebaBotonCombate()
    {
        // Si hay dos o mas cartas traqueadas, activamos botón combate 
        if (nCartasTrackeadas >= 2) // Si hay dos o más cartas traqueadas, activamos botón combate
        {
            ARCard carta1 = cartasTrackeadas[0];
            ARCard carta2 = cartasTrackeadas[1];

            // Si las dos cartas están en ataque o  en defensa, activamos botón combate 
            if ((carta1.estado == CardPose.ATTACK || carta1.estado == CardPose.DEFENSE) &&
               (carta2.estado == CardPose.ATTACK || carta2.estado == CardPose.DEFENSE))
            {
                botonCombate.SetActive(true);
            }
            //else 
            //botonCombate.SetActive(false);
            //botonReset.SetActive(false);


        }
        else
        {
            botonCombate.SetActive(false);
        }

    }
    private void ActualizaListaCartas()
    {
        //Vacia la lista de cartas traqueadas
        cartasTrackeadas.Clear();
        nCartasTrackeadas = 0;

        foreach (ARCard carta in cartasEnEscena)
        {
            if (carta.GetStatus() != Vuforia.Status.NO_POSE)
            {
                cartasTrackeadas.Add(carta);
                nCartasTrackeadas++;
            }
        }
    }
    private void CheckGameOver()
    {
        if (sliderJ1.value <= 0) // Pierde J2
        {
           
            TextoVictoriaJ1.gameObject.SetActive(true);
            botonReset.SetActive(true);
            gameOver = true;


        }
        else if (sliderJ2.value <= 0) // Pierde J1
        {
            
            TextoVictoriaJ2.gameObject.SetActive(true);
            botonReset.SetActive(true);
            gameOver = true;

        }
        else if (nCartasDerrotadas >= cartasEnEscena.Length - 1)
        {
            // Gana el que tenga más vida
            if (sliderJ1.value > sliderJ2.value)
            {
               
                TextoVictoriaJ1.gameObject.SetActive(true);
                botonReset.SetActive(true);
                gameOver = true;


            }
            else if (sliderJ2.value > sliderJ1.value)
            {
                
                TextoVictoriaJ2.gameObject.SetActive(true);
                botonReset.SetActive(true);
                gameOver = true;
            }
            else //empatan
            {
              
                TextoEmpate.gameObject.SetActive(true);
                botonReset.SetActive(true);
                gameOver = true;
            }
        }
    }
}
