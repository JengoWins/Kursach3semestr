PGDMP              	         }            GameMods_Post    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    24673    GameMods_Post    DATABASE     �   CREATE DATABASE "GameMods_Post" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "GameMods_Post";
                     postgres    false            �            1259    24681    CategoryGame    TABLE     k   CREATE TABLE public."CategoryGame" (
    id uuid NOT NULL,
    category character varying(100) NOT NULL
);
 "   DROP TABLE public."CategoryGame";
       public         heap r       postgres    false            �            1259    24688    DevelopmentProcess    TABLE     q   CREATE TABLE public."DevelopmentProcess" (
    id uuid NOT NULL,
    category character varying(100) NOT NULL
);
 (   DROP TABLE public."DevelopmentProcess";
       public         heap r       postgres    false            �            1259    24693    Post    TABLE     �   CREATE TABLE public."Post" (
    id uuid NOT NULL,
    "id_Game" uuid NOT NULL,
    "id_Progress" uuid NOT NULL,
    "id_User" uuid,
    id_info uuid
);
    DROP TABLE public."Post";
       public         heap r       postgres    false            �            1259    24674 	   Post_Info    TABLE     �   CREATE TABLE public."Post_Info" (
    id uuid NOT NULL,
    "Date" date NOT NULL,
    name character varying(150) NOT NULL,
    "miniDescript" character varying(150) NOT NULL,
    "Descript" character varying(650) NOT NULL
);
    DROP TABLE public."Post_Info";
       public         heap r       postgres    false            �          0    24681    CategoryGame 
   TABLE DATA           6   COPY public."CategoryGame" (id, category) FROM stdin;
    public               postgres    false    218   �       �          0    24688    DevelopmentProcess 
   TABLE DATA           <   COPY public."DevelopmentProcess" (id, category) FROM stdin;
    public               postgres    false    219   �       �          0    24693    Post 
   TABLE DATA           R   COPY public."Post" (id, "id_Game", "id_Progress", "id_User", id_info) FROM stdin;
    public               postgres    false    220   �       �          0    24674 	   Post_Info 
   TABLE DATA           S   COPY public."Post_Info" (id, "Date", name, "miniDescript", "Descript") FROM stdin;
    public               postgres    false    217   '       /           2606    24687    CategoryGame CategoryGame_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."CategoryGame"
    ADD CONSTRAINT "CategoryGame_pkey" PRIMARY KEY (id);
 L   ALTER TABLE ONLY public."CategoryGame" DROP CONSTRAINT "CategoryGame_pkey";
       public                 postgres    false    218            1           2606    24692 *   DevelopmentProcess DevelopmentProcess_pkey 
   CONSTRAINT     l   ALTER TABLE ONLY public."DevelopmentProcess"
    ADD CONSTRAINT "DevelopmentProcess_pkey" PRIMARY KEY (id);
 X   ALTER TABLE ONLY public."DevelopmentProcess" DROP CONSTRAINT "DevelopmentProcess_pkey";
       public                 postgres    false    219            -           2606    24680    Post_Info Post_info_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Post_Info"
    ADD CONSTRAINT "Post_info_pkey" PRIMARY KEY (id);
 F   ALTER TABLE ONLY public."Post_Info" DROP CONSTRAINT "Post_info_pkey";
       public                 postgres    false    217            3           2606    24697    Post Post_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public."Post"
    ADD CONSTRAINT "Post_pkey" PRIMARY KEY (id);
 <   ALTER TABLE ONLY public."Post" DROP CONSTRAINT "Post_pkey";
       public                 postgres    false    220            4           2606    24698 	   Post game    FK CONSTRAINT     �   ALTER TABLE ONLY public."Post"
    ADD CONSTRAINT game FOREIGN KEY ("id_Game") REFERENCES public."CategoryGame"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 5   ALTER TABLE ONLY public."Post" DROP CONSTRAINT game;
       public               postgres    false    4655    220    218            5           2606    32826 	   Post info    FK CONSTRAINT     �   ALTER TABLE ONLY public."Post"
    ADD CONSTRAINT info FOREIGN KEY (id_info) REFERENCES public."Post_Info"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 5   ALTER TABLE ONLY public."Post" DROP CONSTRAINT info;
       public               postgres    false    4653    217    220            6           2606    24703    Post progress    FK CONSTRAINT     �   ALTER TABLE ONLY public."Post"
    ADD CONSTRAINT progress FOREIGN KEY ("id_Progress") REFERENCES public."DevelopmentProcess"(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;
 9   ALTER TABLE ONLY public."Post" DROP CONSTRAINT progress;
       public               postgres    false    4657    220    219            �   7   x�KMJ4N�43�543J�5I14�ML35�533�02�0�f��yE��9\1z\\\ :�      �   �   x�%�91 �:�����HJx���a�b�_ �ϸ�Z��1��"�v�4,V�t}9���c�&6����/`�	��!H�H������[X�	$ց+6��K�]�z]�⟑	J��W��;}�������n~�x{��r�_&9/,      �   �   x����E!���.�����F��ۤI� m�|���	��a��:;Sog�VR�$��A՜��Q?B��k=U��
��L�['7~v����!���S��s�$��{�l� ��nx/	y9���1�?�0V      �   K   x�3J3M2�4I�5IKJ�5IMKӵ0MLѵHJIJI6J��H2�4202�50�50�I-.	r50���, a�=... ��D     