PGDMP  )            	         }            GameMods_Avatar    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    41018    GameMods_Avatar    DATABASE     �   CREATE DATABASE "GameMods_Avatar" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
 !   DROP DATABASE "GameMods_Avatar";
                     postgres    false            �            1259    41019 	   File_Info    TABLE     �   CREATE TABLE public."File_Info" (
    "Id" uuid NOT NULL,
    name character varying(150) NOT NULL,
    path character varying(350) NOT NULL,
    date date NOT NULL
);
    DROP TABLE public."File_Info";
       public         heap r       postgres    false            �            1259    41026    Image_Avatar    TABLE     t   CREATE TABLE public."Image_Avatar" (
    "Id" uuid NOT NULL,
    "id_FileInfo" uuid NOT NULL,
    "id_User" uuid
);
 "   DROP TABLE public."Image_Avatar";
       public         heap r       postgres    false            �          0    41019 	   File_Info 
   TABLE DATA           =   COPY public."File_Info" ("Id", name, path, date) FROM stdin;
    public               postgres    false    217   �       �          0    41026    Image_Avatar 
   TABLE DATA           H   COPY public."Image_Avatar" ("Id", "id_FileInfo", "id_User") FROM stdin;
    public               postgres    false    218   {       %           2606    41025    File_Info File_Info_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."File_Info"
    ADD CONSTRAINT "File_Info_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."File_Info" DROP CONSTRAINT "File_Info_pkey";
       public                 postgres    false    217            '           2606    41030    Image_Avatar Image_Avatar_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."Image_Avatar"
    ADD CONSTRAINT "Image_Avatar_pkey" PRIMARY KEY ("Id");
 L   ALTER TABLE ONLY public."Image_Avatar" DROP CONSTRAINT "Image_Avatar_pkey";
       public                 postgres    false    218            (           2606    41036    Image_Avatar info    FK CONSTRAINT     �   ALTER TABLE ONLY public."Image_Avatar"
    ADD CONSTRAINT info FOREIGN KEY ("id_FileInfo") REFERENCES public."File_Info"("Id") NOT VALID;
 =   ALTER TABLE ONLY public."Image_Avatar" DROP CONSTRAINT info;
       public               postgres    false    217    218    4645            �   �   x���=
1F��)��d��f��6���M&?�����o"�h��"�����7_Lh}�"`���%�H��e!:�MSN�Zn��(�r+��G�?�|��5����>���2���������~���RBi�b���`�)��!8�9F�<bO�)�<�,|��
V�v��XG�6&{%jr#�f�S����M9c��iJ      �   �   x���u@1C��]����K0��q((@�
ߌ�@1X���,Ȟ�s�䗥��J���&��]��T"�3O�t�tz"V��,]b�pV�;1��J.�>
{<k�q�k~��ř��DG��3�\�c��ϒ�B�5;�u�y�ts#����}za���gAt�ߛ6�}7n��G�#�E�>!@b�>�U������?�yB;�H �x9~�z�Vl     