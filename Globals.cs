using System;

namespace Sce.PlayStation.Core.Graphics
{
	public partial class GL
	{
		static GraphicsContext graphics;
		
		static Matrix4 WorldViewProj = Matrix4.Identity;
		
		/// <summary>
		/// (PSM Specific Convenience Function) Sets the graphics context.
		/// </summary>
		/// <param name='context'>
		/// Context.
		/// </param>
		public static void SetGraphicsContext(ref GraphicsContext context)
		{
			graphics = context;	
		}
	}
}

