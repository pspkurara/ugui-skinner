using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// タイプによる処理とキャッシュを行う
	/// これを通すことでオブジェクト変換等を抑制させる
	/// </summary>
	public abstract class ISkinLogicBase
	{

		internal ISkinPartsProperty cachedSkinProperty { get; set; }

		/// <summary>
		/// 値をオブジェクトに反映させる
		/// </summary>
		/// <param name="property">プロパティ</param>
		public abstract void SetValues(SkinLogicProperty property);

	}

}