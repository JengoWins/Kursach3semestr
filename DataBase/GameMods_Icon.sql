PGDMP              	         }            GameMods_Icon    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    41041    GameMods_Icon    DATABASE     �   CREATE DATABASE "GameMods_Icon" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "GameMods_Icon";
                     postgres    false            �            1259    41042 	   File_Info    TABLE     �   CREATE TABLE public."File_Info" (
    "Id" uuid NOT NULL,
    name character varying(150) NOT NULL,
    path character varying(150) NOT NULL,
    date date NOT NULL
);
    DROP TABLE public."File_Info";
       public         heap r       postgres    false            �            1259    41047    Image_IconPost    TABLE        CREATE TABLE public."Image_IconPost" (
    "Id" uuid NOT NULL,
    "id_FileInfo" uuid NOT NULL,
    "id_Post" uuid NOT NULL
);
 $   DROP TABLE public."Image_IconPost";
       public         heap r       postgres    false            �          0    41042 	   File_Info 
   TABLE DATA           =   COPY public."File_Info" ("Id", name, path, date) FROM stdin;
    public               postgres    false    217   �       �          0    41047    Image_IconPost 
   TABLE DATA           J   COPY public."Image_IconPost" ("Id", "id_FileInfo", "id_Post") FROM stdin;
    public               postgres    false    218   �       %           2606    41046    File_Info File_Info_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."File_Info"
    ADD CONSTRAINT "File_Info_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."File_Info" DROP CONSTRAINT "File_Info_pkey";
       public                 postgres    false    217            '           2606    41051 "   Image_IconPost Image_IconPost_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public."Image_IconPost"
    ADD CONSTRAINT "Image_IconPost_pkey" PRIMARY KEY ("Id");
 P   ALTER TABLE ONLY public."Image_IconPost" DROP CONSTRAINT "Image_IconPost_pkey";
       public                 postgres    false    218            (           2606    41052    Image_IconPost info    FK CONSTRAINT     �   ALTER TABLE ONLY public."Image_IconPost"
    ADD CONSTRAINT info FOREIGN KEY ("id_FileInfo") REFERENCES public."File_Info"("Id");
 ?   ALTER TABLE ONLY public."Image_IconPost" DROP CONSTRAINT info;
       public               postgres    false    217    4645    218            �   �   x����JA�s�]�;�$��B�^�L&���Vi���������@���Qs���r%�TjO
k5��5�ü�����~��r��u������cyoS ��x�4��0c���� .\��4�����QRp�l��j��r
�v�.Op[;�������y�����@򄅈%
0�BA$�C"��;��M��n�9���C�ЁR�-c���͇y��W�n�C      �   \   x�ɱ�0����%z�Ā鿄'�}��"�B���Q����a�J.����2����E\[��r�U�0���U���+M}��=���)�     