import path from "node:path"
// 'path' es un módulo de Node para construir rutas de archivos. Lo usamos para el alias.

import tailwindcss from "@tailwindcss/vite"
// El plugin oficial de Tailwind: procesa tu CSS y genera las clases que uses.

import react from "@vitejs/plugin-react"
// Plugin que enseña a Vite a entender JSX (el HTML dentro de JavaScript) y activa
//   el refresco instantáneo de componentes al guardar.

import { defineConfig } from "vite"
// 'defineConfig' no hace nada en tiempo de ejecución; solo da autocompletado y
//   comprobación de tipos al escribir la configuración.

export default defineConfig({
  plugins: [react(), tailwindcss()],
  // Lista de plugins activos, cada uno añade una capacidad a Vite.

  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
      // Cuando el código importe algo que empiece por "@", Vite lo traduce a la carpeta
      //   'src' (ruta absoluta). TypeScript ya lo entiende por 'paths'; esto lo hace
      //   funcionar también al ejecutar y compilar.
    },
  },

  server: {
    proxy: {
      "/api": {
        target: "http://localhost:5274",
        // Toda petición del navegador a /api/... se reenvía a tu API de .NET.
        //   Cambia el puerto por el que muestre tu API al arrancar.
        changeOrigin: true,
        // Ajusta la cabecera Host para que la API vea la petición como si fuera suya.
      },
    },
  },
})