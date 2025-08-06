// RestComponent.tsx
import React, { useEffect, useState } from "react";

type RestData = {
  name: string;
};

const RestComponent = () => {
  const [data, setData] = useState<RestData | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch("https://localhost:7161/fake")
      .then((res) => {
        if (!res.ok) throw new Error("REST API error");
        return res.json() as Promise<RestData>;
      })
      .then((data) => setData(data))
      .catch((err) => setError(err.message));
  }, []);

  if (error) return <p style={{ color: "red" }}>Błąd REST: {error}</p>;
  if (!data) return <p>Ładowanie REST danych...</p>;

  return (
    <div>
      <h2>Dane z REST API</h2>
      <pre>
        Name: <b>{data.name}</b>
      </pre>
    </div>
  );
};

export default RestComponent;
