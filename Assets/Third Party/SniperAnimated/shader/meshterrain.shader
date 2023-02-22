// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "meshterrain"
{
	Properties
	{
		_basecolor("basecolor", 2D) = "white" {}
		_baseNormal("baseNormal", 2D) = "bump" {}
		_basespecular("basespecular", 2D) = "white" {}
		_splatmap("splatmap", 2D) = "white" {}
		_greencolor("greencolor", 2D) = "white" {}
		_greenNormal("greenNormal", 2D) = "bump" {}
		_redcolor("redcolor", 2D) = "white" {}
		_redNormal("redNormal", 2D) = "bump" {}
		_blueColor("blueColor", 2D) = "white" {}
		_blueNormal("blueNormal", 2D) = "bump" {}
		_Float0("Float 0", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _redNormal;
		uniform float4 _redNormal_ST;
		uniform sampler2D _greenNormal;
		uniform float4 _greenNormal_ST;
		uniform sampler2D _splatmap;
		uniform float4 _splatmap_ST;
		uniform sampler2D _blueNormal;
		uniform float4 _blueNormal_ST;
		uniform sampler2D _baseNormal;
		uniform float4 _baseNormal_ST;
		uniform sampler2D _basecolor;
		uniform float4 _basecolor_ST;
		uniform sampler2D _redcolor;
		uniform float4 _redcolor_ST;
		uniform sampler2D _greencolor;
		uniform float4 _greencolor_ST;
		uniform sampler2D _blueColor;
		uniform float4 _blueColor_ST;
		uniform float _Float0;
		uniform sampler2D _basespecular;
		uniform float4 _basespecular_ST;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_redNormal = i.uv_texcoord * _redNormal_ST.xy + _redNormal_ST.zw;
			float2 uv_greenNormal = i.uv_texcoord * _greenNormal_ST.xy + _greenNormal_ST.zw;
			float2 uv_splatmap = i.uv_texcoord * _splatmap_ST.xy + _splatmap_ST.zw;
			float4 tex2DNode4 = tex2D( _splatmap, uv_splatmap );
			float3 lerpResult10 = lerp( UnpackNormal( tex2D( _redNormal, uv_redNormal ) ) , UnpackNormal( tex2D( _greenNormal, uv_greenNormal ) ) , tex2DNode4.g);
			float2 uv_blueNormal = i.uv_texcoord * _blueNormal_ST.xy + _blueNormal_ST.zw;
			float3 lerpResult14 = lerp( lerpResult10 , UnpackNormal( tex2D( _blueNormal, uv_blueNormal ) ) , tex2DNode4.b);
			float2 uv_baseNormal = i.uv_texcoord * _baseNormal_ST.xy + _baseNormal_ST.zw;
			o.Normal = BlendNormals( lerpResult14 , UnpackNormal( tex2D( _baseNormal, uv_baseNormal ) ) );
			float2 uv_basecolor = i.uv_texcoord * _basecolor_ST.xy + _basecolor_ST.zw;
			float2 uv_redcolor = i.uv_texcoord * _redcolor_ST.xy + _redcolor_ST.zw;
			float2 uv_greencolor = i.uv_texcoord * _greencolor_ST.xy + _greencolor_ST.zw;
			float4 lerpResult5 = lerp( tex2D( _redcolor, uv_redcolor ) , tex2D( _greencolor, uv_greencolor ) , tex2DNode4.g);
			float2 uv_blueColor = i.uv_texcoord * _blueColor_ST.xy + _blueColor_ST.zw;
			float4 lerpResult13 = lerp( lerpResult5 , tex2D( _blueColor, uv_blueColor ) , tex2DNode4.b);
			float4 lerpResult15 = lerp( tex2D( _basecolor, uv_basecolor ) , lerpResult13 , _Float0);
			o.Albedo = lerpResult15.rgb;
			float2 uv_basespecular = i.uv_texcoord * _basespecular_ST.xy + _basespecular_ST.zw;
			float4 tex2DNode3 = tex2D( _basespecular, uv_basespecular );
			o.Specular = tex2DNode3.rgb;
			o.Smoothness = tex2DNode3.a;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13801
1609;417;1560;892;1409.401;434.3209;1.6;True;True
Node;AmplifyShaderEditor.SamplerNode;8;-469.5449,550.838;Float;True;Property;_redcolor;redcolor;6;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;6;-824.5449,458.838;Float;True;Property;_greencolor;greencolor;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-816.5449,664.838;Float;True;Property;_greenNormal;greenNormal;5;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-1018.376,188.7979;Float;True;Property;_splatmap;splatmap;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;9;-472.5449,760.838;Float;True;Property;_redNormal;redNormal;7;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;5;-119.5449,424.838;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;11;-32.54504,758.838;Float;True;Property;_blueColor;blueColor;8;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;12;-32.54504,984.838;Float;True;Property;_blueNormal;blueNormal;9;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;10;-118.5449,571.838;Float;False;3;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SamplerNode;2;-628.516,-242.4464;Float;True;Property;_baseNormal;baseNormal;1;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-844.4687,-392.6038;Float;True;Property;_basecolor;basecolor;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;171.3992,-181.5209;Float;False;Property;_Float0;Float 0;10;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;13;350.705,511.4501;Float;False;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;14;360.905,715.5502;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0;False;2;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.LerpOp;15;-143.4948,-298.3503;Float;False;3;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.BlendNormalsNode;17;615.3048,724.6503;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SamplerNode;3;-631.5644,-16.37726;Float;True;Property;_basespecular;basespecular;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;StandardSpecular;meshterrain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;8;0
WireConnection;5;1;6;0
WireConnection;5;2;4;2
WireConnection;10;0;9;0
WireConnection;10;1;7;0
WireConnection;10;2;4;2
WireConnection;13;0;5;0
WireConnection;13;1;11;0
WireConnection;13;2;4;3
WireConnection;14;0;10;0
WireConnection;14;1;12;0
WireConnection;14;2;4;3
WireConnection;15;0;1;0
WireConnection;15;1;13;0
WireConnection;15;2;18;0
WireConnection;17;0;14;0
WireConnection;17;1;2;0
WireConnection;0;0;15;0
WireConnection;0;1;17;0
WireConnection;0;3;3;0
WireConnection;0;4;3;4
ASEEND*/
//CHKSM=7D1DC82FBE86DC16F1FDE0DA0F9B8135B8DF263C