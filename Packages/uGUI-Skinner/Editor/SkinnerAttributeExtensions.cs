using System;
using Object = UnityEngine.Object;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="Attribute"/>関係の拡張関数群
	/// </summary>
	public static class SkinnerAttributeExtensions
	{

		/// <summary>
		/// <see cref="Attribute.IsDefined"/>を親クラスも含めて行う
		/// </summary>
		public static bool IsDefinedWithBaseType(this Type element, bool inherit = true)
		{
			return IsDefinedWithBaseType(element, typeof(Attribute), inherit);
		}

		/// <summary>
		/// <see cref="Attribute.IsDefined"/>を親クラスも含めて行う
		/// </summary>
		public static bool IsDefinedWithBaseType(this Type element, Type attributeType, bool inherit = true)
		{
			if (element == null) return false;
			if (element.IsDefined(attributeType, inherit))
			{
				return true;
			}
			return element.IsDefinedWithBaseType(attributeType.BaseType, inherit);
		}

		/// <summary>
		/// <see cref="Attribute.GetCustomAttribute"/>を親クラスも含めて行う
		/// </summary>
		public static Attribute[] GetCustomAttributesWithBaseType(this Type element, bool inherit = true)
		{
			return GetCustomAttributesWithBaseType(element, typeof(Attribute), inherit);
		}

		/// <summary>
		/// <see cref="Attribute.GetCustomAttribute"/>を親クラスも含めて行う
		/// </summary>
		public static Attribute[] GetCustomAttributesWithBaseType(this Type element, Type attributeType, bool inherit = true)
		{
			if (element == null) return Array.Empty<Attribute>();
			HashSet<Attribute> attributeList = new HashSet<Attribute>();
			if (attributeType == null) attributeType = typeof(Attribute);
			AccumulationAttributesWithBaseType(element, attributeList, attributeType, inherit);
			return attributeList.ToArray();
		}

		/// <summary>
		/// <see cref="GetCustomAttributesWithBaseType(Type, Type, bool)"/>の内部処理
		/// </summary>
		private static void AccumulationAttributesWithBaseType(Type element, HashSet<Attribute> attributeList, Type attributeType, bool inherit)
		{
			if (element == null) return;
			attributeList.UnionWith(element.GetCustomAttributes(attributeType, inherit).Cast<Attribute>());
			AccumulationAttributesWithBaseType(element.BaseType, attributeList, attributeType, inherit);
		}

	}

}
