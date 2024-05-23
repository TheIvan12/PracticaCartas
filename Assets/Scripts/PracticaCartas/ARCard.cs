using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class ARCard : MonoBehaviour
{
    public enum CardPose { DEFAULT, ATTACK, DEFEND, DESTROYED };
    public CardPose estado = CardPose.DEFAULT;

    public ImageTargetBehaviour target;

    public int ATK = 0;
    public int DEF = 0;

    public GameObject indicadorJ1, indicadorJ2;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<ImageTargetBehaviour>();

        if (target.TargetStatus.Status == Status.NO_POSE)
        {
            Debug.Log("el target" + target.TargetName + "no esta en pantalla");
        }

        else Debug.Log("el target" + target.TargetName + " esta en pantalla");

    }
    public void PonPosicionAtaque()
    {
        estado = CardPose.ATTACK;
        //ActualizaSegunEstado();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void ActualizaEstado()
    {
        switch(estado)
        {
            case CardPose.DEFAULT:
                break;
            case CardPose.ATTACK:
                break;
            case CardPose.DEFEND:
                break;
            case CardPose.DESTROYED:
                break;
            default:
                break;
        }

    }

    public int GetCardStat()
    {
        /*if (pose == CardPose.ATTACK)
        {
            return ATK;
        }

        else if (pose == CardPose.DEFEND)
        {
            return DEF;
        }

        else return -1;*/

        return (estado == CardPose.ATTACK) ? ATK : DEF;

    }

    public void PonEstado()
    {

    }


   public Status GetStatus()
    {
      return target.TargetStatus.Status;
    
    }

    public void AsignaPropietario(bool j2)
    {
        indicadorJ1.SetActive(!j2);
        indicadorJ2.SetActive(j2);
    }


}
