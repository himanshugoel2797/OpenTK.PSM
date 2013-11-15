using System;

namespace Sce.PlayStation.Core.Graphics
{
	public partial class GL
	{
		private static BlendEquationMode blendEquation;
		
		public enum BlendEquationMode
		{
			FuncAdd = (byte)BlendFuncMode.Add,
			FuncReverseSubtract = (byte)BlendFuncMode.ReverseSubtract,
			FuncSubtract = (byte)BlendFuncMode.Subtract
		}

		public enum AlphaFunction
		{
			Always,
			Equal,
			Gequal,
			Greater,
			Lequal,
			Less,
			Never,
			Notequal
		}

		public enum EnableCap : uint
		{
			CullFace = EnableMode.CullFace,
			Lighting = 1000u,
			Fog = 1000u,
			DepthTest = EnableMode.DepthTest,
			StencilTest = EnableMode.StencilTest,
			Dither = EnableMode.Dither,
			Blend = EnableMode.Blend,
			ScissorTest = EnableMode.ScissorTest,
			Light0 = 1000u,
			Light1 = 1000u,
			Light2 = 1000u,
			Light3 = 1000u,
			Light4 = 1000u,
			Light5 = 1000u,
			Light6 = 1000u,
			Light7 = 1000u,
			Multisample = 1001u,
			PolygonOffsetFill = EnableMode.PolygonOffsetFill
		}
		
		public static void Enable (EnableCap enable)
		{
			EnableMode m;
			if(Enum.TryParse<EnableMode>(enable.ToString(), out m))	graphics.Enable(m);	//If PSM supports the requested feature, enable it
			else if(enable.HasFlag(EnableCap.Multisample))
			{
				GraphicsContext temp = graphics;		//Create a copy
				graphics = new GraphicsContext(temp.Screen.Width, temp.Screen.Height,PixelFormat.RgbaH,
				                               PixelFormat.Depth24Stencil8, MultiSampleMode.Msaa2x);
			}
		}
		
		public static void BlendColor (Single red, Single green, Single blue, Single alpha)
		{
			//Dummy implementation, PSM does not seem to support
		}
		
		public static void BlendEquation (BlendEquationMode blend)
		{
			blendEquation = blend;
		}
		
		public static void BlendFunc (BlendFuncFactor src, BlendFuncFactor dst)
		{
			graphics.SetBlendFunc ((BlendFuncMode)blendEquation, src, dst);	
		}
		
		public static void AlphaFunc (AlphaFunction alpha, ref Single @ref)
		{
#warning NOT IMPLEMENTED YET
		}

	}
}

