using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// タイプによる処理とキャッシュを行う
	/// これを通すことでオブジェクト変換等を抑制させる
	/// </summary>
	public interface ISkinLogic
	{

		/// <summary>
		/// 値をオブジェクトに反映させる
		/// </summary>
		/// <param name="property">プロパティ</param>
		void SetValues(SkinPartsPropertry property);

	}

}