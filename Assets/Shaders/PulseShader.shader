Shader "Unlit/PulseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {           
     
                float2 fragCoord = i.vertex.xy;
                
                float2 uv = (2.0 * fragCoord - _ScreenParams.xy)/_ScreenParams.y;
  	            float3 color;
	            float d;	
	            
	            float deg = 3.14 * _Time[1];
	                
	            float x = cos(deg) * 0.1;
	            float y = sin(deg) * 0.1;	
	            
	            d = distance(float2(cos(x) * 0.2, sin(y) * 0.2), float2(.0, .0)) + 0.09;
	            
                float m = fmod(_Time[1] * 0.03, 2.0);	
                float p = (smoothstep(0.8, 1.2, m) - smoothstep(1.6, 2.0, m)) * 30.6 + 1.4;
                
	            d *= length(min(abs(uv) * (abs(sin(_Time[1] * 0.2))) * 0.3, 0.1) * p);    
	            float mul = ((sin(_Time[1] * 0.8)) * 5.0);
	            float col = smoothstep(0.0, 0.7, frac(d * mul)) - smoothstep(0.7, 1.0, frac(d * mul));
                
	            d /= length(max(abs(uv) - 0.0001, 0.225));    
	            mul = 10.0;    
	            col /= smoothstep(0.0, 0.7, frac(d * mul)) - smoothstep(0.7, 1.0, frac(d * mul));   
	            
	            
                
                float4 fragColor = float4(float3(0.0,col,0.0), 1.0);    
                return fragColor;                  
               
            }
            ENDCG
        }
    }
}
