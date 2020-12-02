// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Lighting/02BasicLightingPerVertex"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}

        [Header(Ambient)]
        _Ambient("Intensity", Range(0., 1.)) = 0.1
        _AmbColor("Color", color) = (1., 1., 1., 1.)

        [Header(Flashlight)]
        _CharacterPosition("Char Pos", vector) = (0,0,0,0)
        _CircleRadius("Flashlight size", Range(0,20)) = 3
        _RingSize("Ring size", Range(0,5)) = 1
        _ColorTin("outside color", Color) = (0,0,0,0)
    }

        SubShader
        {
            Pass
            {
                Tags { "RenderType" = "Opaque" "Queue" = "Geometry" "LightMode" = "ForwardBase" }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

            // Change "shader_feature" with "pragma_compile" if you want set this keyword from c# code
            #pragma shader_feature __ _SPEC_ON

            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 light : COLOR0;
            };

            fixed4 _LightColor0;

            float4 _CharacterPosition;
            float _CircleRadius;
            float _RingSize;
            float4 _ColorTin;

            //Ambient
            fixed _Ambient;
            fixed4 _AmbColor;

            v2f vert(appdata_base v)
            {
                v2f o;
                // World position
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

                // Clip position
                o.pos = mul(UNITY_MATRIX_VP, worldPos);

                // Compute ambient lighting
                fixed4 amb = _Ambient * _AmbColor;

                o.light = amb;

                o.uv = v.texcoord;

                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                c.rgb *= i.light;

                return c;
            }

            ENDCG
        }
        }
}