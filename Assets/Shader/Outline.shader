//Shader "Custom/Custom"
//{
//    Properties
//    {
//        _Color ("Color", Color) = (1,1,1,1)
//        _MainTex ("Albedo (RGB)", 2D) = "white" {}
//        _Glossiness ("Smoothness", Range(0,1)) = 0.5
//        _Metallic ("Metallic", Range(0,1)) = 0.0
//    }
//    SubShader
//    {
//        Tags { "RenderType"="Opaque" }
//        LOD 200
//
//        CGPROGRAM
//        // Physically based Standard lighting model, and enable shadows on all light types
//        #pragma surface surf Standard fullforwardshadows
//
//        // Use shader model 3.0 target, to get nicer looking lighting
//        #pragma target 3.0
//
//        sampler2D _MainTex;
//
//        struct Input
//        {
//            float2 uv_MainTex;
//        };
//
//        half _Glossiness;
//        half _Metallic;
//        fixed4 _Color;
//
//        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//        // #pragma instancing_options assumeuniformscaling
//        UNITY_INSTANCING_BUFFER_START(Props)
//            // put more per-instance properties here
//        UNITY_INSTANCING_BUFFER_END(Props)
//
//        void surf (Input IN, inout SurfaceOutputStandard o)
//        {
//            // Albedo comes from a texture tinted by color
//            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//            o.Albedo = c.rgb;
//            // Metallic and smoothness come from slider variables
//            o.Metallic = _Metallic;
//            o.Smoothness = _Glossiness;
//            o.Alpha = c.a;
//        }
//        ENDCG
//    }
//    FallBack "Diffuse"
//}

Shader "Custom/Outline"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0.0, 0.0, 0.0, 1)
        [Slider(0.1)] _OutlineWidth("Outline Width", Range(0.0, 10.0)) = 3

             
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            // 【1パス目】ダミー色で塗りつぶし
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    half4 vertex : POSITION;
                };

                struct v2f
                {
                    half4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    return fixed4(1.0, 0.0, 1.0, 0);
                }
                ENDCG
            }

            GrabPass {}

                    // 【2パス目】Grabテクスチャを使ってアウトライン＋通常描画
                    Pass
                    {
                        CGPROGRAM
                        #pragma vertex vert
                        #pragma fragment frag

                        #include "UnityCG.cginc"

                        #define SAMPLE_NUM 6
                        #define SAMPLE_INV 0.16666666
                        #define PI2 6.2831852
                        #define EPSILON 0.001
                        #define DUMMY_COLOR fixed3(1.0, 0.0, 1.0)

                        struct appdata
                        {
                            half4 vertex : POSITION;
                            half2 uv : TEXCOORD0;
                        };

                        struct v2f
                        {
                            half4 pos : SV_POSITION;
                            half2 uv : TEXCOORD0;
                            half4 grabPos : TEXCOORD1;
                        };

                        sampler2D _GrabTexture;
                        fixed4 _MainColor;
                        sampler2D _MainTex;
                        half4 _MainTex_ST;
                        fixed4 _OutlineColor;
                        half _OutlineWidth;

                        v2f vert(appdata v)
                        {
                            v2f o;
                            o.pos = UnityObjectToClipPos(v.vertex);
                            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                            o.grabPos = ComputeGrabScreenPos(o.pos);

                            return o;
                        }

                        fixed4 frag(v2f i) : SV_Target
                        {
                            half2 delta = (1 / _ScreenParams.xy) * _OutlineWidth;

                            int edge = 0;
                            [unroll]
                            for (int j = 0; j < SAMPLE_NUM && edge == 0; j++)
                            {
                                fixed4 tex = tex2D(_GrabTexture, i.grabPos.xy / i.grabPos.w + half2(sin(SAMPLE_INV * j * PI2) * delta.x, cos(SAMPLE_INV * j * PI2) * delta.y));
                                edge += distance(tex.rgb, DUMMY_COLOR) < EPSILON ? 0 : 1;
                            }

                            fixed4 col = lerp(tex2D(_MainTex, i.uv) * _MainColor, _OutlineColor, edge);
                            return col;
                        }
                        ENDCG
                    }
        }
}

