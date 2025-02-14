PGDMP              	         }            GameMods_User    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    24641    GameMods_User    DATABASE     �   CREATE DATABASE "GameMods_User" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "GameMods_User";
                     postgres    false            �            1259    24642    Account    TABLE        CREATE TABLE public."Account" (
    id uuid NOT NULL,
    email character varying(150),
    password character varying(150)
);
    DROP TABLE public."Account";
       public         heap r       postgres    false            �            1259    24649    Account_Info    TABLE     �   CREATE TABLE public."Account_Info" (
    id uuid NOT NULL,
    date date NOT NULL,
    username character varying(150) NOT NULL
);
 "   DROP TABLE public."Account_Info";
       public         heap r       postgres    false            �            1259    24668    User    TABLE     �   CREATE TABLE public."User" (
    id uuid NOT NULL,
    id_account uuid NOT NULL,
    id_info uuid NOT NULL,
    id_avatar uuid
);
    DROP TABLE public."User";
       public         heap r       postgres    false            �          0    24642    Account 
   TABLE DATA           8   COPY public."Account" (id, email, password) FROM stdin;
    public               postgres    false    217   �       �          0    24649    Account_Info 
   TABLE DATA           <   COPY public."Account_Info" (id, date, username) FROM stdin;
    public               postgres    false    218   �       �          0    24668    User 
   TABLE DATA           D   COPY public."User" (id, id_account, id_info, id_avatar) FROM stdin;
    public               postgres    false    219   ,       +           2606    24655    Account_Info Account_Info_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."Account_Info"
    ADD CONSTRAINT "Account_Info_pkey" PRIMARY KEY (id);
 L   ALTER TABLE ONLY public."Account_Info" DROP CONSTRAINT "Account_Info_pkey";
       public                 postgres    false    218            )           2606    24648    Account Account_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Account"
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY (id);
 B   ALTER TABLE ONLY public."Account" DROP CONSTRAINT "Account_pkey";
       public                 postgres    false    217            -           2606    24672    User User_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);
 <   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_pkey";
       public                 postgres    false    219            .           2606    41073    User account    FK CONSTRAINT     ~   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT account FOREIGN KEY (id_account) REFERENCES public."Account"(id) NOT VALID;
 8   ALTER TABLE ONLY public."User" DROP CONSTRAINT account;
       public               postgres    false    217    219    4649            /           2606    41078 	   User info    FK CONSTRAINT     }   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT info FOREIGN KEY (id_info) REFERENCES public."Account_Info"(id) NOT VALID;
 5   ALTER TABLE ONLY public."User" DROP CONSTRAINT info;
       public               postgres    false    218    219    4651            �   �   x�=�KN�0 �ur4m2�{Q�X(��G���g�n�DD�ӓU/�QFt,J	�rKX@��	GU�H����>�N~�l{8/o���W�E���}���rgщ� ������1��j"�"Nl�t�PO�s~0�����}�ێT ��2tU"���h�d<tm����ǋ�ڵ�\�����]=����q��&y��:SB�      �   �   x�M�11 �:y0�c�b���P�$N�=			~O��w�VQ��=���8�R�(By�#,D�5]��R��:�2�8u�x�l��gpO�����<d
��+\qT��
Xý������x��9��ޱ&�      �   �   x��ˑD1����_c���b��?���UR�}51��"Эr����!��m���0��gtm��~����Y�@I���b%G����k��ko>uˢa	1P�vt�<����+����o�x&[w�|;N�	f"�A	)�P!${Y�o�W�Y;�>z�!M/�EA�10�Yi�eL���1
2���.y�
�Sv'�34�c2̅�K������}�j/W�     