using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// エディタ専用のプロパティパック
	/// </summary>
	/// <seealso cref="SkinPartsPropertry" />
	public sealed class EditorSkinPartsPropertry : EditorSkinPartsPropertryWithoutObjectReference
	{

		/// <summary>
		/// <see cref="SerializedProperty.objectReferenceValue"/>のデータパック
		/// </summary>
		public SerializedProperty objectReferenceValues { get; private set; }

		/// <summary>
		/// 値をプロパティにマップする
		/// </summary>
		/// <param name="property">親となるオブジェクト</param>
		internal override void MapProperties(SerializedProperty property)
		{
			objectReferenceValues = property.FindPropertyRelative("m_ObjectReferenceValues");
			base.MapProperties(property);
		}

	}

	/// <summary>
	/// エディタ専用のプロパティパック
	/// オブジェクト参照は除く
	/// </summary>
	/// <seealso cref="SkinPartsPropertryWithoutObjectReference" />
	public class EditorSkinPartsPropertryWithoutObjectReference
	{

		/// <summary>
		/// <see cref="SerializedProperty.boolValue"/>のデータパック
		/// </summary>
		public SerializedProperty boolValues { get; private set; }

		/// <summary>
		/// <see cref="SerializedProperty.colorValue"/>のデータパック
		/// </summary>
		public SerializedProperty colorValues { get; private set; }

		/// <summary>
		/// <see cref="SerializedProperty.floatValue"/>のデータパック
		/// </summary>
		public SerializedProperty floatValues { get; private set; }

		/// <summary>
		/// <see cref="SerializedProperty.intValue"/>のデータパック
		/// </summary>
		public SerializedProperty intValues { get; private set; }

		/// <summary>
		/// 値をプロパティにマップする
		/// </summary>
		/// <param name="property">親となるオブジェクト</param>
		internal virtual void MapProperties(SerializedProperty property)
		{
			boolValues = property.FindPropertyRelative("m_BoolValues");
			colorValues = property.FindPropertyRelative("m_ColorValues");
			floatValues = property.FindPropertyRelative("m_FloatValues");
			intValues = property.FindPropertyRelative("m_IntValues");
		}

	}

}
