Shader "shader/camera/depth" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct vertex_output {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _CameraDepthNormalsTexture;

			vertex_output vert(
				float4 vertex : POSITION,
				float2 uv : TEXCOORD0
			) {
				vertex_output output;

				output.vertex = UnityObjectToClipPos(vertex);
				output.uv = uv;

				return output;
			}

			fixed4 frag(vertex_output input) : SV_TARGET {
				fixed4 color;
				float depth;
				float3 normal;

				color = tex2D(_MainTex, input.uv);

				DecodeDepthNormal(
					tex2D(_CameraDepthNormalsTexture, input.uv),
					depth,
					normal.xyz
				);

				color.rgb = depth;
				color.a = 1.00f;

				return color;
			}

			ENDCG
		}
	}
}
