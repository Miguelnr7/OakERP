# OakERP

ERP a pequeña escala para gestión y facturación. Proyecto de portfolio y de aprendizaje, construido desde cero y paso a paso.

> Estado: **Fase 0** (entorno y repositorio).

## Stack

- **Backend:** C# con ASP.NET Core (.NET LTS), Entity Framework Core
- **Base de datos:** PostgreSQL
- **Frontend:** React + TypeScript (Vite), Tailwind CSS y shadcn/ui
- **Infraestructura:** Docker / Docker Compose, GitHub Actions

## Estructura del repositorio

| Carpeta | Contenido |
|---|---|
| `backend/` | Solución .NET (API, dominio e infraestructura) |
| `frontend/` | Aplicación React |
| `infra/` | Docker Compose y configuración de despliegue |
| `docs/` | Documentación, decisiones de arquitectura (ADRs) y wireframes |

## Cómo ejecutarlo

Pendiente: se completará cuando exista la primera versión funcional.

## Alcance del MVP

- Usuarios y roles
- Clientes y proveedores
- Catálogo de productos
- Facturación (series, líneas, IVA, totales y PDF)

## Hoja de ruta

0. Entorno y repositorio
1. Primera API
2. Base de datos (PostgreSQL + EF Core)
3. Primer módulo: clientes
4. Frontend (React)
5. Seguridad (JWT, roles)
6. Facturación
7. CI/CD, tests y despliegue
