using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public static float ToFloat(this bool self)
		{
			return self ? 1 : 0;
		}

		#endregion

		#region 逆変換

		public static Color ToColor(this Vector4 self)
		{
			return new Color(self.x, self.y, self.z, self.w);
		}

		public static int ToInt(this float self)
		{
			return Mathf.RoundToInt(self);
		}

		public static bool ToBool(this float self)
		{
			return self.ToInt() > 0;
		}

		#endregion

	}

}