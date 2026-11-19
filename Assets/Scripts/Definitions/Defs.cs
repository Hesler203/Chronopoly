using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// Definitions: handles definitions for global enums as well as namedarray functionality
/// </summary>

public enum ePos
{
    Go, Mediterranean, CommChest1, Baltic, IncomeTax, ReadingRR, Oriental, Chance1, Vermont, Connecticut, JustVisiting,
    StCharles, ElectricCompany, States, Virginia, PennRR, StJames, CommChest2, Tennessee, NewYork, FreeParking,
    Kentucky, Chance2, Indiana, Illinois, BnOoRR, Atlantic, Ventnor, WaterWorks, MarvinGardens, GoToJail,
    Pacific, NorthCarolina, CommChest3, Pennsylvania, ShortLineRR, Chance3, ParkPlace, LuxuryTax, Boardwalk, Terminator
}

public enum eEffectSounds {Click, NoGo, PassGo }

public class NamedArrayAttribute : PropertyAttribute
{
    public Type TargetEnum;
    public NamedArrayAttribute(Type TargetEnum)
    {
        this.TargetEnum = TargetEnum;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Properly configure height for expanded contents.
        return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Replace label with enum name if possible.
        try
        {
            var config = attribute as NamedArrayAttribute;
            var enum_names = System.Enum.GetNames(config.TargetEnum);
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            var enum_label = enum_names.GetValue(pos) as string;
            // Make names nicer to read (but won't exactly match enum definition).
            enum_label = ObjectNames.NicifyVariableName(enum_label.ToLower());
            label = new GUIContent(enum_label);
        }
        catch
        {
            // keep default label
        }
        EditorGUI.PropertyField(position, property, label, property.isExpanded);
    }
}
#endif

public class Defs : MonoBehaviour
{

}
