using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(GraphicMaterial))]
	internal sealed class GraphicMaterialInspector : SkinPartsInspector
	{
		public override void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, GraphicMaterial.ObjectLength);
			SkinnerEditorUtility.CleanObject<Graphic>(property.objectReferenceValues, GraphicMaterial.GraphicIndex);
			SkinnerEditorUtility.CleanObject<Material>(property.objectReferenceValues, GraphicMaterial.MaterialIndex);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		public override void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, GraphicMaterial.ObjectLength);
			var graphicProperty = property.objectReferenceValues.GetArrayElementAtIndex(GraphicMaterial.GraphicIndex);
			var materialProperty = property.objectReferenceValues.GetArrayElementAtIndex(GraphicMaterial.MaterialIndex);
			SkinnerEditorGUILayout.ObjectField(SkinContent.Graphic, graphicProperty, typeof(Graphic));
			SkinnerEditorGUILayout.ObjectField(SkinContent.Material, materialProperty, typeof(Material));
		}

	}

}
