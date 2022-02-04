using UnityEngine;
using Type = System.Type;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 型チェック等を行う
	/// </summary>
	public static partial class SkinnerSystemType
	{

		#region UnityEngine.Object

		public static bool IsObject(Type type)
		{
			return type == typeof(Object) || type.IsSubclassOf(typeof(Object));
		}

		#endregion

		#region Float

		public static bool IsBoolean(Type type)
		{
			return type == typeof(bool);
		}

		public static bool IsInteger(Type type)
		{
			return type == typeof(int);
		}

		public static bool IsLayerMask(Type type)
		{
			return type == typeof(LayerMask);
		}

		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		public static bool IsFloat(Type type)
		{
			return type == typeof(float);
		}

		#endregion

		#region Vector4

		public static bool IsColor(Type type)
		{
			return type == typeof(Color) || type == typeof(Color32);
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

		public static bool IsRect(Type type)
		{
			return type == typeof(Rect);
		}

		#endregion

		#region String

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
