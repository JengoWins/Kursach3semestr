PGDMP  4            	         }            GameMods_Image    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    41057    GameMods_Image    DATABASE     �   CREATE DATABASE "GameMods_Image" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
     DROP DATABASE "GameMods_Image";
                     postgres    false            �            1259    41058 	   File_Info    TABLE     �   CREATE TABLE public."File_Info" (
    "Id" uuid NOT NULL,
    name character varying(150) NOT NULL,
    path character varying(150) NOT NULL,
    date date NOT NULL
);
    DROP TABLE public."File_Info";
       public         heap r       postgres    false            �            1259    41063    Images    TABLE     w   CREATE TABLE public."Images" (
    "Id" uuid NOT NULL,
    "id_FileInfo" uuid NOT NULL,
    "id_Post" uuid NOT NULL
);
    DROP TABLE public."Images";
       public         heap r       postgres    false            �          0    41058 	   File_Info 
   TABLE DATA           =   COPY public."File_Info" ("Id", name, path, date) FROM stdin;
    public               postgres    false    217   [       �          0    41063    Images 
   TABLE DATA           B   COPY public."Images" ("Id", "id_FileInfo", "id_Post") FROM stdin;
    public               postgres    false    218   x       %           2606    41062    File_Info File_Info_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."File_Info"
    ADD CONSTRAINT "File_Info_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."File_Info" DROP CONSTRAINT "File_Info_pkey";
       public                 postgres    false    217            '           2606    41067    Images Images_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "Images_pkey" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Images" DROP CONSTRAINT "Images_pkey";
       public                 postgres    false    218            (           2606    41068    Images info    FK CONSTRAINT     �   ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT info FOREIGN KEY ("id_FileInfo") REFERENCES public."File_Info"("Id") NOT VALID;
 7   ALTER TABLE ONLY public."Images" DROP CONSTRAINT info;
       public               postgres    false    217    4645    218            �      x������ � �      �      x������ � �     