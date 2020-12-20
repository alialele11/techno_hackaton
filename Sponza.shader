Shader "Unit/SponzaShader"{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
	[Enum(CompareFunction)] _StencilComp("Stencil Comp", Int) = 3
    }
    SubShader{
        Tags{ "RenderType" = "Opaque"}
        LOD 100
        Pass{
            Stencil{
                Ref 1
                Comp [_StencilComp]
            }
            CGPROGRAM
            #pragma vertex vert
#pragma fragment frag
# include "UnityCG.cginc"
struct appdata{
    float4 vertex : POSITION;
        float4 normal : NORMAL;
        float2 uv : TEXCOORD0;
}
struct v2f
{
    float2 uv : TEXCOORD0;
float3 normal = TEXCOORD1;
}
        }
    }
}