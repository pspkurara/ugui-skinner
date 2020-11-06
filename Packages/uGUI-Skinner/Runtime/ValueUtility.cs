using UnityEngine;
using Enum = System.Enum;
using Convert = System.Convert;

namespace Pspkurara.UI.Skinner
{

	public static class ValueUtility
	{

		#region 変換

		public static Vector4 ToVector(this Color color)
		{
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		public static Vector4 ToVector(this Color32 color)
		{
			return color.ToVector();
		}

		public static Vector4 ToVector(this Rect rect)
		{
			return new Vector4(rect.x, rect.y, rect.width, rect.height);
		}

		public static float ToFloat<T>(this T self) where T : Enum
		{
			return Convert.ToInt32(self);
		}

		public static float ToFloat(this bool self)
		{
			return self ? 1 : 0;
		}

		public static float ToFloat(this LayerMask self)
		{
			return self.value;
		}

		#endregion

		#region 逆変換

		public static Color ToColor(this Vector4 self)
		{
			return new Color(self.x, self.y, self.z, self.w);
		}

		public static Rect ToRect(this Vector4 self)
		{
			return new Rect(self.x, self.y, self.z, self.w);
		}

		public static int ToInt(this float self)
		{
			return Mathf.RoundToInt(self);
		}

		public static T ToEnum<T>(this float self) where T : Enum
		{
			return (T)Enum.ToObject(typeof(T), self.ToInt());
		}

		public static bool ToBool(this float self)
		{
			return self.ToInt() > 0;
		}

		public static LayerMask ToLayerMask(this float self)
		{
			return new LayerMask() { value = self.ToInt() };
		}

		#endregion

	}

}
