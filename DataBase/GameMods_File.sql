PGDMP  9            	         }            GameMods_File    17.0    17.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    24708    GameMods_File    DATABASE     �   CREATE DATABASE "GameMods_File" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "GameMods_File";
                     postgres    false            �            1259    24736    Archive    TABLE     �   CREATE TABLE public."Archive" (
    id uuid NOT NULL,
    "id_ArchiveInfo" uuid NOT NULL,
    "id_Post" uuid NOT NULL,
    "id_FileInfo" uuid NOT NULL
);
    DROP TABLE public."Archive";
       public         heap r       postgres    false            �            1259    24716    Archive_Info    TABLE     v   CREATE TABLE public."Archive_Info" (
    id uuid NOT NULL,
    size bigint NOT NULL,
    download integer NOT NULL
);
 "   DROP TABLE public."Archive_Info";
       public         heap r       postgres    false            �            1259    24709 	   File_Info    TABLE     �   CREATE TABLE public."File_Info" (
    id uuid NOT NULL,
    name character varying(150) NOT NULL,
    path character varying(350) NOT NULL,
    date date NOT NULL
);
    DROP TABLE public."File_Info";
       public         heap r       postgres    false            �          0    24736    Archive 
   TABLE DATA           S   COPY public."Archive" (id, "id_ArchiveInfo", "id_Post", "id_FileInfo") FROM stdin;
    public               postgres    false    219   d       �          0    24716    Archive_Info 
   TABLE DATA           <   COPY public."Archive_Info" (id, size, download) FROM stdin;
    public               postgres    false    218   �       �          0    24709 	   File_Info 
   TABLE DATA           ;   COPY public."File_Info" (id, name, path, date) FROM stdin;
    public               postgres    false    217   �       +           2606    24720    Archive_Info Archive_Info_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."Archive_Info"
    ADD CONSTRAINT "Archive_Info_pkey" PRIMARY KEY (id);
 L   ALTER TABLE ONLY public."Archive_Info" DROP CONSTRAINT "Archive_Info_pkey";
       public                 postgres    false    218            -           2606    24740    Archive Archive_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public."Archive"
    ADD CONSTRAINT "Archive_pkey" PRIMARY KEY (id);
 B   ALTER TABLE ONLY public."Archive" DROP CONSTRAINT "Archive_pkey";
       public                 postgres    false    219            )           2606    24715    File_Info File_Info_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."File_Info"
    ADD CONSTRAINT "File_Info_pkey" PRIMARY KEY (id);
 F   ALTER TABLE ONLY public."File_Info" DROP CONSTRAINT "File_Info_pkey";
       public                 postgres    false    217            .           2606    24761    Archive file_Archive    FK CONSTRAINT     �   ALTER TABLE ONLY public."Archive"
    ADD CONSTRAINT "file_Archive" FOREIGN KEY ("id_FileInfo") REFERENCES public."File_Info"(id) NOT VALID;
 B   ALTER TABLE ONLY public."Archive" DROP CONSTRAINT "file_Archive";
       public               postgres    false    219    217    4649            /           2606    24766    Archive info    FK CONSTRAINT     �   ALTER TABLE ONLY public."Archive"
    ADD CONSTRAINT info FOREIGN KEY ("id_ArchiveInfo") REFERENCES public."Archive_Info"(id) NOT VALID;
 8   ALTER TABLE ONLY public."Archive" DROP CONSTRAINT info;
       public               postgres    false    4651    219    218            �      x������ � �      �      x������ � �      �      x������ � �     