using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	public sealed class GraphicMaterialInspector : ISkinPartsInspector
	{
		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, GraphicMaterial.ObjectLength);
			SkinnerEditorUtility.CleanObject<Graphic>(property.objectReferenceValues, GraphicMaterial.GraphicIndex);
			SkinnerEditorUtility.CleanObject<Material>(property.objectReferenceValues, GraphicMaterial.MaterialIndex);
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, GraphicMaterial.ObjectLength);
			var graphicProperty = property.objectReferenceValues.GetArrayElementAtIndex(GraphicMaterial.GraphicIndex);
			var materialProperty = property.objectReferenceValues.GetArrayElementAtIndex(GraphicMaterial.MaterialIndex);
			graphicProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.Graphic, graphicProperty.objectReferenceValue, typeof(Graphic), true);
			materialProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.Material, materialProperty.objectReferenceValue, typeof(Material), false);
		}

	}

}
