using System;
using System.Collections.Generic;

using vector3d = Sce.PlayStation.Core.Vector3;
using vector2d = Sce.PlayStation.Core.Vector2;
using vector4d = Sce.PlayStation.Core.Vector4;
using System.Reflection;

namespace Sce.PlayStation.Core.Graphics
{
	public partial class GL
	{
		private static VertexBuffer immModeVBuffer;
		private static List<vector3d> vertices;
		private static List<vector4d> colors;
		private static List<vector3d> normals;
		private static List<vector3d> texcoords;
		private static BeginMode immMode;
		private static ShaderProgram ImmShader;
		
		public enum BeginMode{
			Lines = DrawMode.Lines, LineStrip = DrawMode.LineStrip, Points = DrawMode.Points,
			TriangleFan = DrawMode.TriangleFan, Triangles = DrawMode.Triangles, TriangleStrip = DrawMode.TriangleStrip 	
		}
		
		public static void Begin(BeginMode mode)
		{
			immMode = mode;
			if(vertices == null)vertices = new List<vector3d>();
			if(colors == null)colors = new List<vector4d>();
			if(normals == null)normals = new List<vector3d>();
			if(texcoords == null)texcoords = new List<vector3d>();
			
			vertices.Clear();
			colors.Clear();
			normals.Clear();
			texcoords.Clear();
		}
		
		public static void Vector3(vector3d vec)
		{
			vertices.Add(vec);
			if(colors.Count > 0)colors.Add(colors[colors.Count -1]);
			else colors.Add(new vector4d(1,1,1,1));
			normals.Add(vector3d.Zero);
			texcoords.Add(new vector3d(vec.X,vec.Y, 0));
		}
		public static void Normal3(vector3d norm)
		{
			normals[normals.Count - 1] = norm;	
		}
		public static void TexCoord2(vector2d tex)
		{
			texcoords[texcoords.Count - 1] = tex.Xy0;	
		}
		public static void Color3(vector3d col)
		{
			colors[colors.Count - 1] = col.Xyz1;
		}
		public static void Color4(vector4d col)
		{
			colors[colors.Count - 1] = col.Xyzw;
		}
		
		public static void End()
		{
			immModeVBuffer = new 
				VertexBuffer(vertices.Count, 
			                                  VertexFormat.Float3 /*Vertices*/,
			                                  VertexFormat.Float3 /*Normals*/,
			                                  VertexFormat.Float3 /*TexCoords*/,
			                                  VertexFormat.Float4 /*Colors*/
			                                  );
			immModeVBuffer.SetVertices(0, vertices.ToArray());
			immModeVBuffer.SetVertices(1, normals.ToArray());
			immModeVBuffer.SetVertices(2, texcoords.ToArray());
			immModeVBuffer.SetVertices(3, colors.ToArray());
			
			graphics.SetVertexBuffer(0, immModeVBuffer);
			
			if(ImmShader == null)ConfigureImmShader();
			
			graphics.SetShaderProgram(ImmShader);
			InitImmShader();
			
			graphics.DrawArrays((DrawMode)immMode, 0, vertices.Count);
			graphics.SwapBuffers();
		}
		
		
		private static void ConfigureImmShader()
		{
			ImmShader = new ShaderProgram(Util.Utility.ReadEmbeddedFile("Sce.PlayStation.Core.Graphics.shader.ImmMode.cgx"));
			
			ImmShader.SetAttributeBinding(0, "a_Position");
			ImmShader.SetAttributeBinding(1, "a_Normal");
			ImmShader.SetAttributeBinding(2, "a_TexCoord");
			ImmShader.SetAttributeBinding(3, "a_Color");
			
			ImmShader.SetUniformBinding(0, "WorldViewProj");
		}
		private static void InitImmShader()
		{
				ImmShader.SetUniformValue(0, ref WorldViewProj);
		}
		
	}
}

