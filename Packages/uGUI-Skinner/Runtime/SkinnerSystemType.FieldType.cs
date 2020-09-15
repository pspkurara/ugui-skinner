using Type = System.Type;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 型チェック等を行う
	/// </summary>
	public static partial class SkinnerSystemType
	{

		#region フィールド型チェック

		/// <summary>
		/// <see cref="SkinPartsPropertry.objectReferenceValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsObjectReferenceValue(Type type)
		{
			return IsObject(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.floatValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsFloatValue(Type type)
		{
			return IsFloat(type) || IsInteger(type) || IsEnum(type) || IsBoolean(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.vector4Values"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsVector4Value(Type type)
		{
			return IsVector2(type) || IsVector3(type) || IsVector4(type) || IsColor(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.stringValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsStringValue(Type type)
		{
			return IsChar(type) || IsString(type);
		}

		#endregion

	}

}
