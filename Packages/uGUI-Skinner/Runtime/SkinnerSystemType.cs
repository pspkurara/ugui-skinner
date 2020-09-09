using UnityEngine;
using Type = System.Type;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 型チェック等を行う
	/// </summary>
	public static class SkinnerSystemType
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
		/// <see cref="SkinPartsPropertryWithoutObjectReference.colorValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsColorValue(Type type)
		{
			return IsColor(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.boolValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsBoolValue(Type type)
		{
			return IsBoolean(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.intValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsIntValue(Type type)
		{
			return IsInteger(type) || IsEnum(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.floatValues"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsFloatValue(Type type)
		{
			return IsFloat(type);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference.vector4Values"/>に入る型かチェックする
		/// </summary>
		/// <param name="type">型</param>
		public static bool IsVector4Value(Type type)
		{
			return IsVector2(type) || IsVector3(type) || IsVector4(type);
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

		#region 型チェック

		public static bool IsObject(Type type)
		{
			return type == typeof(Object) || type.IsSubclassOf(typeof(Object));
		}

		public static bool IsBoolean(Type type)
		{
			return type == typeof(bool);
		}

		public static bool IsInteger(Type type)
		{
			return type == typeof(int);
		}

		public static bool IsFloat(Type type)
		{
			return type == typeof(float);
		}

		public static bool IsColor(Type type)
		{
			return type == typeof(Color) || type == typeof(Color32);
		}

		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		public static bool IsVector2(Type type)
		{
			return type == typeof(Vector2);
		}

		public static bool IsVector3(Type type)
		{
			return type == typeof(Vector3);
		}

		public static bool IsVector4(Type type)
		{
			return type == typeof(Vector4);
		}

		public static bool IsChar(Type type)
		{
			return type == typeof(char);
		}

		public static bool IsString(Type type)
		{
			return type == typeof(string);
		}

		#endregion

	}

}
