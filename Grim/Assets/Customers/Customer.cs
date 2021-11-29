using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] Button serveButton;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    [SerializeField] List<Ingredient> recipe;  // option to just list ingredients for drink
    
    int servedCounter = 0;
    bool servable = true;

    List<string> Drinks = new List<string>()
    {
        "GRELYCMANTEA",
        "BOBMATMIL",
        "STRMIL",
        "RANDRK"
    };

    public string Name {
        get { return name; }
    }

    public List<Ingredient> Recipe {
        get { return recipe; }
    }

    public string RecipeString() {
        string recipeString = "";
        foreach (Ingredient ingredient in recipe) {
            recipeString += ingredient.Code;
        }
        return recipeString;
    }

    public void Start() {
        serveButton.onClick.AddListener(ServeDrink);
        Debug.Log("Customer's perfect drink string: " + RecipeString());
    }

    public void ServeDrink() {
        if (!servable) {
            Debug.Log("Customer is no longer servable!");
        }

        Debug.Log("Serving drink string " + Drinks[servedCounter]);
        bool perfectDrink = CompareDrink(Drinks[servedCounter]);
        servedCounter++;

        if (servedCounter >= 3 || perfectDrink) {
            servable = false;
        }

        if (perfectDrink) {
            Debug.Log("Customer has been served their perfect drink!");
        }
        if (servedCounter >= 3) {
            Debug.Log("Customer has been served thrice. It's like twice but three!");
        }
    }

    bool CompareDrink(string servedDrink)
    {
        int codeSize = 3;
        bool perfectDrink = true;
        List<string> servedDrinkCodes = new List<string>();
        
        // convert string of codes into list of codes
        for (int k = 0; k < servedDrink.Length; k += codeSize) {
            string chunk = servedDrink.Substring(k, codeSize);
            servedDrinkCodes.Add(chunk);
        }

        // check for ingredients missing from served drink
        foreach (Ingredient ingredient in recipe) {
            string currCode = ingredient.Code;
            bool found = false;

            for (int k = 0; k < servedDrinkCodes.Count; k++) {
                if (currCode == servedDrinkCodes[k]) {
                    servedDrinkCodes.RemoveAt(k);
                    found = true;
                    break;
                }
            }

            if (!found) {
                perfectDrink = false;
                Debug.Log(ingredient.Response);
            }
        }
        return perfectDrink;
    }
}
