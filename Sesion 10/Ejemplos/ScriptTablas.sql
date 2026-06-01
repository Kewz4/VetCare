-- public.alumnos definition

-- Drop table

-- DROP TABLE public.alumnos;

CREATE TABLE public.alumnos (
id serial4 NOT NULL,
nombre varchar(100) NOT NULL,
apellido varchar(100) NOT NULL,
email varchar(150) NOT NULL,
fecha_nacimiento date NULL,
telefono varchar(20) NULL,
direccion text NULL,
fecha_registro timestamp DEFAULT CURRENT_TIMESTAMP NULL,
activo bool DEFAULT true NULL,
CONSTRAINT alumnos_email_key UNIQUE (email),
CONSTRAINT alumnos_pkey PRIMARY KEY (id)
);

-- Crear tabla de materias
CREATE TABLE public.materias (
    id serial4 NOT NULL,
    nombre varchar(150) NOT NULL,
    codigo varchar(50) NOT NULL UNIQUE,
    descripcion text NULL,
    creditos int4 DEFAULT 3 NULL,
    horas_teorica int4 DEFAULT 0 NULL,
    horas_practica int4 DEFAULT 0 NULL,
    profesor varchar(100) NULL,
    activa bool DEFAULT true NULL,
    fecha_creacion timestamp DEFAULT CURRENT_TIMESTAMP NULL,
    CONSTRAINT materias_pkey PRIMARY KEY (id),
    CONSTRAINT materias_codigo_key UNIQUE (codigo)
);

-- Crear tabla de unión (muchos-a-muchos entre alumnos y materias)
CREATE TABLE public.alumnos_materias (
    id serial4 NOT NULL,
    alumno_id int4 NOT NULL,
    materia_id int4 NOT NULL,
    calificacion decimal(4,2) NULL,
    estado varchar(50) DEFAULT 'inscrito' NULL, -- inscrito, aprobado, reprobado, retirado
    fecha_inscripcion timestamp DEFAULT CURRENT_TIMESTAMP NULL,
    fecha_calificacion timestamp NULL,
    CONSTRAINT alumnos_materias_pkey PRIMARY KEY (id),
    CONSTRAINT alumnos_materias_alumno_fk FOREIGN KEY (alumno_id) 
        REFERENCES public.alumnos(id) ON DELETE CASCADE,
    CONSTRAINT alumnos_materias_materia_fk FOREIGN KEY (materia_id) 
        REFERENCES public.materias(id) ON DELETE CASCADE,
    CONSTRAINT alumnos_materias_unique UNIQUE (alumno_id, materia_id)
);

-- Crear índices para mejorar rendimiento
CREATE INDEX idx_alumnos_materias_alumno ON public.alumnos_materias(alumno_id);
CREATE INDEX idx_alumnos_materias_materia ON public.alumnos_materias(materia_id);
CREATE INDEX idx_alumnos_materias_estado ON public.alumnos_materias(estado);