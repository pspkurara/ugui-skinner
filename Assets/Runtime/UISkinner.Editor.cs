#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine.EventSystems;
using Pspkurara.UI.Skinner;

namespace Pspkurara.UI
{	
	public partial class UISkinner : UIBehaviour
	{
		
		protected override void Reset()
		{
			base.Reset();
			m_Styles = new List<SkinStyle>() { new SkinStyle() };
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			foreach (SkinStyle cObject in m_Styles)
			{
				cObject.OnValidate();
			}
			SetSkins(styleIndex);
		}

	}

}

#endif