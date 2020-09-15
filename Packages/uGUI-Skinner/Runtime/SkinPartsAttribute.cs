using System;

namespace Pspkurara.UI.Skinner
{

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SkinPartsAttribute : Attribute
	{

		public SkinPartsAttribute(int id, Type logicType)
		{
			Id = id;
			LogicType = logicType;
		}

		public SkinPartsAttribute(SkinPartsType partsType, Type logicType) : this((int)partsType, logicType) { }

		public int Id;

		public Type LogicType;

	}

}
