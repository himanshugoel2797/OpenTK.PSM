using System;
using System.Collections.Generic;

namespace Sce.PlayStation.Core.Graphics
{
	public partial class GL
	{ 
		private static List<Texture> texs = new List<Texture>();
		
		public static UInt32 AddTexture(Texture t)
		{
			texs.Add(t);
			graphics.SetTexture(0, t);
			return (UInt32)texs.IndexOf(t);
		}
		
		public enum TextureTarget
		{
			Texture1D,
			Texture2D,
			Texture3D,
			TextureCubeMap,
			TextureCubeMapPositiveX,
			TextureCubeMapNegativeX,
			TextureCubeMapPositiveY,
			TextureCubeMapNegativeY,
			TextureCubeMapPositiveZ,
			TextureCubeMapNegativeZ,
		}
		
		public static void BindTexture(TextureTarget target, UInt32 id)
		{
			graphics.SetTexture(0, texs[(int)id]);
		}
	}
}

