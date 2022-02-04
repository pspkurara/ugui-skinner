using UnityEditor;
using System.Text;

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

		/// <summary>
		/// 文字列に変換する
		/// クラスの変数の内容を出力する
		/// </summary>
		/// <returns>自身の中身の文字列</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			SkinnerEditorUtility.AppendSkinPartsPropertyElementString(builder, objectReferenceValues, p => p.objectReferenceValue);
			SkinnerUtility.AppendIfStringNotEmpty(builder, base.ToString());
			return builder.ToString();
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

		/// <summary>
		/// 文字列に変換する
		/// クラスの変数の内容を出力する
		/// </summary>
		/// <returns>自身の中身の文字列</returns>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			SkinnerEditorUtility.AppendSkinPartsPropertyElementString(builder, floatValues, p => p.floatValue);
			SkinnerEditorUtility.AppendSkinPartsPropertyElementString(builder, vector4Values, p => p.vector4Value);
			SkinnerEditorUtility.AppendSkinPartsPropertyElementString(builder, stringValues, p => p.stringValue);
			return builder.ToString();
		}

	}

}
