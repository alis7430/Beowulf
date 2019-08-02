// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-9606-OUT,spec-5498-OUT,gloss-5676-OUT,normal-3550-OUT,difocc-7467-B;n:type:ShaderForge.SFN_Multiply,id:6343,x:31448,y:32411,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31288,y:32517,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.75,c2:0.75,c3:0.75,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31288,y:32338,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2fdde73f26a14594cbfb1a5f29b39db1,ntxv:0,isnm:False|UVIN-4622-OUT;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32227,y:33321,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44819a5f03a6e944f859e94da2282e78,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ObjectScale,id:4972,x:30519,y:32337,varname:node_4972,prsc:2,rcp:True;n:type:ShaderForge.SFN_ValueProperty,id:6115,x:30584,y:32517,ptovrint:False,ptlb:TileValue,ptin:_TileValue,varname:node_6115,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:226,x:30747,y:32428,varname:node_226,prsc:2|A-4972-XYZ,B-6115-OUT;n:type:ShaderForge.SFN_TexCoord,id:5997,x:30747,y:32223,varname:node_5997,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:642,x:30953,y:32348,varname:node_642,prsc:2|A-5997-UVOUT,B-226-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4622,x:31120,y:32338,varname:node_4622,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-642-OUT;n:type:ShaderForge.SFN_Tex2d,id:7467,x:30703,y:32840,ptovrint:False,ptlb:ECA,ptin:_ECA,varname:node_7467,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:88a6b94c693638245a06848a1114d651,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:4772,x:30844,y:32764,ptovrint:False,ptlb:EdgePower,ptin:_EdgePower,varname:node_4772,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:9408,x:31104,y:32729,varname:node_9408,prsc:2|VAL-7467-R,EXP-4772-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1613,x:30801,y:33029,ptovrint:False,ptlb:AOPower,ptin:_AOPower,varname:_EdgePower_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:8919,x:31060,y:32994,varname:node_8919,prsc:2|VAL-7467-B,EXP-1613-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:9348,x:31277,y:32700,varname:node_9348,prsc:2,min:0,max:1|IN-9408-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:2292,x:31253,y:32984,varname:node_2292,prsc:2,min:0,max:1|IN-8919-OUT;n:type:ShaderForge.SFN_Lerp,id:9606,x:31877,y:32657,varname:node_9606,prsc:2|A-5010-OUT,B-7861-OUT,T-9348-OUT;n:type:ShaderForge.SFN_Color,id:1344,x:31459,y:32589,ptovrint:False,ptlb:EdgeColor,ptin:_EdgeColor,varname:node_1344,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6838235,c2:0.6180009,c3:0.5631488,c4:1;n:type:ShaderForge.SFN_Blend,id:7861,x:31650,y:32477,varname:node_7861,prsc:2,blmd:10,clmp:True|SRC-6343-OUT,DST-1344-RGB;n:type:ShaderForge.SFN_Lerp,id:2653,x:31611,y:32979,varname:node_2653,prsc:2|A-2533-RGB,B-7495-RGB,T-2292-OUT;n:type:ShaderForge.SFN_Color,id:2533,x:31414,y:32837,ptovrint:False,ptlb:AOColor1,ptin:_AOColor1,varname:node_2533,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1397059,c2:0.09656142,c3:0.05341696,c4:1;n:type:ShaderForge.SFN_Color,id:7495,x:31393,y:33009,ptovrint:False,ptlb:AOColor2,ptin:_AOColor2,varname:_AOColor2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7720588,c2:0.7720588,c3:0.7720588,c4:1;n:type:ShaderForge.SFN_Multiply,id:5010,x:31700,y:32728,varname:node_5010,prsc:2|A-6343-OUT,B-2653-OUT;n:type:ShaderForge.SFN_Multiply,id:4763,x:31654,y:33150,varname:node_4763,prsc:2|A-7467-G,B-7467-B;n:type:ShaderForge.SFN_OneMinus,id:5357,x:31825,y:33107,varname:node_5357,prsc:2|IN-4763-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9581,x:31825,y:33267,ptovrint:False,ptlb:Gloss Min,ptin:_GlossMin,varname:node_9581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.4;n:type:ShaderForge.SFN_ValueProperty,id:1610,x:31825,y:33349,ptovrint:False,ptlb:Gloss Max,ptin:_GlossMax,varname:_GlossMin_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Clamp,id:5676,x:32063,y:33107,varname:node_5676,prsc:2|IN-5357-OUT,MIN-9581-OUT,MAX-1610-OUT;n:type:ShaderForge.SFN_Vector1,id:5498,x:32246,y:32790,varname:node_5498,prsc:2,v1:0;n:type:ShaderForge.SFN_Color,id:9679,x:32227,y:33528,ptovrint:False,ptlb:NormalMapIntensity,ptin:_NormalMapIntensity,varname:node_9679,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6772,x:32425,y:33431,varname:node_6772,prsc:2|A-5964-RGB,B-9679-RGB;n:type:ShaderForge.SFN_Tex2d,id:9950,x:32209,y:33760,ptovrint:False,ptlb:DetailNormalMAp,ptin:_DetailNormalMAp,varname:node_9950,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c08ccc0b4e392534fa96ff74a128c8ad,ntxv:3,isnm:True|UVIN-30-OUT;n:type:ShaderForge.SFN_TexCoord,id:2394,x:31822,y:33607,varname:node_2394,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:30,x:32028,y:33732,varname:node_30,prsc:2|A-2394-UVOUT,B-9055-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9055,x:31821,y:33820,ptovrint:False,ptlb:DetailNormalTile,ptin:_DetailNormalTile,varname:node_9055,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_NormalBlend,id:3550,x:32636,y:33578,varname:node_3550,prsc:2|BSE-6772-OUT,DTL-9950-RGB;proporder:5964-6665-7736-6115-7467-4772-1344-1613-2533-7495-9581-1610-9679-9950-9055;pass:END;sub:END;*/

