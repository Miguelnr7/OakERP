# ADR 0001: Elección del stack tecnológico

- **Estado:** aceptada
- **Fecha:** 2026-11-18

## Contexto

OakERP es un ERP a pequeña escala (gestión y facturación) con doble objetivo: portfolio y aprendizaje. Un ERP necesita datos relacionales consistentes, transacciones, roles y permisos, auditoría e informes.

## Decisión

- Base de datos: **PostgreSQL**.
- Backend: **C# con ASP.NET Core** y Entity Framework Core.
- Frontend: **React con TypeScript** (Vite), Tailwind CSS y shadcn/ui.
- Arquitectura: **monolito modular**, empezando con una solución de tres proyectos (Api, Domain, Infrastructure) y controladores.
- Infraestructura: Docker Compose y GitHub Actions.

## Motivos

- .NET es un estándar en software empresarial y de gestión, con gran demanda.
- React con TypeScript es la opción con más demanda en frontend.
- PostgreSQL ofrece garantías ACID y un ecosistema excelente.
- Un monolito modular es proporcionado al alcance; los microservicios serían sobreingeniería.

## Consecuencias

- Curva de aprendizaje de .NET desde cero: se aborda con un desarrollo didáctico por fases.
- La arquitectura podrá evolucionar hacia módulos más separados cuando se domine lo básico.
