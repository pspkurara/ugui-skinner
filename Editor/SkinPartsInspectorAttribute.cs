using System;

namespace Pspkurara.UI.Skinner
{

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SkinPartsInspectorAttribute : Attribute
	{

		public SkinPartsInspectorAttribute(Type rootType)
		{
			RootType = rootType;
		}

		public Type RootType;

	}

}
