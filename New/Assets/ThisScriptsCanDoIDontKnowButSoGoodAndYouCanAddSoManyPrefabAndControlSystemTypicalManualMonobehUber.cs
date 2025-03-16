using UnityEngine;

public class ThisScriptsCanDoIDontKnowButSoGoodAndYouCanAddSoManyPrefabAndControlSystemTypicalManualMonobehUber : MonoBehaviour
{
   public MonoBehaviour alphaScript;

   private void Awake()
   {
      if (alphaScript == null)
      {
         Debug.LogWarning("if AlphaScript is null You Have Big Problema");
      }
   }
}
