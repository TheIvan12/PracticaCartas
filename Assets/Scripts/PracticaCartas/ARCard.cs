using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;
public enum CardPose { DEFAULT, ATTACK, DEFENSE, DESTROYED };
public class ARCard : MonoBehaviour
{
    public CardPose estado = CardPose.DEFAULT;

    public ImageTargetBehaviour target;

    public int ATK = 0;
    public int DEF = 0;

    public TextMeshPro textoATK;
    public TextMeshPro textoDEF;
    public GameObject botonATK;
    public GameObject botonDEF;  
    public GameObject modelo;
    public GameObject spriteDerrota;
    public GameObject spriteDEFENSA;
    public GameObject indicadorJ1, indicadorJ2;
 
    void Start()
    {
        target = GetComponent<ImageTargetBehaviour>();
        estado = CardPose.DEFAULT;
        ActualizaTextos();
        ActualizaSegunEstado();

    }

    void Update()
    {

    }

    public void ResetCarta()
    {
        estado = CardPose.DEFAULT;   
        ActualizaSegunEstado();    
        indicadorJ1.SetActive(false);  
        indicadorJ2.SetActive(false);
    }

    private void ActualizaTextos()
    {
        textoATK.text = "ATK: \n" + ATK.ToString();
        textoDEF.text = "DEF: \n" + DEF.ToString();
    }

    private void ActualizaSegunEstado()
    {
        switch (estado)
        {
            case CardPose.DEFAULT:
                botonATK.SetActive(true);
                botonDEF.SetActive(true);
                modelo.SetActive(false);
                spriteDEFENSA.SetActive(false);
                spriteDerrota.SetActive(false);
                break;
            case CardPose.ATTACK:
                botonATK.SetActive(false);
                botonDEF.SetActive(false);
                modelo.SetActive(true);
                spriteDEFENSA.SetActive(false);
                spriteDerrota.SetActive(false);
                break;
            case CardPose.DEFENSE:
                botonATK.SetActive(false);
                botonDEF.SetActive(false);
                modelo.SetActive(false);
                spriteDEFENSA.SetActive(true);
                spriteDerrota.SetActive(false);
                break;
            case CardPose.DESTROYED:
                botonATK.SetActive(false);
                botonDEF.SetActive(false);
                modelo.SetActive(false);
                spriteDEFENSA.SetActive(false);
                spriteDerrota.SetActive(true);
                break;
        }

    }

    public int GetCardStat()
    {  
        return (estado == CardPose.ATTACK) ? ATK : DEF;
    }

    public void PonEstado(CardPose fase)
    {
        estado = fase;
        ActualizaSegunEstado();
    }

    public void PonPosicionAtaque()
    {   
        estado = CardPose.ATTACK; // Cambia el estado de la carta
        ActualizaSegunEstado();
    }

    public void PonPosicionDefensa()
    {
        Debug.Log("Defensa");
        estado = CardPose.DEFENSE; // Cambia el estado de la carta
        ActualizaSegunEstado();
    }

    public void PonPosicionDestruido()
    {
        estado = CardPose.DESTROYED; // Cambia el estado de la carta
        ActualizaSegunEstado();
    }



   public Status GetStatus()
    {
      return target.TargetStatus.Status;
    
    }

    public void AsignaPropietario(bool j1)
    {
        indicadorJ1.SetActive(j1);
        indicadorJ2.SetActive(!j1);

        if (j1 == true)
        {
            indicadorJ1.SetActive(true);
            indicadorJ2.SetActive(false);
        }

        else
        {
            indicadorJ1.SetActive(false);
            indicadorJ2.SetActive(true);
        }
    }




}
