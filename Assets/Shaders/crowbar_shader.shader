// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "crowbarShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Albedo("Albedo", 2D) = "white" {}
		_Ramp("Ramp", 2D) = "white" {}
		_OpacityEmission("OpacityEmission", Float) = 0
		_EmissionColor("EmissionColor", 2D) = "white" {}
		_normal("normal", 2D) = "white" {}
		_metallic("metallic", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _normal;
		uniform float4 _normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _OpacityEmission;
		uniform sampler2D _Ramp;
		uniform float4 _Ramp_ST;
		uniform sampler2D _EmissionColor;
		uniform sampler2D _metallic;
		uniform float4 _metallic_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_normal = i.uv_texcoord * _normal_ST.xy + _normal_ST.zw;
			o.Normal = tex2D( _normal, uv_normal ).rgb;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			float2 uv_Ramp = i.uv_texcoord * _Ramp_ST.xy + _Ramp_ST.zw;
			float temp_output_85_0 = ( (0.3 + (_OpacityEmission - 0.0) * (1.0 - 0.3) / (1.0 - 0.0)) + ( 1.0 - tex2D( _Ramp, uv_Ramp ).r ) );
			float clampResult90 = clamp( (-4.0 + (temp_output_85_0 - 0.0) * (4.0 - -4.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
			float temp_output_91_0 = ( 1.0 - clampResult90 );
			float2 appendResult92 = (float2(temp_output_91_0 , 0.0));
			o.Emission = ( temp_output_91_0 * tex2D( _EmissionColor, appendResult92 ) ).rgb;
			float2 uv_metallic = i.uv_texcoord * _metallic_ST.xy + _metallic_ST.zw;
			o.Metallic = tex2D( _metallic, uv_metallic ).r;
			o.Alpha = 1;
			clip( temp_output_85_0 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
-1913;29;1906;1044;2042.695;432.6003;1.183649;True;True
Node;AmplifyShaderEditor.RangedFloatNode;88;-1064.33,405.3524;Float;False;Property;_OpacityEmission;OpacityEmission;3;0;Create;True;0;0;False;0;0;1.63;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;84;-1137.84,561.5486;Float;True;Property;_Ramp;Ramp;2;0;Create;True;0;0;False;0;None;270c45b8928fec7409ea28cde872f270;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;95;-827.542,591.2786;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;83;-841.5844,405.2242;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.3;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;85;-580.5909,416.6574;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;89;-1336.336,50.80296;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-4;False;4;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;90;-1138.283,-116.2709;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;91;-928.4665,-114.4421;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;92;-1125.157,57.11755;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;93;-917.2842,8.13844;Float;True;Property;_EmissionColor;EmissionColor;4;0;Create;True;0;0;False;0;c236aa2903563364eb743b239b6b9163;20e364874674a3f47bc6dee8315e780f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-347.9121,-619.5007;Float;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;38;-343.3863,-384.2931;Float;True;Property;_normal;normal;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;94;-547.5873,-51.60503;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;39;-219.127,-49.3825;Float;True;Property;_metallic;metallic;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;217.8804,-239.0356;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;crowbarShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;95;0;84;1
WireConnection;83;0;88;0
WireConnection;85;0;83;0
WireConnection;85;1;95;0
WireConnection;89;0;85;0
WireConnection;90;0;89;0
WireConnection;91;0;90;0
WireConnection;92;0;91;0
WireConnection;93;1;92;0
WireConnection;94;0;91;0
WireConnection;94;1;93;0
WireConnection;0;0;2;0
WireConnection;0;1;38;0
WireConnection;0;2;94;0
WireConnection;0;3;39;0
WireConnection;0;10;85;0
ASEEND*/
//CHKSM=AD1F131849D92CC6E2214ACED43E3001C55668F0