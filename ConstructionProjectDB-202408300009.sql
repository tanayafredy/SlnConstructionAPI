--
-- PostgreSQL database dump
--

-- Dumped from database version 14.5
-- Dumped by pg_dump version 14.5

-- Started on 2024-08-30 00:09:40

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3320 (class 1262 OID 90746)
-- Name: ConstructionProjectDB; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "ConstructionProjectDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_Indonesia.1252';


ALTER DATABASE "ConstructionProjectDB" OWNER TO postgres;

\connect "ConstructionProjectDB"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 3321 (class 0 OID 0)
-- Dependencies: 3
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 213 (class 1255 OID 90758)
-- Name: deleteconstruction(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.deleteconstruction(IN _projectid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM ConstructionProject
    WHERE ProjectID = _ProjectID;
END;
$$;


ALTER PROCEDURE public.deleteconstruction(IN _projectid integer) OWNER TO postgres;

--
-- TOC entry 217 (class 1255 OID 90760)
-- Name: getallconstructions(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getallconstructions() RETURNS TABLE(projectid integer, projectname character varying, projectlocation character varying, projectstage character varying, projectcategory character varying, projectconstructionstartdate date, projectdetail character varying, projectcreatorid character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT cp.ProjectID,
    cp.ProjectName,
    cp.ProjectLocation,
    cp.ProjectStage,
    cp.ProjectCategory,
    cp.ProjectConstructionStartDate,
    cp.ProjectDetail,
    cp.ProjectCreatorID
    FROM ConstructionProject cp;
END;
$$;


ALTER FUNCTION public.getallconstructions() OWNER TO postgres;

--
-- TOC entry 226 (class 1255 OID 90759)
-- Name: getconstructionbyid(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getconstructionbyid(_projectid integer) RETURNS TABLE(projectid integer, projectname character varying, projectlocation character varying, projectstage character varying, projectcategory character varying, projectconstructionstartdate date, projectdetail character varying, projectcreatorid character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT cp.ProjectID,
    cp.ProjectName,
    cp.ProjectLocation,
    cp.ProjectStage,
    cp.ProjectCategory,
    cp.ProjectConstructionStartDate,
    cp.ProjectDetail,
    cp.ProjectCreatorID
    FROM ConstructionProject cp
    WHERE cp.ProjectID = _ProjectID;
END;
$$;


ALTER FUNCTION public.getconstructionbyid(_projectid integer) OWNER TO postgres;

--
-- TOC entry 227 (class 1255 OID 90756)
-- Name: insertconstruction(character varying, character varying, character varying, character varying, date, character varying, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.insertconstruction(IN _projectname character varying, IN _projectlocation character varying, IN _projectstage character varying, IN _projectcategory character varying, IN _projectconstructionstartdate date, IN _projectdetail character varying, IN _projectcreatorid character varying, OUT _projectid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO ConstructionProject (ProjectName, ProjectLocation, ProjectStage, ProjectCategory, ProjectConstructionStartDate, ProjectDetail, ProjectCreatorID)
    VALUES (_projectname, _projectlocation, _projectstage, _projectcategory, _projectconstructionstartdate, _projectdetail, _projectcreatorid)
    RETURNING ProjectID INTO _ProjectID;
END;
$$;


ALTER PROCEDURE public.insertconstruction(IN _projectname character varying, IN _projectlocation character varying, IN _projectstage character varying, IN _projectcategory character varying, IN _projectconstructionstartdate date, IN _projectdetail character varying, IN _projectcreatorid character varying, OUT _projectid integer) OWNER TO postgres;

--
-- TOC entry 212 (class 1255 OID 90757)
-- Name: updateconstruction(integer, character varying, character varying, character varying, character varying, date, character varying, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.updateconstruction(IN _projectid integer, IN _projectname character varying, IN _projectlocation character varying, IN _projectstage character varying, IN _projectcategory character varying, IN _projectconstructionstartdate date, IN _projectdetail character varying, IN _projectcreatorid character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE ConstructionProject
    SET ProjectName = _ProjectName, 
	ProjectLocation = _ProjectLocation, 
	ProjectStage = _ProjectStage,
	ProjectCategory = _ProjectCategory,
	ProjectConstructionStartDate = _ProjectConstructionStartDate,
	ProjectDetail = _ProjectDetail,
	ProjectCreatorID = _ProjectCreatorID
    WHERE ProjectID = _ProjectID;
END;
$$;


ALTER PROCEDURE public.updateconstruction(IN _projectid integer, IN _projectname character varying, IN _projectlocation character varying, IN _projectstage character varying, IN _projectcategory character varying, IN _projectconstructionstartdate date, IN _projectdetail character varying, IN _projectcreatorid character varying) OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 90761)
-- Name: six_digit_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.six_digit_id_seq
    START WITH 100000
    INCREMENT BY 1
    MINVALUE 100000
    MAXVALUE 999999
    CACHE 1
    CYCLE;


ALTER TABLE public.six_digit_id_seq OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 90748)
-- Name: constructionproject; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.constructionproject (
    projectid integer DEFAULT nextval('public.six_digit_id_seq'::regclass) NOT NULL,
    projectname character varying(200),
    projectlocation character varying(500),
    projectstage character varying(100),
    projectcategory character varying(100),
    projectconstructionstartdate date,
    projectdetail character varying(2000),
    projectcreatorid character varying(100)
);


ALTER TABLE public.constructionproject OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 90747)
-- Name: constructionproject_projectid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.constructionproject_projectid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.constructionproject_projectid_seq OWNER TO postgres;

--
-- TOC entry 3322 (class 0 OID 0)
-- Dependencies: 209
-- Name: constructionproject_projectid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.constructionproject_projectid_seq OWNED BY public.constructionproject.projectid;


--
-- TOC entry 3313 (class 0 OID 90748)
-- Dependencies: 210
-- Data for Name: constructionproject; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.constructionproject (projectid, projectname, projectlocation, projectstage, projectcategory, projectconstructionstartdate, projectdetail, projectcreatorid) FROM stdin;
100000	string	string	Concept	string	2024-08-30	string	string
100001	string	string	Concept	string	2024-08-30	string	string
\.


--
-- TOC entry 3323 (class 0 OID 0)
-- Dependencies: 209
-- Name: constructionproject_projectid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.constructionproject_projectid_seq', 14, true);


--
-- TOC entry 3324 (class 0 OID 0)
-- Dependencies: 211
-- Name: six_digit_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.six_digit_id_seq', 100001, true);


--
-- TOC entry 3172 (class 2606 OID 90755)
-- Name: constructionproject constructionproject_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.constructionproject
    ADD CONSTRAINT constructionproject_pkey PRIMARY KEY (projectid);


-- Completed on 2024-08-30 00:09:40

--
-- PostgreSQL database dump complete
--

