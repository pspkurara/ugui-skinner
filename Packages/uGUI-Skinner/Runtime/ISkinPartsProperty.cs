using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// プロパティパックへのアクセサ
	/// <seealso cref="SkinPartsPropertry"/>
	/// <seealso cref="SkinLogicProperty"/>
	/// </summary>
	public interface ISkinPartsProperty
	{

		/// <summary>
		/// Unityオブジェクト
		/// </summary>
		List<Object> objectReferenceValues { get; }

		/// <summary>
		/// 少数値
		/// </summary>
		List<float> floatValues { get; }

		/// <summary>
		/// Vector4
		/// </summary>
		List<Vector4> vector4Values { get; }

		/// <summary>
		/// 文字列
		/// </summary>
		List<string> stringValues { get; }

	}

}
