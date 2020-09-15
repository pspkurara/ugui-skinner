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
		/// <see cref="SerializedProperty.floatValue"/>のデータパック
		/// </summary>
		public SerializedProperty floatValues { get; private set; }

		/// <summary>
		/// <see cref="SerializedProperty.vector4Value"/>のデータパック
		/// </summary>
		public SerializedProperty vector4Values { get; private set; }

		/// <summary>
		/// <see cref="SerializedProperty.stringValue"/>のデータパック
		/// </summary>
		public SerializedProperty stringValues { get; private set; }

		/// <summary>
		/// 値をプロパティにマップする
		/// </summary>
		/// <param name="property">親となるオブジェクト</param>
		internal virtual void MapProperties(SerializedProperty property)
		{
			floatValues = property.FindPropertyRelative("m_FloatValues");
			vector4Values = property.FindPropertyRelative("m_Vector4Values");
			stringValues = property.FindPropertyRelative("m_StringValues");
		}

	}

}