Shader "Realistic Forest Pack/Rock" {
    Properties {
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (0.75,0.75,0.75,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _TileValue ("TileValue", Float ) = 1
        _ECA ("ECA", 2D) = "white" {}
        _EdgePower ("EdgePower", Float ) = 1
        _EdgeColor ("EdgeColor", Color) = (0.6838235,0.6180009,0.5631488,1)
        _AOPower ("AOPower", Float ) = 1
        _AOColor1 ("AOColor1", Color) = (0.1397059,0.09656142,0.05341696,1)
        _AOColor2 ("AOColor2", Color) = (0.7720588,0.7720588,0.7720588,1)
        _GlossMin ("Gloss Min", Float ) = 0.4
        _GlossMax ("Gloss Max", Float ) = 0.5
        _NormalMapIntensity ("NormalMapIntensity", Color) = (1,1,1,1)
        _DetailNormalMAp ("DetailNormalMAp", 2D) = "bump" {}
        _DetailNormalTile ("DetailNormalTile", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _TileValue;
            uniform sampler2D _ECA; uniform float4 _ECA_ST;
            uniform float _EdgePower;
            uniform float _AOPower;
            uniform float4 _EdgeColor;
            uniform float4 _AOColor1;
            uniform float4 _AOColor2;
            uniform float _GlossMin;
            uniform float _GlossMax;
            uniform float4 _NormalMapIntensity;
            uniform sampler2D _DetailNormalMAp; uniform float4 _DetailNormalMAp_ST;
            uniform float _DetailNormalTile;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float2 node_30 = (i.uv0*_DetailNormalTile);
                float3 _DetailNormalMAp_var = UnpackNormal(tex2D(_DetailNormalMAp,TRANSFORM_TEX(node_30, _DetailNormalMAp)));
                float3 node_3550_nrm_base = (_BumpMap_var.rgb*_NormalMapIntensity.rgb) + float3(0,0,1);
                float3 node_3550_nrm_detail = _DetailNormalMAp_var.rgb * float3(-1,-1,1);
                float3 node_3550_nrm_combined = node_3550_nrm_base*dot(node_3550_nrm_base, node_3550_nrm_detail)/node_3550_nrm_base.z - node_3550_nrm_detail;
                float3 node_3550 = node_3550_nrm_combined;
                float3 normalLocal = node_3550;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _ECA_var = tex2D(_ECA,TRANSFORM_TEX(i.uv0, _ECA));
                float gloss = clamp((1.0 - (_ECA_var.g*_ECA_var.b)),_GlossMin,_GlossMax);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 node_4622 = (float3(i.uv0,0.0)*(recipObjScale*_TileValue)).rgb;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4622, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float3 diffuseColor = lerp((node_6343*lerp(_AOColor1.rgb,_AOColor2.rgb,clamp(pow(_ECA_var.b,_AOPower),0,1))),saturate(( _EdgeColor.rgb > 0.5 ? (1.0-(1.0-2.0*(_EdgeColor.rgb-0.5))*(1.0-node_6343)) : (2.0*_EdgeColor.rgb*node_6343) )),clamp(pow(_ECA_var.r,_EdgePower),0,1)); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, 0.0, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= _ECA_var.b; // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _TileValue;
            uniform sampler2D _ECA; uniform float4 _ECA_ST;
            uniform float _EdgePower;
            uniform float _AOPower;
            uniform float4 _EdgeColor;
            uniform float4 _AOColor1;
            uniform float4 _AOColor2;
            uniform float _GlossMin;
            uniform float _GlossMax;
            uniform float4 _NormalMapIntensity;
            uniform sampler2D _DetailNormalMAp; uniform float4 _DetailNormalMAp_ST;
            uniform float _DetailNormalTile;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float2 node_30 = (i.uv0*_DetailNormalTile);
                float3 _DetailNormalMAp_var = UnpackNormal(tex2D(_DetailNormalMAp,TRANSFORM_TEX(node_30, _DetailNormalMAp)));
                float3 node_3550_nrm_base = (_BumpMap_var.rgb*_NormalMapIntensity.rgb) + float3(0,0,1);
                float3 node_3550_nrm_detail = _DetailNormalMAp_var.rgb * float3(-1,-1,1);
                float3 node_3550_nrm_combined = node_3550_nrm_base*dot(node_3550_nrm_base, node_3550_nrm_detail)/node_3550_nrm_base.z - node_3550_nrm_detail;
                float3 node_3550 = node_3550_nrm_combined;
                float3 normalLocal = node_3550;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _ECA_var = tex2D(_ECA,TRANSFORM_TEX(i.uv0, _ECA));
                float gloss = clamp((1.0 - (_ECA_var.g*_ECA_var.b)),_GlossMin,_GlossMax);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 node_4622 = (float3(i.uv0,0.0)*(recipObjScale*_TileValue)).rgb;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4622, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float3 diffuseColor = lerp((node_6343*lerp(_AOColor1.rgb,_AOColor2.rgb,clamp(pow(_ECA_var.b,_AOPower),0,1))),saturate(( _EdgeColor.rgb > 0.5 ? (1.0-(1.0-2.0*(_EdgeColor.rgb-0.5))*(1.0-node_6343)) : (2.0*_EdgeColor.rgb*node_6343) )),clamp(pow(_ECA_var.r,_EdgePower),0,1)); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, 0.0, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _TileValue;
            uniform sampler2D _ECA; uniform float4 _ECA_ST;
            uniform float _EdgePower;
            uniform float _AOPower;
            uniform float4 _EdgeColor;
            uniform float4 _AOColor1;
            uniform float4 _AOColor2;
            uniform float _GlossMin;
            uniform float _GlossMax;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float3 node_4622 = (float3(i.uv0,0.0)*(recipObjScale*_TileValue)).rgb;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4622, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float4 _ECA_var = tex2D(_ECA,TRANSFORM_TEX(i.uv0, _ECA));
                float3 diffColor = lerp((node_6343*lerp(_AOColor1.rgb,_AOColor2.rgb,clamp(pow(_ECA_var.b,_AOPower),0,1))),saturate(( _EdgeColor.rgb > 0.5 ? (1.0-(1.0-2.0*(_EdgeColor.rgb-0.5))*(1.0-node_6343)) : (2.0*_EdgeColor.rgb*node_6343) )),clamp(pow(_ECA_var.r,_EdgePower),0,1));
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float roughness = 1.0 - clamp((1.0 - (_ECA_var.g*_ECA_var.b)),_GlossMin,_GlossMax);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
