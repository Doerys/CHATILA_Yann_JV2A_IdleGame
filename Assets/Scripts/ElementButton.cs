using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class ElementButton : MonoBehaviour
{
    public ElementScriptableObject dataElement;

    public PlayerManager myPlayer;
    public SceneManager sceneData;

    public ColorBlock colorsButton;

    public Image spriteButton, squareButton;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerManager>();

        sceneData = FindObjectOfType<SceneManager>();

        spriteButton.sprite = dataElement.spriteElement;

        colorsButton = GetComponent<Button>().colors;

        colorsButton.normalColor = new Color(.6f, .6f, .6f);

        // on colorise l'élément qu'on vient de select
        switch (dataElement.element)
        {
            case ElementsPlayer.Fire:
                colorsButton.selectedColor = new Color(1f, .4f, .4f);
                break;
            case ElementsPlayer.Water:
                colorsButton.selectedColor = new Color(.4f, .6f, 1f);
                break;
            case ElementsPlayer.Thunder:
                colorsButton.selectedColor = new Color(.7f, .4f, 1f);
                break;
            case ElementsPlayer.Earth:
                colorsButton.selectedColor = new Color(1f, .7f, .4f);
                break;
            case ElementsPlayer.Light:
                colorsButton.selectedColor = new Color(1f, 1f, .4f);
                break;
            case ElementsPlayer.Nothing:
                colorsButton.selectedColor = new Color(.6f, .6f, .6f);
                break;
            default:
                break;
        }

        button.colors = colorsButton;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickElementButton()
    {
        // on remet tout à gris
        for (int i = 0; i < myPlayer.allButtonElements.Length; i++) 
        {
            ColorBlock colorsOthersButtons = myPlayer.allButtonElements[i].GetComponent<Button>().colors;

            colorsOthersButtons.normalColor = new Color(.6f, .6f, .6f);

            myPlayer.allButtonElements[i].button.colors = colorsOthersButtons;

            myPlayer.allButtonElements[i].spriteButton.color = new Color (1f, 1f, 1f, .5f);
        }

        spriteButton.color = new Color(1f, 1f, 1f, 1f);

        // on colorise l'élément qu'on vient de select
        switch (dataElement.element)
        {
            case ElementsPlayer.Fire:
                colorsButton.normalColor = new Color(1f, .4f, .4f);
                break;
            case ElementsPlayer.Water:
                colorsButton.normalColor = new Color(.4f, .6f, 1f);
                break;
            case ElementsPlayer.Thunder:
                colorsButton.normalColor = new Color(.7f, .4f, 1f);
                break;
            case ElementsPlayer.Earth:
                colorsButton.normalColor = new Color(1f, .7f, .4f);
                break;
            case ElementsPlayer.Light:
                colorsButton.normalColor = new Color(1f, 1f, .4f);
                break;
            case ElementsPlayer.Nothing:
                colorsButton.normalColor = new Color(.6f, .6f, .6f);
                break;
            default:
                break;
        }

        button.colors = colorsButton;

        // on change la couleur des dés
        for (int i = 0;i < sceneData.allDices.Length; i++)
        {
            switch (dataElement.element)
            {
                case ElementsPlayer.Fire:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.elementalDices[0];
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.elementalDices[0];
                    break;
                case ElementsPlayer.Water:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.elementalDices[1];
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.elementalDices[1];
                    break;
                case ElementsPlayer.Thunder:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.elementalDices[2];
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.elementalDices[2];
                    break;
                case ElementsPlayer.Earth:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.elementalDices[3];
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.elementalDices[3];
                    break;
                case ElementsPlayer.Light:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.elementalDices[4];
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.elementalDices[4];
                    break;
                case ElementsPlayer.Nothing:
                    sceneData.allDices[i].spriteAbility.sprite = sceneData.allDices[i].dataDice.sprite;
                    sceneData.allDices[i].spriteAbilityCooldown.sprite = sceneData.allDices[i].dataDice.sprite;
                    break;
                default:
                    break;
            }
        }

        // le joueur possède le nouveau élément
        myPlayer.currentElement = dataElement.element;

        }
}
