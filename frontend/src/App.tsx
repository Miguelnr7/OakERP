import { useEffect, useState } from "react"
// Dos "hooks" de React. Un hook es una función especial que da a un componente
//   capacidades extra: useState guarda datos, useEffect ejecuta código en momentos concretos.

import { Button } from "@/components/ui/button"
// El '@' es el alias que configuramos. Importamos el botón de shadcn, que es un
//   archivo que vive en nuestro propio proyecto.

export default function App() {
  // Un componente de React es una función que devuelve lo que se debe pintar.

  const [estado, setEstado] = useState("comprobando…")
  // useState devuelve un par: el valor actual ('estado') y la función para cambiarlo
  //   ('setEstado'). Cuando llamas a setEstado, React vuelve a pintar el componente.

  useEffect(() => {
    // Código que se ejecuta después de pintar. Con el array vacío del final, solo una vez.
    fetch("/api/health")
      // Petición a nuestra API. La ruta empieza por /api, así que el proxy de Vite
      //   la reenvía a .NET.
      .then((r) => r.json())
      // Convierte el cuerpo de la respuesta de JSON a un objeto.
      .then((d) => setEstado(d.status))
      // Guarda el campo 'status' ("ok") en el estado, y React lo pinta.
      .catch(() => setEstado("sin conexión"))
      // Si algo falla (la API apagada, por ejemplo), lo indicamos.
  }, [])
  // '[]' son las dependencias: vacío significa "ejecútalo solo al montar el componente".

  return (
    <main className="min-h-screen bg-background p-8 text-foreground">
      {/* className usa clases de Tailwind: altura mínima de pantalla, fondo y texto de
          nuestra paleta (bg-background y text-foreground leen las variables de index.css),
          y 2rem de relleno (p-8). */}
      <h1 className="text-2xl font-bold">
        <span className="text-primary">Oak</span>ERP
      </h1>
      <p className="mt-2 text-muted-foreground">
        Comprobacion de la API: <strong>{estado}</strong>
        {/* Las llaves insertan una variable de JavaScript dentro del JSX. */}
      </p>
      <Button className="mt-6">Guardar cliente</Button>
    </main>
  )
}